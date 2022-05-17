using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikePower : MonoBehaviour
{

    [Header("Bike Settings")]

    public float bikeTime;
    public float magnetTimeCountdown;
    public static bool isBikeActive;
    public Text magneticTimeText;
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
        if (!PowerUPController.instance.canUsePower && !FlyingPower.isFlyingActive && !SkatePower.isSkateActive && !HulkPower.ishulkActive)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (PowerUPController.instance.canUsePower && !isBikeActive && !FlyingPower.isFlyingActive && !SkatePower.isSkateActive && !HulkPower.ishulkActive)
        {

            blockPanel.SetActive(false);
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else if (FlyingPower.isFlyingActive || SkatePower.isSkateActive || HulkPower.ishulkActive)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }

    }

    private void Start()
    {
        EventController.instance.explosionEvent += TrapsScript_explosionEvent;
        countCheck();
    }
    private void OnDisable()
    {
        EventController.instance.explosionEvent -= TrapsScript_explosionEvent;
    }
    private void TrapsScript_explosionEvent()
    {
        OnClick_Stop();
    }

    private void countCheck()
    {
        if (PlayerDataController.instance.bikeCount == 0)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            blockPanel.SetActive(true);
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            blockPanel.SetActive(false);
        }
        powerCountText.text = PlayerDataController.instance.bikeCount.ToString();
    }

    public void UseByController()
    {
        if (!isBikeActive)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            isBikeActive = true;
            Debug.Log("isBike" + isBikeActive);
            powerCountPanel.SetActive(!isBikeActive);
            panel.SetActive(isBikeActive);
            EventController.instance.bikeEvent_fn(true);
            magnetTimeCountdown = bikeTime;
            InvokeRepeating("DisplayMagnetTime", 0f, 1f);
            InstantiatePrefab(magnetTimeCountdown);
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
        if (PlayerDataController.instance.bikeCount > 0)
        {
            PlayerDataController.instance.bikeCount -= 1;
            powerCountText.text = PlayerDataController.instance.bikeCount.ToString();
            PlayerDataController.instance.SetPowerData(Save.bikerPower, PlayerDataController.instance.bikeCount);
            UseByController();
            PlayerDataController.instance.UsePowerUP(Save.bikerPower);
        }

    }

    private void OnClick_Stop()
    {
        countCheck();
        magnetTimeCountdown = 0;
        isBikeActive = false;
        powerCountPanel.SetActive(!isBikeActive);
        panel.SetActive(isBikeActive);
        EventController.instance.bikeEvent_fn(isBikeActive);
        CancelInvoke("DisplayMagnetTime");
        PowerUPController.instance.setMinPowerInUse();
        if (UiIns != null)
            Destroy(UiIns);

    }

    void DisplayMagnetTime()
    {
        if (!GameController.instance.isPlayerDead)
        {
            magnetTimeCountdown -= 1;
            if (magnetTimeCountdown < 0)
            {
                OnClick_Stop();
            }
            else
            {
                // magneticTimeText.text = "00:" + magnetTimeCountdown.ToString();
            }
        }
        else
        {
            OnClick_Stop();
        }
    }
}
