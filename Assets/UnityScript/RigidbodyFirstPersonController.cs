using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


    

    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {

        [Serializable]
        public class MovementSettings
        {
            public float ForwardSpeed = 8.0f;   // Speed when walking forward
            public float BackwardSpeed = 4.0f;  // Speed when walking backwards
            public float StrafeSpeed = 4.0f;    // Speed when walking sideways
            public float RunMultiplier = 2.0f;   // Speed when sprinting
	        public KeyCode RunKey = KeyCode.LeftShift;
            public float JumpForce = 30f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 8f;

#if !MOBILE_INPUT
            private bool m_Running;
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
	            if (input == Vector2.zero) return;
				if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
				}
				if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
				}
#if !MOBILE_INPUT
	            if (Input.GetKey(RunKey))
	            {
		            CurrentTargetSpeed *= RunMultiplier;
		            m_Running = true;
	            }
	            else
	            {
		            m_Running = false;
	            }
#endif
            }

#if !MOBILE_INPUT
            public bool Running
            {
                get { return m_Running; }
            }
#endif
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }
        public enum State{
            holdRifle,Switching,closeRifle,noBullet,noAmm
        }
        private State state = State.holdRifle;
        
        public KeyCode weponSwitchKey = KeyCode.Alpha1;
        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();

        private Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal;
        private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;
        [SerializeField] private GameObject Gun = null;
        private bool m_WeaponSwitch = false;
        [SerializeField] private float minGun_posY = 0.1F,maxGun_posY = 0.5F;
        [SerializeField] private Vector3 gunposition = Vector3.zero;
        [SerializeField] private Animator m_animator = null;
        [SerializeField] private GameObject bulletPrefab = null;
        [SerializeField] private GameObject Bullet_transform = null;
        [SerializeField] private GameObject Deer = null;
        [SerializeField] private GameObject Cardridge_transform = null;
        [SerializeField] private GameObject CardridgePrefab = null;
        [SerializeField] private GameObject AmmUI = null;
        [SerializeField] private GameObject myAudioCtl = null;
        [SerializeField] private int haveAmm = 5;
        private bool noAmm = false;

        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }

        public bool Jumping
        {
            get { return m_Jumping; }
        }
        public bool Running
        {
            get
            {
 #if !MOBILE_INPUT
				return movementSettings.Running;
#else
	            return false;
#endif
            }
        }
        
        private void OnTriggerEnter(Collider other) {
            
            if(other.tag == "sight"){
                Debug.Log(other.name);
                Deer.GetComponent<DeerController>().SetState_Vigilant();
                Deer.GetComponent<DeerController>().ReSetDestination();
            }
            if(other.tag == "vigiland"){
                Debug.Log(other.tag);
                Deer.GetComponent<DeerController>().Animation_LookAroundLeft();
            }
        }

        private void Start()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_animator = GetComponent<Animator>();
            mouseLook.Init (transform, cam.transform);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return)){
                Debug.Log(Bullet_transform.name);
            }
            RotateView();
            AnimationTest();
            
            switch(state){
                case State.closeRifle:
                    if(Input.GetKeyDown(KeyCode.Return)){
                        Debug.Log("closeRifleState");
                    }
                    StateChange();
                    break;
                case State.holdRifle:
                    if(Input.GetKeyDown(KeyCode.Return)){
                        Debug.Log("haveRifleState");
                    }
                    StateChange();
                    if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale == 1f){
                        // TriggerHappy();
                        if(!noAmm){
                            BulletFire();
                        }
                    }
                    break;
                case State.noBullet: 
                    if(Input.GetKeyDown(KeyCode.R)){
                        Reload();
                    }
                    break;
                // case State.noAmm:
                //     if(Input.GetKeyDown(KeyCode.R)){
                //         Reload();
                //     }
                //     break;
                case State.Switching:
                    // SwitchingWeapon();
                    break;
                default:
                    break;
            }
            // if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump)
            // {
            //     m_Jump = true;
            // }
        }
        void BulletInstance(){
            GameObject Bullet = 
            Instantiate(bulletPrefab,Bullet_transform.transform.position,Bullet_transform.transform.rotation) as GameObject;
        
        }
        public void Cardridge_throw(){
            GameObject Cardridge = 
            Instantiate(CardridgePrefab,Cardridge_transform.transform.position,Cardridge_transform.transform.rotation) as GameObject;
        }
        
        // //弾のステートはboolで管理したほうがいい気もする
        void BulletFire(){
            BulletInstance();
            FireSound();
            state = State.noBullet;
            Debug.Log("Fire");
            haveAmm--;
            AmmUI.GetComponent<AmmDisplayController>().ChangeAmmValue(haveAmm);
            AmmUI.GetComponent<AmmDisplayController>().AmmImageInvisible();
            Debug.Log(haveAmm);
            if(haveAmm < 1){
                haveAmm = 0;
                noAmm = true;
            }
        }
        //テスト用撃ち放題
        // void TriggerHappy(){
        //     BulletInstance();
        // }

        void Reload(){
            if(state == State.noBullet){
                m_animator.Play("BoltAction");

                // Cardridge_throw();
            }else if(state == State.noAmm){
                m_animator.Play("noAmmBoltAction");
            }
            else{
                Debug.Log("err");
            }
        }
        public void BoltActionSound(){
            myAudioCtl.GetComponent<AudioController>().BoltActionSoundPlay();
        }
        public void BoltActionSound2(){
            myAudioCtl.GetComponent<AudioController>().BoltActionSound2Play();
        }
        public void FireSound(){
            myAudioCtl.GetComponent<AudioController>().FireSoundPlay();
        }
        private void FixedUpdate()
        {
            GroundCheck();
            Vector2 input = GetInput();
            
            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
            {
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = cam.transform.forward*input.y + cam.transform.right*input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                desiredMove.x = desiredMove.x*movementSettings.CurrentTargetSpeed;
                desiredMove.z = desiredMove.z*movementSettings.CurrentTargetSpeed;
                desiredMove.y = desiredMove.y*movementSettings.CurrentTargetSpeed;
                if (m_RigidBody.velocity.sqrMagnitude <
                    (movementSettings.CurrentTargetSpeed*movementSettings.CurrentTargetSpeed))
                {
                    m_RigidBody.AddForce(desiredMove*SlopeMultiplier(), ForceMode.Impulse);
                }
            }

            if (m_IsGrounded)
            {
                m_RigidBody.drag = 5f;

                if (m_Jump)
                {
                    m_RigidBody.drag = 0f;
                    m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                    m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                    m_Jumping = true;
                }

                if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
                {
                    m_RigidBody.Sleep();
                }
            }
            else
            {
                m_RigidBody.drag = 0f;
                if (m_PreviouslyGrounded && !m_Jumping)
                {
                    StickToGroundHelper();
                }
            }
            m_Jump = false;
        }
        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }

        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) +
                                   advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                {
                    m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
                }
            }
        }


        private Vector2 GetInput()
        {
            
            Vector2 input = new Vector2
                {
                    x = CrossPlatformInputManager.GetAxis("Horizontal"),
                    y = CrossPlatformInputManager.GetAxis("Vertical")
                };
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }

        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation (transform, cam.transform);

            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                m_RigidBody.velocity = velRotation*m_RigidBody.velocity;
            }
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_IsGrounded = true;
                m_GroundContactNormal = hitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = Vector3.up;
            }
            if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
            {
                m_Jumping = false;
            }
        }

        private void StateChange(){
            if(Input.GetKeyDown(KeyCode.Alpha1) &&
                (state == State.closeRifle)){
                state = State.Switching;
                m_animator.Play("HoldRifle");

            }
            if(Input.GetKeyDown(KeyCode.Alpha1) &&
                (state == State.holdRifle)){
                state = State.Switching;
                m_animator.Play("LowerTheRifle");
            }
        }
        //設定した値を超えたときに値を正しくする関数
        //引数が0の時は何もしない
        //ローカルポジションで使う事
        void floatToLocalPosition(float argx,float argy,float argz,GameObject targetObj){
            Vector3 postmp = targetObj.transform.localPosition;
            if(argx != 0)  postmp.x = argx;
            if(argy != 0)  postmp.y = argy;
            if(argz != 0)  postmp.z = argz;
            targetObj.transform.localPosition = postmp;
        }
        void AnimationTest(){
            // if(Input.GetKeyDown(KeyCode.Alpha3)){
            //     m_animator.Play("LowerTheRifle");
            // }
            // if(Input.GetKeyDown(KeyCode.Alpha4)){
            //     m_animator.Play("BoltAction");
            // }
        }
        public void ChangeState_Hold(){
            if(haveAmm >0){
                AmmUI.GetComponent<AmmDisplayController>().AmmImageSee();
            }
            state = State.holdRifle;
            Debug.Log("HoldRifleState");
        }
        public void ChangeState_Close(){
            state = State.closeRifle;
            Debug.Log("closeRifleState");
        }        
    }

