using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingCtl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeleteObject",30F);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void DeleteObject(){

        Destroy(this.gameObject);
    }
}
