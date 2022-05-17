using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Text leveltext;

    private void OnEnable()
    {
      
        leveltext.text = PlayerDataController.instance.LevelCount.ToString();
        Time.timeScale = 0.15f;
    }


    public void OnClick_Resume()
    {
        Time.timeScale = 1f;
        QuizController.instance.startFetchQuestion();
        this.gameObject.SetActive(false);

    }
    public void OnClick_Leave()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }

    
}
