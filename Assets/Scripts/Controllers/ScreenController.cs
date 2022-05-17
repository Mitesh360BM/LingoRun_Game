using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour
{
    public static ScreenController instance;
    public bool ScreenControllerReady { get; set; }

    [Header("Game Screens")]
    public GameObject loadingPanel;
    public GameObject ChooseAvatarScreen;
    public GameObject PowerPurchaseScreen;
    public GameObject GamePersistantScreen;
    public GameObject HomeScreen;
    public GameObject GamePlayScreen;
    public GameObject ResumeScreen;
    public GameObject PauseScreen;
    public GameObject RetryPanel;
    public GameObject NoLifePanel;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject PowerPurchaseAtStart;
    public GameObject touchBlocker;
    public GameObject confettiPanel;
    public GameObject musicOnBT;
    public GameObject musicOffBT;

    [Header("Player Data UI Component")]
    public Text leveltext;
    public Text CoinsText;
    public Text totalScoreText;
    public Text DistanceText;

    [Header("UI Component")]
    public Text StartDelayText;
    public Text fpsDisplay;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InitialLoading();
    }
    public void InitialLoading()
    {
        if (!loadingPanel.activeSelf)
        {
            loadingPanel.SetActive(true);
        }
    }


    public void OnClick_ShowScreenGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);

    }
    public void OnClick_HideScreenGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);

    }
    public void AppicalionExit()
    {

        Application.Quit();

    }
    public void setUserInterface()
    {
      totalScoreText.text = PlayerDataController.instance.TotalScore.ToString();



    }

}
