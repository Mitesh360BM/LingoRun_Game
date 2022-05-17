using UnityEngine;
using UnityEngine.UI;

public class MagnetPower : MonoBehaviour
{
    [Header("Magnet Settings")]
    
    public float magnettime;
    public float magnetTimeCountdown;
    public static bool isMagnetActive { get; set; }
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
        else if (PowerUPController.instance.canUsePower && !isMagnetActive)
        {

            blockPanel.SetActive(false);
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }
    private void checkCount()
    {
        if (PlayerDataController.instance.magnetCount == 0)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            blockPanel.SetActive(true);
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            blockPanel.SetActive(false);

        }
        powerCountText.text = PlayerDataController.instance.magnetCount.ToString();
    }

    private void OnClick_Stop()
    {
        isMagnetActive = false;
        panel.SetActive(isMagnetActive);
        powerCountPanel.SetActive(!isMagnetActive);
        EventController.instance.magnetEvent_fn(isMagnetActive);
        CancelInvoke("DisplayMagnetTime");
        PowerUPController.instance.setMinPowerInUse();
        if (UiIns != null)
            Destroy(UiIns);
        checkCount();
    }


    public void OnClick_Start()
    {
        if (PlayerDataController.instance.magnetCount > 0)
        {
            PlayerDataController.instance.magnetCount -= 1;
            powerCountText.text = PlayerDataController.instance.magnetCount.ToString();
            PlayerDataController.instance.SetPowerData(Save.magnetPower, PlayerDataController.instance.magnetCount);
            UseByController();
            PlayerDataController.instance.UsePowerUP(Save.magnetPower);

        }
    }

    public void UseByController()
    {
        if (!isMagnetActive)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            isMagnetActive = true;
            powerCountPanel.SetActive(!isMagnetActive);
            panel.SetActive(isMagnetActive);
            EventController.instance.magnetEvent_fn(isMagnetActive);
            magnetTimeCountdown = magnettime;
            InvokeRepeating("DisplayMagnetTime", 0f, 1f);
            InstantiatePrefab(magnetTimeCountdown) ;
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
              //  magneticTimeText.text = "00:" + magnetTimeCountdown.ToString();
            }
        }
        else
        {
            magnetTimeCountdown = 0;
            OnClick_Stop();
        }
    }

}
