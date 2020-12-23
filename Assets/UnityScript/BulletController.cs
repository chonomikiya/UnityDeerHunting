using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    Rigidbody m_rigidbody = null;
    [SerializeField] float initial_velocity = 0;
    Vector3 speed;
    
    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "body_L"){
            Debug.Log(other.collider.tag);
            // this.gameObject.GetComponent<Rigidbody>().isKinematic=true;
            other.gameObject.GetComponent<DeerController>().Animatiopn_Damage_Left_Play();
            Destroy(this.gameObject);
            return ;
        }
        if(other.collider.tag == "body_R"){
            Debug.Log(other.collider.tag);
            // this.gameObject.GetComponent<Rigidbody>().isKinematic=true;
            other.gameObject.GetComponent<DeerController>().Animatiopn_Damage_Right_Play();
            Destroy(this.gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.velocity = transform.forward * initial_velocity;
        Invoke("Destroy_this",10F);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Destroy_this(){
        Destroy(this.gameObject);
    }
}
