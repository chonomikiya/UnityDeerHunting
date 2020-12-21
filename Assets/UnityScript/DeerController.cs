using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerController : MonoBehaviour
{
    
    enum State {
        walk,
        lookAround,
        idle,
        run
    }
    // enum NavTarget{

    // }


    Rigidbody m_rigidbody = null;
    State state = State.idle;
    NavMeshAgent DeerNav;
    [SerializeField] GameObject Destination;

    [SerializeField] GameObject Destination02;
    [SerializeField] GameObject Destination03;
    [SerializeField] GameObject Destination04;
    [SerializeField] GameObject DeerDroppingPrefab;
    [SerializeField] GameObject DroppingPosition;
    int NavValue=0;
    Animator m_DeerAnimator;
    [SerializeField] int max_Random_value = 6;
    Vector3 NavPos;
    bool navSwitch = false;


    // Start is called before the first frame update

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        DeerNav = GetComponent<NavMeshAgent>();
        //はじめに複数行き先を用意して順番に回る
        //二番目に近いTargetへ行く
        m_DeerAnimator = GetComponent<Animator>();
    }
    void Update() {
        switch(state){
            case State.idle:
                break;
            default :
                break;
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            // Animatinon_Idle_Play();
            // Eat_or_nothing();
            SetDestination();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            Dropping();
        }
    }
    void FixedUpdate(){
        if(Vector3.Distance(this.transform.position,NavPos) < 5){
            // SetDestination();
            Animation_Eat_Play();
            DeerNav.ResetPath();
        }
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
    
    public void Animation_Eat_Play(){
        m_DeerAnimator.Play("Eat");
    }
    public void Animatinon_Idle_Play(){
        m_DeerAnimator.Play("Idle");
    }
    public void Animation_walk_Play(){
        m_DeerAnimator.Play("walk");
        DeerNav.SetDestination(Destination.transform.position);
    }

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
        m_DeerAnimator.Play("walk");
    }
}
