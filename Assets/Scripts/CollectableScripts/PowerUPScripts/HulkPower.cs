using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HulkPower : MonoBehaviour
{


    [Header("Magnet Settings")]
   
    public float hulkTime;
    public float hulkTimeCountdown;
    public static bool ishulkActive;
    public Text hulkTimeText;
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
        if (!PowerUPController.instance.canUsePower && !FlyingPower.isFlyingActive && !BikePower.isBikeActive && !SkatePower.isSkateActive)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (PowerUPController.instance.canUsePower && !ishulkActive && !FlyingPower.isFlyingActive && !BikePower.isBikeActive && !SkatePower.isSkateActive)
        {

            blockPanel.SetActive(false);
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else if (FlyingPower.isFlyingActive || BikePower.isBikeActive || SkatePower.isSkateActive)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    private void countCheck()
    {
        if (PlayerDataController.instance.hulkCount == 0)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            blockPanel.SetActive(false);
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        powerCountText.text = PlayerDataController.instance.hulkCount.ToString();
    }

    public void OnClick_Start()
    {
        if (PlayerDataController.instance.hulkCount > 0)
        {
            PlayerDataController.instance.hulkCount -= 1;
            powerCountText.text = PlayerDataController.instance.hulkCount.ToString();
            PlayerDataController.instance.SetPowerData(Save.hulkPower, PlayerDataController.instance.hulkCount);
            UseByController();
            PlayerDataController.instance.UsePowerUP(Save.hulkPower);
        }

    }

    private void OnClick_Stop()
    {
        countCheck();
        hulkTimeCountdown = 0;
        ishulkActive = false;
        powerCountPanel.SetActive(!ishulkActive);
        panel.SetActive(ishulkActive);
        EventController.instance.hulkEvent_fn(ishulkActive);
        CancelInvoke("DisplayMagnetTime");
        PowerUPController.instance.setMinPowerInUse();


    }
    public void UseByController()
    {
        if (!ishulkActive)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            ishulkActive = true;
            powerCountPanel.SetActive(!ishulkActive);
            panel.SetActive(ishulkActive);
            EventController.instance.hulkEvent_fn(ishulkActive);
            hulkTimeCountdown = hulkTime;
            InvokeRepeating("DisplayMagnetTime", 0f, 1f);
            InstantiatePrefab(hulkTimeCountdown);
            PowerUPController.instance.setMaxPowerInUse();
        }

    }

    //Based on Ui Changes
    private void InstantiatePrefab(float time)
    {
        GameObject go = Instantiate(PowerUiPrefab, contentParent.transform);
        go.name = this.name;
        go.GetComponent<PowerUIinstance>().powerSprite.sprite = powerSprite.sprite;
        go.GetComponent<PowerUIinstance>().powerTime = time;
        go.GetComponent<PowerUIinstance>().testMode = false;
        go.SetActive(true);

    }

    void DisplayMagnetTime()
    {
        if (!GameController.instance.isPlayerDead)
        {
            hulkTimeCountdown -= 1;
            if (hulkTimeCountdown < 0)
            {
                OnClick_Stop();
            }
            else
            {
              //  hulkTimeText.text = "00:" + hulkTimeCountdown.ToString();

            }
        }

        else
        {
            hulkTimeCountdown = 0;
            OnClick_Stop();
        }
    }
}
