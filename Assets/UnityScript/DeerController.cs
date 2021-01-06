using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DeerController : MonoBehaviour
{
    
    enum State {
        walk,
        lookAround,
        vigilant,
        injured,
        idle,
        run
    }
    Rigidbody m_rigidbody = null;
    State state = State.idle;
    NavMeshAgent DeerNav;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Destination;

    [SerializeField] GameObject Destination02;
    [SerializeField] GameObject Destination03;
    [SerializeField] GameObject Destination04;
    [SerializeField] GameObject DeerDroppingPrefab;
    [SerializeField] GameObject DroppingPosition;
    int NavValue=0;
    [SerializeField] int m_Health = 5;
    Animator m_DeerAnimator;
    [SerializeField] int max_Random_value = 6;
    Vector3 NavPos;
    bool navSwitch = false;
    bool vigilant = false;
    bool Deer_isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        DeerNav = GetComponent<NavMeshAgent>();
        //はじめに複数行き先を用意して順番に回る
        //二番目に近いTargetへ行く
        m_DeerAnimator = GetComponent<Animator>();
        SetDestination();
    }
    void Update() {
        // switch(state){
        //     case State.idle:
        //         break;
        //     case State.vigilant:
        //         break;
        //     default :
        //         break;
        // }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            Dropping();
        }
    }
    void FixedUpdate(){
        if(Vector3.Distance(this.transform.position,NavPos) < 5){
            // SetDestination();
            if(state == State.idle){
                Animation_Eat_Play();
                DeerNav.ResetPath();
            }else if(state == State.vigilant){
                SetDestination();
            }
        }
    }
    public bool getDeer_isDie(){
        return Deer_isDie;
    }
    //Idleアニメーション時に呼び出す関数
    //1/max_Random_valueでEatアニメーションを再生
    public void Eat_or_nothing(){
        int log = Random.Range(0,max_Random_value);
        if(log == 0){
            Animation_Eat_Play();
            Debug.Log(log);
            Debug.Log(true);
        }else{
            Debug.Log(log);
        }
    }
    public void Dropping(){
        GameObject Drop = Instantiate(DeerDroppingPrefab,DroppingPosition.transform.position,DroppingPosition.transform.rotation) as GameObject;
    }
    public void After_Run_switch(){
        switch(state){
            case State.idle:
                break;
            case State.vigilant:
                PlayerDistance();
                break;
            case State.injured:
                Damage_to_Life();
                break;
            default :
                break;
        }
    }
    public void PlayerDistance(){
        if(Vector3.Distance(Player.transform.position,this.transform.position) > 100){
            SetState_Idle();
            Animation_walk_Play();
            ReSetDestination();
        }
    }

    public void Animation_Eat_Play(){
        m_DeerAnimator.Play("Eat");
    }
    public void Animatinon_Idle_Play(){
        m_DeerAnimator.Play("Idle");
    }
    public void Animation_walk_Play(){
        // m_DeerAnimator.Play("walk");
        Run_or_Walk();
        // DeerNav.SetDestination(Destination.transform.position);
    }
    public void Animatiopn_Damage_Left_Play(){
        m_DeerAnimator.Play("Damage_Left");
        SetState_injured();
    }
    public void Animatiopn_Damage_Right_Play(){
        m_DeerAnimator.Play("Damage_Right");
        SetState_injured();
    }
    public void HeadShot(){
        state = State.injured;
        m_Health = 0;
        Animation_Die_Play();
    }

    public void Animation_Die_Play(){
        m_DeerAnimator.Play("Die_Left");
        Deer_isDie = true;
        m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void Animation_LookAroundLeft(){
        if(state == State.idle){
            m_DeerAnimator.Play("LookAroundLeft");
            DeerNav.ResetPath();
        }
    }
    public void Run_or_Walk(){
        switch(state){
            case State.idle:
                m_DeerAnimator.Play("walk");
                break;
            case State.vigilant:
                m_DeerAnimator.Play("Run");
                break;
            default :
                Debug.Log("Run_or_walk() default");
                break;
        }
    }

    public void Damage_to_Life(){

        m_Health--;
        if(m_Health <= 0){
            Animation_Die_Play();
            DeerNav.ResetPath();
            m_rigidbody.velocity = Vector3.zero;
        }
    }


    //Stateの管理、アクセス用の関数

    public void SetState_Vigilant(){
        state = State.vigilant;
    }
    public void SetState_Idle(){
        state = State.idle;
    }
    public void SetState_injured(){
        state = State.injured;
    }
    //目的地をセットする関数
    public void SetDestination(){
        switch(NavValue){
            case 0:
                NavPos = Destination.transform.position;
                if(navSwitch)  navSwitch = !navSwitch;
                break;
            case 1:
                NavPos = Destination02.transform.position;
                break;
            case 2:
                NavPos = Destination03.transform.position;
                break;
            case 3:
                NavPos = Destination04.transform.position;
                if(!navSwitch)  navSwitch = !navSwitch;
                break;
            default:
                break;
        }
        if(navSwitch)   NavValue--;
        if(!navSwitch)  NavValue++;
        DeerNav.SetDestination(NavPos);
        // m_DeerAnimator.Play("walk");
        Run_or_Walk();
    }
    public void ReSetDestination(){
        switch(NavValue){
            case 0:
                NavPos = Destination.transform.position;
                if(navSwitch)  navSwitch = !navSwitch;
                break;
            case 1:
                NavPos = Destination02.transform.position;
                break;
            case 2:
                NavPos = Destination03.transform.position;
                break;
            case 3:
                NavPos = Destination04.transform.position;
                if(!navSwitch)  navSwitch = !navSwitch;
                break;
            default:
                break;
        }
        DeerNav.SetDestination(NavPos);
        Run_or_Walk();
    }
}
