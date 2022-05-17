using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCommunication : MonoBehaviour
{
    public Text text;

    public bool isReady;

    public string playerUniqueID;



    private void Awake()
    {
        // playerUniqueID = "ytanWJhc";

        getIntentData();
        if (!getIntentData())
        {
            isReady = true;
        }


    }

    private bool getIntentData()
    {
#if (UNITY_EDITOR)
        playerUniqueID = "OjZSxnSq";
        text.text = playerUniqueID;
        isReady = true;
        return true;
#endif
#if (UNITY_ANDROID && !UNITY_EDITOR)
    return CreatePushClass (new AndroidJavaClass ("com.unity3d.player.UnityPlayer"));
#endif


       // return false;

    }

    public bool CreatePushClass(AndroidJavaClass UnityPlayer)
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
        AndroidJavaObject extras = GetExtras(intent);

        if (extras != null)
        {
            playerUniqueID = GetProperty(extras, "data");

            isReady = true;
            text.text =  playerUniqueID;
            return true;
        }
        //else
        //{
        //    playerUniqueID = "RFVAjA1e";
        //    isReady = true;
        //    return true;
        //}
#endif

        //default id
        //playerUniqueID = "F4LCNtHN";

        return false;
    }




    private AndroidJavaObject GetExtras(AndroidJavaObject intent)
    {
        AndroidJavaObject extras = null;

        try
        {
            extras = intent.Call<AndroidJavaObject>("getExtras");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        return extras;
    }

    private string GetProperty(AndroidJavaObject extras, string name)
    {
        string s = string.Empty;

        try
        {
            s = extras.Call<string>("getString", name);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        return s;
    }



}
