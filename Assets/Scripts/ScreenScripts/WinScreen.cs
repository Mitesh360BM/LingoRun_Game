using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject WonRotate,levelTextPrefab;

    public Text newLevelText,oldLevelText;

    private void OnEnable()
    {
        
        // StartCoroutine(disableScreen());
       // WonRotate.SetActive(true);
    }

    public void disableScreen()
    {
       

        WonRotate.SetActive(false);
        this.gameObject.SetActive(false);

    }


    public void changeLevelNumber()
    {
        PlayerDataController.instance.LevelCount += 1;
        newLevelText.text = PlayerDataController.instance.LevelCount.ToString();
        PlayerLifeInstance.instance.life_Current = 3;
        StartCoroutine(APIController.instance.UpdateLevel((status)=>{


            if (status != null && !string.IsNullOrEmpty(status))
            {

                Debug.Log("Level Upated to: " + status + " : on Server");
                QuizController.instance.startFetchQuestion();
            }
            else {
                Debug.Log("Level Not Updated");

            }
        
        
        }));


    }

    public void disableOldLevelText()
    { 
        oldLevelText.gameObject.SetActive(false);
        
    }
    public void enableoldText()
    {
        oldLevelText.text = PlayerDataController.instance.LevelCount.ToString();
        oldLevelText.gameObject.SetActive(true);


    }
    public void instantiateNumber()
    {
        //Transform trans = GameObject.Find("GameCanvas").transform;
        //GameObject go = Instantiate(levelTextPrefab,trans);
    
    }

    public void OnClick_Resume()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);

    }
    public void OnClick_Leave()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }
}
