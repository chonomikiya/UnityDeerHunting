using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerColliderCtl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        // if(other.gameObject.tag == "Player"){
        //     Debug.Log(other.gameObject.tag);
        //     this.GetComponent<DeerController>().Animation_LookAroundLeft();
        // }
        // Debug.Log(other.collision);
    }
}
