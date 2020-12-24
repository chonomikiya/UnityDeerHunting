using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textUI_Ctl : MonoBehaviour
{
    //　カメラ内にいるかどうか
    private bool isInsideCamera;
    //　経過時間表示テキスト
    [SerializeField] private GameObject TextObj = null;
    [SerializeField] private GameObject DeerPosition = null;
    [SerializeField] private GameObject DeerObj = null;
    private DeerController m_DeerCtl = null;
    [SerializeField] private GameObject m_sChange = null;
    
    Text UItext01,UItext02; 
    private RectTransform myRectTfm;
    bool DeerOnRange = false;
    bool textDisplayflag = false;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Deer"){
            changetextUI_See();
            DeerOnRange =true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Deer"){
            changetextUIInvisible();
            DeerOnRange =false;
        }
    }
    void Start(){
        UItext01 = TextObj.GetComponent<Text>();
        UItext02 = TextObj.transform.GetChild(0).GetComponent<Text>();
        myRectTfm = TextObj.GetComponent<RectTransform>();
        m_DeerCtl = DeerObj.GetComponent<DeerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if(DeerOnRange && m_DeerCtl.getDeer_isDie()){
            changetextUI_See();
        }else if(textDisplayflag) {
            changetextUIInvisible();
        }
        if(textDisplayflag && Input.GetKeyDown(KeyCode.F)){
            m_sChange.GetComponent<SceneChange>().GameSceneChange();
        }
        myRectTfm.position 
        = RectTransformUtility.WorldToScreenPoint(Camera.main, DeerPosition.transform.position);
    }
    //　カメラから外れた
    void changetextUIInvisible(){
        UItext01.enabled = false;
        UItext02.enabled = false;
        textDisplayflag = false;
    }
    void changetextUI_See(){
        UItext01.enabled = true;
        UItext02.enabled = true;
        textDisplayflag = true;
    }
}
