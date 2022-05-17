using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMusicGameMenu : MonoBehaviour
{
    [Header("UI Component")]
    public Button musicButton;
    public Button musicButton_;
    [Header("UI Resource")]
    public Sprite muteSprite;
    public Sprite un_muteSprite;

    [Header("Music")]
    public AudioSource audioSource;
    public AudioClip menuMusic;   
    public bool isMute;

    public GameObject loadPanel;
    




   

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("mute")))
        {

            isMute = false;
            PlayerPrefs.SetString("mute", isMute.ToString());
        }
        else
        {

            isMute = Convert.ToBoolean(PlayerPrefs.GetString("mute"));
            if (isMute)
            {
                audioSource.mute = isMute;
                musicButton.image.sprite = muteSprite;
                musicButton_.image.sprite = muteSprite;
            }
            else
            {
                audioSource.mute = isMute;
                musicButton.image.sprite = un_muteSprite;
                musicButton_.image.sprite = un_muteSprite;
            }

        }
        StartCoroutine(startMusic());
        StartCoroutine(stopMusic());
        // audioSource.clip = menuMusic;
        //// audioSource.time = 65.05f;
        // audioSource.Play();
    }

    public void Mute()
    {
        if (!isMute)
        {
            isMute = true;
            audioSource.mute = isMute;
            musicButton.image.sprite = muteSprite;
            musicButton_.image.sprite = muteSprite;
            //  GameInstance.instance.Mute();
            PlayerPrefs.SetString("mute",isMute.ToString());
        }
       else if (isMute)
        {
            isMute = false;
            audioSource.mute = isMute;
            musicButton.image.sprite = un_muteSprite;
            musicButton_.image.sprite = un_muteSprite;
            //  GameInstance.instance.UnMute();
            PlayerPrefs.SetString("mute", isMute.ToString());
        }
    
    
    }

    IEnumerator startMusic()
    {

        yield return new WaitUntil(() => !loadPanel.activeInHierarchy);
        audioSource.clip = menuMusic;
        audioSource.Play();
        StartCoroutine(stopMusic());
    
    }
    IEnumerator stopMusic()
    {

        yield return new WaitUntil(() => loadPanel.activeInHierarchy);
        audioSource.clip = menuMusic;
        audioSource.Pause();
        StartCoroutine(startMusic());
    }


}
