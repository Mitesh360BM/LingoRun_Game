using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotionPower : MonoBehaviour
{
    [Header("Magnet Settings")]
    public float slowMotime;
    public float slowMoCountdown;
    public static bool isSloMoActive { get; set; }
    public Text sloMoTimeText;
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
        checkCount();
    }
    private void Start()
    {
        checkCount();
    }

    private void Update()
    {
        if (!PowerUPController.instance.canUsePower)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (PowerUPController.instance.canUsePower && !isSloMoActive)
        {

            blockPanel.SetActive(false);
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }
    private void checkCount()
    {
        if (PlayerDataController.instance.slowMoCount == 0)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            blockPanel.SetActive(false);
            powerCountText.text = PlayerDataController.instance.slowMoCount.ToString();
        }
    }
    private void OnClick_Stop()
    {
        Time.timeScale = 1f;
        isSloMoActive = false;
        panel.SetActive(isSloMoActive);
        powerCountPanel.SetActive(!isSloMoActive);
        EventController.instance.slowMoEvent_fn(isSloMoActive);
        CancelInvoke("DisplaySloMoTime");
        PowerUPController.instance.setMinPowerInUse();
        if (PlayerDataController.instance.slowMoCount == 0)
        {
            blockPanel.SetActive(true);
        }
        powerCountText.text = PlayerDataController.instance.slowMoCount.ToString();
    }


    public void OnClick_Start()
    {
        if (PlayerDataController.instance.slowMoCount > 0)
        {
           // Time.timeScale = 0.5f;
            PlayerDataController.instance.slowMoCount -= 1;
            powerCountText.text = PlayerDataController.instance.slowMoCount.ToString();
            PlayerDataController.instance.SetPowerData(Save.sloMoPower, PlayerDataController.instance.slowMoCount);
            UseByController();
            PlayerDataController.instance.UsePowerUP(Save.sloMoPower);

        }
    }

    public void UseByController()
    {
        if (!isSloMoActive)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            Time.timeScale = 0.5f;
            isSloMoActive = true;
            panel.SetActive(isSloMoActive);
            powerCountPanel.SetActive(!isSloMoActive);
            EventController.instance.slowMoEvent_fn(isSloMoActive);
            slowMoCountdown = slowMotime;
            InvokeRepeating("DisplaySloMoTime", 0f,1f);
            InstantiatePrefab(slowMoCountdown);
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
    void DisplaySloMoTime()
    {
        if (!GameController.instance.isPlayerDead)
        {
            slowMoCountdown -= 1;
            if (slowMoCountdown < 0)
            {
                OnClick_Stop();

            }
            else
            {
                //sloMoTimeText.text = "00:" + slowMoCountdown.ToString();
            }
        }
        else
        {
            slowMoCountdown = 0;
            OnClick_Stop();
        }
    }

}
