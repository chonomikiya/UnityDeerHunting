using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void Update () {
        // if(SceneManager.GetActiveScene().name == "StartScene" && Input.GetKey(KeyCode.Space)){
        //     SceneManager.LoadScene("GameScene");
        // }
        
        // if(SceneManager.GetActiveScene().name == "ResultScene" && Input.GetKey(KeyCode.Space)){
        //     SceneManager.LoadScene("StartScene");
        // }
        if(Input.GetKeyDown(KeyCode.Return)){
            GameSceneChange();
        }
    }
    public void StartSceneChange(){
        SceneManager.LoadScene("GameScene");
    }
    public void GameSceneChange(){
        SceneManager.LoadScene("ResultScene");
    }
    public void ResultSceneChange(){
        SceneManager.LoadScene("StartScene");
    }
}
