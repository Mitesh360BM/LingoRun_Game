using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{

    public static PlayerAudioManager instance;


    public AudioSource audioSource;

    public AudioClip coinCollect;
    public AudioClip CollideSFX, CollideMusic;

    public AudioClip slide;
    public AudioClip Jump;

 
    public AudioClip tick,tock;

    

    private void Awake()
    {
        instance = this;
    }


    public void PlayerAudio(AudioClip audioClip, bool isLoop)
    {
        audioSource.clip = audioClip;
        if (audioClip != null)
        {
            audioSource.Play();
            audioSource.loop = isLoop;
        }
        else
        {
            audioSource.Stop();
            audioSource.loop = isLoop;

        }

    }




}
