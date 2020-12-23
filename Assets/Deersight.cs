using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deersight : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            Debug.Log(other.name);
            this.GetComponent<DeerController>().Animation_LookAroundLeft();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
