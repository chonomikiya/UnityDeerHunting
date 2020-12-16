using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour
{
    Rigidbody m_rigidbody = null;
    [SerializeField] float walkspeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)){
            AddWalk();
        }
        
    }
    public void AddWalk(){
        m_rigidbody.AddRelativeForce(Vector3.forward * walkspeed);
    }

}
