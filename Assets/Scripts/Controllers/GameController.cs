using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool GameInstanceReady { get; set; }

    public AudioSource audioSource;
    public bool isMuted;

    public GameObject brokenHeartImage { get; set; }
    [Header("Game Settings")]
    public float StartDelay;

    [Header("Player State")]
    public bool isPressedStart;
    public bool isGameStart;
    public bool isPlayerDead;
    public bool isContinueGame;
    public bool isWon;

    //[Header("Tutorial")]
    //public bool isTutorialDone;
    //public bool tutClickPlay;
    //public bool tutSwipeRight;
    //public bool tutSwipeLeft;
    //public bool tutSwipeUp;
    //public GameObject mainPanelTutorial;
    //public GameObject inGameTurial;
    //public GameObject swipeRighttxt;
    //public GameObject swipeLefttxt;
    //public GameObject swipeUptxt;




    private void Awake()
    {
        DOTween.Clear(true);

        if (instance != this)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);

        }

        Application.runInBackground = true;

        if (PlayerPrefs.HasKey("sound"))
        {
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                MuteAudio(true);

            }
            else if (PlayerPrefs.GetInt("sound") == 0)
            {
                MuteAudio(false);
            }
        }
    }

    private void gc()
    {

        System.GC.Collect();

    }

    private void Start()
    {
        Camera.main.nearClipPlane = 3f;
        Application.targetFrameRate = 45;
        EventController.instance.playerDeadEvent += playergameOverEvent_fn;
        EventController.instance.playerLifeEvent += Instance_playerLifeEvent;
        Invoke("gc", 10f);

    }

    private void Instance_playerLifeEvent()
    {
      
        StartCoroutine(checkForLife());
    }

    IEnumerator checkForLife()
    {
        yield return new WaitForSeconds(1f);
        if (PlayerLifeInstance.instance.life_Current != 0)
        {
            // OnClick_ContinueGame();

        }
        else
        {
            PlayerDataController.instance.SendScoreToServer();
            ScreenController.instance.touchBlocker.SetActive(false);
            isPlayerDead = true;
            ScreenController.instance.NoLifePanel.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventController.instance.playerDeadEvent -= playergameOverEvent_fn;
        EventController.instance.playerLifeEvent -= Instance_playerLifeEvent;
    }

    public void customeStart()
    {

    }

    public void OnClick_Play()
    {


        if (PlayerLifeInstance.instance.life_Current != 0)
        {

            isPlayerDead = false;
            isPressedStart = true;
            Save.instance.SetString("LifeUpdateTime", DateTime.Now.ToString());
            StartCoroutine(GameStartDelayRoutine());
        }
        else
        {
            Debug.Log("Not Enough Life");
            ScreenController.instance.NoLifePanel.SetActive(true);
        }
    }
    private IEnumerator GameStartDelayRoutine()
    {
        ScreenController.instance.HomeScreen.SetActive(false);
        ScreenController.instance.GamePlayScreen.SetActive(true);
        ScreenController.instance.StartDelayText.gameObject.SetActive(true);
        ScreenController.instance.StartDelayText.text = "";
        yield return new WaitForSeconds(2f);

        while (StartDelay > 1)
        {
            yield return new WaitForSeconds(1f);

            PlayerAudioManager.instance.PlayerAudio(PlayerAudioManager.instance.tick, false);
            StartDelay -= 1;
            if (StartDelay == 3)
            {
                ScreenController.instance.StartDelayText.color = Color.cyan;


            }
            if (StartDelay == 2)
            {
                ScreenController.instance.StartDelayText.color = new Color(1f, 0.6f, 0);

            }
            if (StartDelay == 1)
            {

                ScreenController.instance.StartDelayText.color = new Color(1f, 0.3f, 0);
            }
            ScreenController.instance.StartDelayText.text = StartDelay.ToString();


        }
        yield return new WaitForSeconds(1f);
        //LoadScore();
        isGameStart = true;
        PlayerAudioManager.instance.PlayerAudio(PlayerAudioManager.instance.tock, false);
        ScreenController.instance.StartDelayText.color = Color.green;
        ScreenController.instance.StartDelayText.text = "Run!";
        yield return new WaitForSeconds(0.5f);
        ScreenController.instance.StartDelayText.gameObject.SetActive(false);
        ScreenController.instance.PowerPurchaseAtStart.GetComponent<PowerPurchaseAtStart>().setPower(Save.magnetPower, true, 0);
        ScreenController.instance.PowerPurchaseAtStart.SetActive(true);
        Camera.main.nearClipPlane = 10f;
    }

    #region Tut
    //public int tutStep = 0;
    //public void RunTutorial()
    //{
    //    if (PlayerPrefs.GetString("tutDone") != "true")
    //    {


    //        if (tutStep == 0)
    //        {
    //            StartCoroutine(mainPanelTUT());
    //        }
    //        if (tutStep == 1)
    //        {
    //            StartCoroutine(RightSwipeTut());


    //        }
    //        if (tutStep == 2)
    //        {
    //            Time.timeScale = 1f;
    //            StartCoroutine(LefttSwipeTut());


    //        }
    //        if (tutStep == 3)
    //        {
    //            Time.timeScale = 1f;
    //            StartCoroutine(UpSwipeTut());


    //        }
    //        if (tutStep == 4)
    //        {
    //            Time.timeScale = 1f;
    //            StartCoroutine(TutDone());
    //        }


    //    }


    //}

    //IEnumerator mainPanelTUT()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    mainPanelTutorial.SetActive(true);
    //    swipeRighttxt.SetActive(false);
    //    swipeLefttxt.SetActive(false);

    //    swipeUptxt.SetActive(false);
    //    tutStep = 1;
    //}

    //IEnumerator RightSwipeTut()
    //{
    //    swipeLefttxt.SetActive(false);
    //    swipeUptxt.SetActive(false);
    //    mainPanelTutorial.SetActive(false);
    //    yield return new WaitForSeconds(0.5f);

    //    swipeRighttxt.SetActive(true);

    //    Time.timeScale = 0f;
    //    tutStep = 2;

    //}
    //IEnumerator LefttSwipeTut()
    //{
    //    mainPanelTutorial.SetActive(false);
    //    swipeRighttxt.SetActive(false);
    //    swipeUptxt.SetActive(false);
    //    yield return new WaitForSeconds(0.5f);

    //    swipeLefttxt.SetActive(true);


    //    Time.timeScale = 0f;
    //    tutStep = 3;

    //}
    //IEnumerator UpSwipeTut()
    //{
    //    mainPanelTutorial.SetActive(false);
    //    swipeRighttxt.SetActive(false);
    //    swipeLefttxt.SetActive(false);
    //    yield return new WaitForSeconds(0.5f);


    //    swipeUptxt.SetActive(true);
    //    Time.timeScale = 0f;
    //    tutStep = 4;

    //}

    //IEnumerator TutDone()
    //{
    //    mainPanelTutorial.SetActive(false);
    //    swipeRighttxt.SetActive(false);
    //    swipeLefttxt.SetActive(false);
    //    swipeUptxt.SetActive(false);
    //    yield return new WaitForSeconds(0.5f);

    //    tutStep = 5;
    //    PlayerPrefs.SetString("tutDone", "true");

    //}

    #endregion


    private void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        // ScreenController.instance.fpsDisplay.text = "" + (int)fps;

        if (Application.isMobilePlatform)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // Debug.Log("B");
                if (ScreenController.instance.HomeScreen.activeInHierarchy)
                {
                    Application.Quit();


                }
            }
        }
    }



    private void playergameOverEvent_fn()
    {
        ScreenController.instance.touchBlocker.SetActive(true);
        PlayerAudioManager.instance.PlayerAudio(PlayerAudioManager.instance.CollideMusic, false);

        isPlayerDead = true;
        //savePlayerData();
        // ScreenController.instance.retryBlocker.SetActive(true);
        StartCoroutine(enableRetryPanel());

    }

    IEnumerator enableRetryPanel()
    {
        //PlayerAudioManager.instance.PlayerAudio(PlayerAudioManager.instance.CollideSFX, false);
        //yield return new WaitUntil(() => !PlayerAudioManager.instance.audioSource.isPlaying);
        //PlayerAudioManager.instance.PlayerAudio(PlayerAudioManager.instance.CollideMusic, false);
        //ScreenController.instance.RetryPanel.SetActive(true);

        audioSource.mute = true;

        yield return new WaitForSeconds(2f);
        if (PlayerLifeInstance.instance.life_Current != 0)
        {
            OnClick_ContinueGame();

        }
        else
        {
            PlayerDataController.instance.SendScoreToServer();

            ScreenController.instance.touchBlocker.SetActive(false);
            ScreenController.instance.NoLifePanel.SetActive(true);
        }

    }

    private void savePlayerData()
    {

        PlayerDataController.instance.saveCoinstoLocal();
        PlayerDataController.instance.SaveTotalScoreData();
    }

    public void OnClick_ContinueGame()
    {
        //continueVFX.SetActive(true);
        //continueVFX.GetComponent<ParticleSystem>().Play();
        EventController.instance.gameContinueEvent_Fn();
        isPlayerDead = false;
        isPressedStart = true;
        isGameStart = false;
        isContinueGame = true;
        isGameStart = false;
        // ScreenController.instance.GamePlayScreen.SetActive(false);
        // ScreenController.instance.Consumablepanel.SetActive(false);
        ScreenController.instance.RetryPanel.SetActive(false);
        // ScreenController.instance.retryBlocker.SetActive(false);
        ScreenController.instance.StartDelayText.gameObject.SetActive(true);
        DifficultyController.instance.moveZSpeed = DifficultyController.instance.easySpeedLimit;

        StartDelay = 4;
        StartCoroutine(ContinueGameStartDelay());
    }

    IEnumerator ContinueGameStartDelay()
    {
        ScreenController.instance.HomeScreen.SetActive(false);
        ScreenController.instance.GamePlayScreen.SetActive(true);
        ScreenController.instance.StartDelayText.gameObject.SetActive(true);
        ScreenController.instance.StartDelayText.text = "";
        yield return new WaitForSeconds(2f);

        while (StartDelay > 0)
        {
            yield return new WaitForSeconds(1f);

            PlayerAudioManager.instance.PlayerAudio(PlayerAudioManager.instance.tick, false);
            StartDelay -= 1;
            if (StartDelay == 3)
            {
                ScreenController.instance.StartDelayText.color = Color.cyan;


            }
            if (StartDelay == 2)
            {
                ScreenController.instance.StartDelayText.color = new Color(1f, 0.6f, 0);

            }
            if (StartDelay == 1)
            {

                ScreenController.instance.StartDelayText.color = new Color(1f, 0.3f, 0);
            }
            ScreenController.instance.StartDelayText.text = StartDelay.ToString();


        }
        yield return new WaitForSeconds(1f);
        //LoadScore();
        isGameStart = true;
        isContinueGame = false;
        PlayerAudioManager.instance.PlayerAudio(PlayerAudioManager.instance.tock, false);
        ScreenController.instance.StartDelayText.color = Color.green;
        ScreenController.instance.StartDelayText.text = "Run!";
        yield return new WaitForSeconds(0.5f);
        ScreenController.instance.StartDelayText.gameObject.SetActive(false);
        ScreenController.instance.touchBlocker.SetActive(false);
        if (!isMuted)
            audioSource.mute = false;
    }

    public void MuteAudio(bool b)
    {
        AudioSource[] audioSource = FindObjectsOfTypeAll(typeof(AudioSource)) as AudioSource[];
        foreach (var i in audioSource)
        {
            i.mute = b;
        }
        isMuted = b;
        if (isMuted)
        {
            ScreenController.instance.musicOffBT.SetActive(true);
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            ScreenController.instance.musicOnBT.SetActive(true);
            PlayerPrefs.SetInt("sound", 0);
        }
    }

    public void OnClick_Pause_Resume(bool b)
    {
        AudioListener[] audioSource = Resources. FindObjectsOfTypeAll(typeof(AudioListener)) as AudioListener[];
        foreach (var i in audioSource)
        {
            if (!isMuted)
                i.enabled = !b;
        }
    }

    public void GameReset()
    {

        PlatformManagerReset();
        // LifeLoad();
        GameInstanceReset();
        ResetConsumable();

    }

    void GameInstanceReset()
    {
        isPlayerDead = false;
        isPressedStart = false;
        StartDelay = 4;
        isGameStart = false;
        ScreenController.instance.GamePlayScreen.SetActive(false);
        // ScreenController.instance.Consumablepanel.SetActive(false);
        ScreenController.instance.RetryPanel.SetActive(false);
        DifficultyController.instance.moveZSpeed = DifficultyController.instance.defaultSpeed;


    }

    void PlatformManagerReset()
    {
        PlatformController.instance.CustomStart();
    }

    void ResetConsumable()
    {
        //ConsumableInstance.instance.load_Consumable();

    }


    public void OnClick_Pause()
    {
        Time.timeScale = 0f;

    }

    public void OnClick_Resume()
    {
        Time.timeScale = 1f;
    }

    public void OnClick_QuitViaNolife()
    {
        ScreenController.instance.loadingPanel.SetActive(true);
        Time.timeScale = 1f;
        DOTween.Clear(true);
        SceneManager.LoadSceneAsync(0);
    }
    public void OnClick_Quit()
    {
        ScreenController.instance.loadingPanel.SetActive(true);
      StartCoroutine(_OnClickQuit());
    }

    private IEnumerator _OnClickQuit()
    {
        Time.timeScale = 0f;
        PlayerDataController.instance.SendScoreToServer();

        yield return StartCoroutine(PlayerDataController.instance.GenTransactionID());
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        DOTween.Clear(true);
        SceneManager.LoadSceneAsync(0);
    }


    public void OnClick_Continue()
    {
        // GameInstanceReset();
        DOTween.Clear(true);
        SceneManager.LoadScene(0);
    }

    public void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 2);
                toastObject.Call("show");
            }));
        }
    }

    public void privacyPolicy()
    {
        Application.OpenURL("");


    }


    private void OnApplicationQuit()
    {

    }



}
