using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeController : MonoBehaviour
{
    Rigidbody m_rigidbody;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float floatoffset;
    [SerializeField] float upoffset;
    [SerializeField] Vector3 setAngularVelo = new Vector3(0,50,0);

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.maxAngularVelocity = Mathf.Infinity;
        Cardridge_throw();
        Invoke("DestroyCartridge", 5F);
    }
    void DestroyCartridge(){
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            Cardridge_throw();
        }
    }
    void Cardridge_throw(){
        m_rigidbody.AddRelativeForce(this.transform.right *-1 * floatoffset); 
        m_rigidbody.AddForce(Vector3.up * floatoffset);
        m_rigidbody.angularVelocity = setAngularVelo;
        m_rigidbody.useGravity =true;
    }
}
