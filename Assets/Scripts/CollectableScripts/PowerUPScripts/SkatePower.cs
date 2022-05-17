using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkatePower : MonoBehaviour
{
    [Header("Bike Settings")]
   
    public float skateTime;
    public float skateTimeCountdown;
    public static bool isSkateActive;
    public Text skateTimeText;
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
        if (!PowerUPController.instance.canUsePower && !FlyingPower.isFlyingActive && !BikePower.isBikeActive && !HulkPower.ishulkActive)
        {
            blockPanel.SetActive(true);
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (PowerUPController.instance.canUsePower && !isSkateActive && !FlyingPower.isFlyingActive && !BikePower.isBikeActive && !HulkPower.ishulkActive)
        {

            blockPanel.SetActive(false);
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else if(FlyingPower.isFlyingActive || BikePower.isBikeActive || HulkPower.ishulkActive)
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
    private void OnDestroy()
    {
        EventController.instance.explosionEvent -= TrapsScript_explosionEvent;
    }
    private void TrapsScript_explosionEvent()
    {
        OnClick_Stop();
    }

    private void countCheck()
    {
        if (PlayerDataController.instance.skateCount == 0)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            blockPanel.SetActive(true);
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            blockPanel.SetActive(false);
        }
        powerCountText.text = PlayerDataController.instance.skateCount.ToString();
    }

    public void UseByController()
    {
        if (!isSkateActive)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            isSkateActive = true;
            powerCountPanel.SetActive(!isSkateActive);
            panel.SetActive(isSkateActive);
            EventController.instance.skateEvent_fn(true);
            skateTimeCountdown = skateTime;
            InvokeRepeating("DisplayMagnetTime", 0f, 1f);
            InstantiatePrefab(skateTimeCountdown);
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
        if (PlayerDataController.instance.skateCount > 0)
        {
            PlayerDataController.instance.skateCount -= 1;
            powerCountText.text = PlayerDataController.instance.skateCount.ToString();
            PlayerDataController.instance.SetPowerData(Save.skatePower, PlayerDataController.instance.skateCount);
            UseByController();
            PlayerDataController.instance.UsePowerUP(Save.skatePower);
        }

    }

    private void OnClick_Stop()
    {
        countCheck();
        skateTimeCountdown = 0;
        isSkateActive = false;
        powerCountPanel.SetActive(!isSkateActive);
        panel.SetActive(isSkateActive);
        EventController.instance.skateEvent_fn(isSkateActive);
        CancelInvoke("DisplayMagnetTime");
        PowerUPController.instance.setMinPowerInUse();
        if (UiIns != null)
            Destroy(UiIns);

    }

    void DisplayMagnetTime()
    {
        if (!GameController.instance.isPlayerDead)
        {
            skateTimeCountdown -= 1;
            if (skateTimeCountdown < 0)
            {
                OnClick_Stop();
            }
            else
            {
               // skateTimeText.text = "00:" + skateTimeCountdown.ToString();
            }
        }
        else
        {
            OnClick_Stop();
        }
    }
}
