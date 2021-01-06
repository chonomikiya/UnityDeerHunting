using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] GameObject mySceneCtl = null;
    [SerializeField] GameObject OperationViewPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextScene(){
        mySceneCtl.GetComponent<SceneChange>().StartSceneChange();
    }
    public void titleSceneChange(){
        IsDeadJudgment.DeerIsAlive();
        mySceneCtl.GetComponent<SceneChange>().ResultSceneChange();
    }
    public void OnOperationView(){
        GameObject OperationView = Instantiate(OperationViewPrefab) as GameObject;
    }
    public void CloseOperationView(){
        Destroy(this.transform.root.gameObject);
    }
    public void NextSceneForGiveUp(){
        mySceneCtl.GetComponent<SceneChange>().GameSceneChange();
    }
}
