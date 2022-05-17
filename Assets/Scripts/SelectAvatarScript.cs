using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAvatarScript : MonoBehaviour
{
    public GameObject BoyObject;
    public GameObject GirlObject;

    public GameObject homescreen,gamePersistant;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("avatar"))
        {
            if (getAvatar() == "boy")
            {
                selectBoy();
            }
            if (getAvatar() == "girl")
            {
                selectGirl();
            }
            StartCoroutine(disable());
            
        }
    }
     IEnumerator disable()
    {
        yield return new WaitForSeconds(0.5f);
        homescreen.SetActive(true);
        gamePersistant.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private void setAvatar(string name)
    {
        PlayerPrefs.SetString("avatar", name);
    }

    private string getAvatar()
    {
        return PlayerPrefs.GetString("avatar");
    }


    public void selectBoy()
    {
        BoyObject.SetActive(true);
        GirlObject.SetActive(false);
        setAvatar("boy");
    }

    public void selectGirl()
    {
        BoyObject.SetActive(false);
        GirlObject.SetActive(true);
        setAvatar("girl");
    }



}
