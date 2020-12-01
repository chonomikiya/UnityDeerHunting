using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneChange : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        // change to StartScene
        if(Input.GetKey(KeyCode.Space)){
            SceneManager.LoadScene("StartScene");
        }
    }
}
