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
 
    private float speed;        // 移動速度    
    private float rotateSpeed;  // 回転速度
    private float gravity;      // 重力
    private float jumpPower;    // ジャンプ力

    [SerializeField]private float run_Limit = 3.5F;
    private float runspeed = 1F;
    private float runvelo = 0.005F;
 
    // Start is called before the first frame update
    void Start()
    {
        state = State.move;
        this.rotateSpeed = 60.0f;
        this.gravity = 10f;   
        this.jumpPower = 6;
        this.speed = 6.0f;
 
        this.controller = this.gameObject.GetComponent<CharacterController>();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        this.addForceDownPower = Vector3.zero;
        switch(state){
                case State.move:
                    PlayerMove();
                    break;
                default:
                    break;
        };
    }
    float RunVelocity(float arg_run){
        float runvalue = arg_run;
        runvalue += (runvelo* Time.deltaTime);
        if(runvalue > run_Limit){
            runvalue = run_Limit;
        }
        return runvalue;
    }
    void PlayerMove(){
        float m_run = 1;
        if(Input.GetKey(KeyCode.LeftShift)){
            m_run = run_Limit;
            if(m_run > run_Limit){
                m_run = run_Limit;
            }
        }
        if (controller.isGrounded){
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
            this.moveDirection = transform.forward * speed * Input.GetAxis("Vertical");
            if (Input.GetButtonDown("Jump"))
            {
                this.addForceDownPower = Vector3.zero;
                moveDirection.y = this.jumpPower;
            }
        }else{
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
            this.moveDirection.y -= this.gravity * Time.deltaTime;
        }
        controller.Move((this.moveDirection　* m_run)* Time.deltaTime + this.addForceDownPower);
    }
}