using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLogger{

    public class logger : MonoBehaviour
    {
        public void DownEnter(string tmp){
            if(Input.GetKeyDown(KeyCode.Return)){
                Debug.Log(tmp);
            }
        }
    }
}