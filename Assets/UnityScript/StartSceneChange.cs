using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneChange : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        // change to GameMainScene
        if(Input.GetKey(KeyCode.Space)){
            SceneManager.LoadScene("GameScene");
        }
    }
}
