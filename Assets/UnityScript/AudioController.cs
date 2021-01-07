using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource myAudio_BoltAction = null;
    [SerializeField] AudioSource myAudio_BoltAction2 = null;
    [SerializeField] AudioSource myAudio_FireSound = null;
    public void BoltActionSoundPlay(){
        myAudio_BoltAction.Play();
    }
    public void BoltActionSound2Play(){
        myAudio_BoltAction2.Play();
    }
    public void FireSoundPlay(){
        myAudio_FireSound.Play();
    }
}
