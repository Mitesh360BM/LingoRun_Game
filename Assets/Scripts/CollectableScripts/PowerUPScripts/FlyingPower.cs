using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingPower : MonoBehaviour
{


    [Header("Magnet Settings")]
   
    public float flyingTime;
    public float flyingTimeCountdown;
    public static bool isFlyingActive;
    public Text flyingTimeText;
    public GameObject panel;
    public GameObject blockPanel;
    [Header("PowerCount")]
    public GameObject powerCountPanel;
    public Text powerCountText;
    [Header("PowerUIContent Object")]
    public GameObject contentParent;
    public GameObject PowerUiPrefab;
    public Image powerSprite;

    private void OnEnable()
    {
        countCheck();
    }
    private void Update()
    {
        if (!PowerUPController.instance.canUsePower  && !QuizController.instance.isQuestionVisible && !BikePower.isBikeActive && !SkatePower.isSkateActive && !HulkPower.ishulkActive)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (PowerUPController.instance.canUsePower && !isFlyingActive && !QuizController.instance.isQuestionVisible && !BikePower.isBikeActive && !SkatePower.isSkateActive && !HulkPower.ishulkActive)
        {

            blockPanel.SetActive(false);
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else if(QuizController.instance.isQuestionVisible)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if(BikePower.isBikeActive || SkatePower.isSkateActive || HulkPower.ishulkActive)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;

        }
    }
    private void countCheck()
    {
        if (PlayerDataController.instance.flyingCount == 0)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            blockPanel.SetActive(true);
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            blockPanel.SetActive(false);
        }
        powerCountText.text = PlayerDataController.instance.flyingCount.ToString();
    }

    public void UseByController()
    {
        if (!isFlyingActive)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            isFlyingActive = true;
            //   gameObject.SetActive(true);
            panel.SetActive(isFlyingActive);
            EventController.instance.flyingEvent_fn(isFlyingActive);
            flyingTimeCountdown = flyingTime;
            InvokeRepeating("DisplayMagnetTime", 0f, 1f);
            InstantiatePrefab(flyingTimeCountdown);
            PowerUPController.instance.setMaxPowerInUse();
        }


    }
    //Based on Ui Changes
    private GameObject UiIns;
    private void InstantiatePrefab(float time)
    {
        GameObject go = Instantiate(PowerUiPrefab, contentParent.transform);
        go.name = this.name;
        UiIns = go;
        go.GetComponent<PowerUIinstance>().powerSprite.sprite = powerSprite.sprite;
        go.GetComponent<PowerUIinstance>().powerTime = time;
        go.GetComponent<PowerUIinstance>().testMode = false;
        go.SetActive(true);

    }



    public void OnClick_Start()
    {

        if (PlayerDataController.instance.flyingCount > 0)
        {
            PlayerDataController.instance.flyingCount -= 1;
            powerCountText.text = PlayerDataController.instance.flyingCount.ToString();
            PlayerDataController.instance.SetPowerData(Save.flyingPower, PlayerDataController.instance.flyingCount);

            UseByController();
            PlayerDataController.instance.UsePowerUP(Save.flyingPower);
        }
    }

    private void OnClick_Stop()
    {
        countCheck();
        flyingTimeCountdown = 0;
        isFlyingActive = false;
        powerCountPanel.SetActive(!isFlyingActive);
        // gameObject.SetActive(false);
        panel.SetActive(isFlyingActive);
        EventController.instance.flyingEvent_fn(isFlyingActive);
        CancelInvoke("DisplayMagnetTime");
        PowerUPController.instance.setMinPowerInUse();
        if (UiIns != null)
            Destroy(UiIns);
    }

    void DisplayMagnetTime()
    {
        if (!GameController.instance.isPlayerDead /*&& QuizController.instance.isQuestionVisible == false*/)
        {
            flyingTimeCountdown -= 1;
            if (flyingTimeCountdown < 0)
            {
                OnClick_Stop();
            }
            else
            {

               // flyingTimeText.text = "00:" + flyingTimeCountdown.ToString();


            }
        }

        else
        {




            OnClick_Stop();
        }
       
    }
   
}
