using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    Rigidbody m_rigidbody = null;
    [SerializeField] float initial_velocity = 0;
    Vector3 speed;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.velocity = transform.forward * initial_velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
