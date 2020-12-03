using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void Update () {
        
        if(SceneManager.GetActiveScene().name == "StartScene" && Input.GetKey(KeyCode.Space)){
            SceneManager.LoadScene("GameScene");
        }
        
        if(SceneManager.GetActiveScene().name == "ResultScene" && Input.GetKey(KeyCode.Space)){
            SceneManager.LoadScene("StartScene");
        }
    }
}
