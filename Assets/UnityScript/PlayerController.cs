using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum State{
        move,haveRifle
    }
    State state;
    private Vector3 moveDirection;
    private Vector3 addForceDownPower = Vector3.zero;
    private CharacterController controller;
    private Rigidbody m_rigidbody = null;
 
    // private float speed;        // 移動速度    
    // private float rotateSpeed;  // 回転速度
    // private float gravity;      // 重力
    // private float jumpPower;    // ジャンプ力
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float jumpPower = 5f;
    //　地面に接地しているかどうか
    [SerializeField]
    private bool isGrounded;
    //　入力値
    private Vector3 input;
    //　歩く速さ
    [SerializeField]
    private float walkSpeed = 1.5f;    

    [SerializeField]private float run_maxLimit = 3.5F;
    [SerializeField]private float run_minLimit = 1.0F;

    [SerializeField]private float runspeed = 1F;
    [SerializeField]private float runvelo = 0.005F;
    [SerializeField]public float restoringTorqueMagnitude = 100.0f;

 
    // Start is called before the first frame update
    void Start()
    {
        state = State.move;
        // this.rotateSpeed = 60.0f;
        // this.gravity = 10f;   
        // this.jumpPower = 6;
        // this.speed = 6.0f;
        this.m_rigidbody = this.gameObject.GetComponent<Rigidbody>();
        // this.controller = this.gameObject.GetComponent<CharacterController>();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        this.addForceDownPower = Vector3.zero;
        switch(state){
                case State.move:
                    // PlayerMove();
                    RigidMove();
                    break;
                default:
                    break;
        };
    }
    float RunVelocity(float arg_run){
        float runvalue = arg_run;
        runvalue += (runvelo* Time.deltaTime);
        if(runvalue > run_maxLimit){
            runvalue = run_maxLimit;
        }
        return runvalue;
    }
    
    void RigidMove(){
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        // m_rigidbody.AddRelativeForce(x*10, 0, z*10);
        // Vector3 right = transform.right;
        // Vector3 up = transform.up;
        // Vector3 forward = transform.forward;
        // Vector3 restoringTorque = new Vector3(forward.y - up.z, right.z - forward.x, up.x - right.y) * restoringTorqueMagnitude;

        // // 機体にトルクを加える
        // m_rigidbody.AddTorque(restoringTorque);
    }
    // void PlayerMove(){
    //     if(Input.GetKey(KeyCode.LeftShift)){
    //         runspeed += (run_maxLimit-runspeed)*((Time.deltaTime+1F)*Time.deltaTime/2.0F);
    //         if(runspeed > run_maxLimit){
    //             runspeed = run_maxLimit;
    //         }
    //     }
    //     if(Input.GetKeyUp(KeyCode.LeftShift)){
    //         runspeed = run_minLimit;
    //     }

    //     if (controller.isGrounded){
    //         transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
    //         this.moveDirection = transform.forward * speed * Input.GetAxis("Vertical");
    //         if (Input.GetButtonDown("Jump"))
    //         {
    //             this.addForceDownPower = Vector3.zero;
    //             moveDirection.y = this.jumpPower;
    //         }
    //     }else{
    //         transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
    //         this.moveDirection.y -= this.gravity * Time.deltaTime;
    //     }
    //     controller.Move((this.moveDirection　* runspeed)* Time.deltaTime + this.addForceDownPower);
    // }
}