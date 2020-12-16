using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour
{
    enum State {
        walk,lookAround,idle,run
    }
    Rigidbody m_rigidbody = null;
    

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    void Update() {

    }



}
