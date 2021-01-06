using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmDisplayController : MonoBehaviour
{
    [SerializeField] GameObject AmmDisplayText = null;
    [SerializeField] GameObject AmmDisplayImage = null;
    Text AmmText;
    Image AmmImage;
    // Start is called before the first frame update
    void Start()
    {
        AmmText = AmmDisplayText.GetComponent<Text>();
        AmmImage = AmmDisplayImage.GetComponent<Image>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            AmmImageInvisible();
        }
        if(Input.GetKeyDown(KeyCode.X)){
            AmmImageSee();
        }
        
    }
    
    public void ChangeAmmValue(int PlayerAmm){
        AmmText.text = PlayerAmm.ToString();
    }
    public void AmmImageInvisible(){
        // AmmImage.enabled = false;
        AmmImage.color = new Color(0,0,0,0.2F);
    }
    public void AmmImageSee(){
        // AmmImage.enabled = true;
        AmmImage.color = new Color(0,0,0,1f);

    }
}
