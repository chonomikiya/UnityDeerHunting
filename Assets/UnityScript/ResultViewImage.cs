using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultViewImage : MonoBehaviour
{
    [SerializeField] GameObject HuntedImagePrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        if(IsDeadJudgment.GetDeerState()){
            GameObject HuntedImage = Instantiate(HuntedImagePrefab) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
