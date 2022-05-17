using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMusicMainMenu : MonoBehaviour
{


    [Header("Music")]
    public AudioSource audioSource;
    public AudioClip menuMusic;
   
    public bool isMute;
   




    private void Awake()
    {
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("mute")))
        {

            
            isMute = false;
        
        }
        else
        {

            isMute = Convert.ToBoolean(PlayerPrefs.GetString("mute"));
            if (isMute)
            {
                audioSource.mute = isMute;
            }
            else
            {
                audioSource.mute = isMute;
            }
            
          }
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    public void Mute()
    {
        if (!isMute)
        {
            isMute = true;
            audioSource.mute = isMute;
            PlayerPrefs.SetString("mute",isMute.ToString());
        }
       else if (isMute)
        {
            isMute = false;
            audioSource.mute = isMute;
            PlayerPrefs.SetString("mute", isMute.ToString());
        }
    
    
    }




}
