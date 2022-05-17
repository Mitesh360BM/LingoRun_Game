using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainPanelScript : MonoBehaviour
{
    [Header("External GameObkjects")]
    public GameObject EnterOTPScreen;
    public GameObject UserDetailScreen;

    [Header("Internal GameObjects")]
    public GameObject LoginButton;
    public GameObject UserButton;




    private void OnEnable()
    {
        if (PlayerPrefs.GetString("islogin")=="true")
        {
            UserButton.SetActive(true);
            LoginButton.SetActive(false);


        }
        else
        {
            LoginButton.SetActive(true);
            UserButton.SetActive(false);
        }


    }


    public void OnClick_Login()
    {
        EnterOTPScreen.SetActive(true);
        EnterOTPScreen.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0),0.5f);
        this.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(2260, 0), 0.5f).OnComplete(() =>
         {
             this.gameObject.SetActive(false);
         });
    }

    public void OnClick_UserDetails()
    {
        UserDetailScreen.SetActive(true);
        UserDetailScreen.GetComponent<RectTransform>().DOAnchorPos(new Vector2(02260, 0), 0.0f);
        UserDetailScreen.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f);
        this.gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-2260, 0), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });

    }

}
