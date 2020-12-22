using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerColliderCtl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        
        Debug.Log(other.gameObject.tag);
        this.GetComponent<DeerController>();
    }
    
}
