using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource myAudio_BoltAction = null;
    [SerializeField] AudioSource myAudio_BoltAction2 = null;
    [SerializeField] AudioSource myAudio_FireSound = null;
    [SerializeField] AudioSource myAudio_DeerMewling01 = null;
    [SerializeField] AudioSource myAudio_DeerMewling02 = null;
    [SerializeField] AudioSource myAudio_DeerMewling03 = null;
    [SerializeField] AudioSource myAudio_Walking = null;
    bool walkingSound = false;
    double FadeDeltaTime = 0;
    double FadeOutSeconds = 0.5;
    public void BoltActionSoundPlay(){
        myAudio_BoltAction.Play();
    }
    public void BoltActionSound2Play(){
        myAudio_BoltAction2.Play();
    }
    public void FireSoundPlay(){
        myAudio_FireSound.Play();
    }
    public void DeerMewling01Play(){
        Debug.Log("Mewling");
        myAudio_DeerMewling01.Play();
    }
    public void DeerMewling02Play(){
        myAudio_DeerMewling02.Play();
    }
    public void DeerMewling03Play(){
        myAudio_DeerMewling03.Play();
    }
    public void WalkingSoundPlay(){
        if(!myAudio_Walking.isPlaying){
            myAudio_Walking.volume = 1.0F;
            myAudio_Walking.Play();
        }
    }
    public void WalkingSoundStop(){
        if(myAudio_Walking.isPlaying && !walkingSound){
            // myAudio_Walking.Stop();
            walkingSound = true;
            FadeDeltaTime = 0;
        }
    }
    public void WalkSoundPitch_High(){
        myAudio_Walking.pitch = 1.5F;
    }
    public void walkSoundPitch_neutral(){
        myAudio_Walking.pitch = 1.0F;
    }
    private void Update() {
        if(walkingSound){
            FadeDeltaTime += Time.deltaTime;
            if(FadeDeltaTime >= FadeOutSeconds){
                FadeDeltaTime = FadeOutSeconds;
                walkingSound = false;
                
                myAudio_Walking.Stop();
            }
            myAudio_Walking.volume = (float)(1.0 - FadeDeltaTime/FadeOutSeconds);
        }
    }
}
