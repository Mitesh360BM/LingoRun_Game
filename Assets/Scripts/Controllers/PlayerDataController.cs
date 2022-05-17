using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController instance;


    [SerializeField]
    private DataCommunication dataCommunication;


    public bool isReady { get; private set; }
    [Header("Player Score Data")]
    public int TotalScore;
    public int CurrentCoins { get;  set; }
    public int TotalCoins;
    public float DistanceCovered;
    public int CorrectAnswer;
    public int DailyStreak;

    [Header("Player Profile Data")]
    public string FirstName;
    public string LastName;
    public string UserName;
    public string Email;
    public bool isVerified;
    public int walletCoins;
    public List<PlayerPowerData> powerDataList = new List<PlayerPowerData>();

    [Header("Player Level Data")]
    public int LevelCount;
    public int GradeCount;
    private int BonusCount;

    [Header("Player PowerUP Data")]
    public int magnetCount;
    public int bikeCount;
    public int slowMoCount;
    public int hulkCount;
    public int skateCount;
    public int flyingCount;

    private void Awake()
    {
        instance = this;
    }
    IEnumerator Start()
    {
        EventController.instance.coinCollectEvent += updateCoinsData;
        // loadPowerData();
        yield return new WaitUntil(() => APIController.instance.isReady);
        //  StartCoroutine(getGameDetails());
        if (APIController.instance.serverStatus == APIController.ServerStatus.Online)
        {
            StartCoroutine(getPlayerUserID());
        }
        else
        {
            loadCoinsDataFromLocal();
            Debug.LogError("Server is Offline");

        }

    }
    private void OnDestroy()
    {
        EventController.instance.coinCollectEvent -= updateCoinsData;
    }


    private void updateCoinsData()
    {
        CurrentCoins += 1;
        TotalCoins += 1;

    }

    private void Update()
    {
        if (GameController.instance.isPressedStart)
            CalculateTotalScore();

    }
    private void CalculateTotalScore()
    {
        BonusCount = LevelCount * 1000;
        TotalScore = (int)((CorrectAnswer * 100) + (DistanceCovered * 1 * DailyStreak) + (CurrentCoins * 1) + BonusCount);

    }
    private void setUserInterface()
    {
        ScreenController.instance.totalScoreText.text = TotalScore.ToString();
    }




    #region Online Modules

    private IEnumerator getPlayerUserID()
    {
        string data = null;
        yield return new WaitUntil(() => APIController.instance.isReady);
        yield return new WaitUntil(() => dataCommunication.isReady);

        //StartCoroutine(APIController.instance.GetUserIDByUniqueID("ytanWJhc", status =>
       StartCoroutine(APIController.instance.GetUserIDByUniqueID(dataCommunication.playerUniqueID, status =>
        {
           // Debug.Log(status);
            if (status != null && !string.IsNullOrEmpty(status.ToString()))
            {
                data = status;
                if (data != null)
                {
                    #region Getting Player Data
                    JArray json = JArray.Parse(data);
                    // Debug.Log(json[0]["id"]);

                    string userId = json[0]["id"].ToString();
                    APIController.userID = userId;
                    FirstName = json[0]["firstName"].ToString();
                    LastName = json[0]["lastName"].ToString();
                    UserName = json[0]["userName"].ToString();
                    Email = json[0]["email"].ToString();
                    GradeCount = int.Parse(json[0]["grade"].ToString());
                    isVerified = bool.Parse(json[0]["isVerified"].ToString());
                    TotalCoins = int.Parse(json[0]["walletCoin"].ToString());

                    #endregion
                    StartCoroutine(getGameDetails());
                    //#region Getting levelCount via GameID
                    //string gameDetails = json["gameDetails"].ToString();
                    //JArray gameDetailsArray = JArray.Parse(gameDetails);
                    ////  Debug.Log(gameDetailsArray.Count);
                    //for (int i = 0; i < gameDetailsArray.Count; i++)
                    //{
                    //    string gameid = gameDetailsArray[i]["gameId"].ToString();
                    //    if (APIController.gameID == gameid)
                    //    {
                    //        LevelCount = int.Parse(gameDetailsArray[i]["level"].ToString());
                    //        // Debug.Log(LevelCount);
                    //        isReady = true;
                    //        break;
                    //    }
                    //}
                    //#endregion
                }
            }

        }));


    }

    private IEnumerator loadPlayerDataFromServer()
    {
        yield return new WaitUntil(() => APIController.instance.isReady);
        string data = null;
        StartCoroutine(APIController.instance.GetUserDataById((status) =>
        {
            if (status != null && !string.IsNullOrEmpty(status.ToString()))
            {
                data = status;
                if (data != null)
                {
                    #region Getting Player Data
                    var json = JObject.Parse(data);
                    FirstName = json["firstName"].ToString();
                    LastName = json["lastName"].ToString();
                    UserName = json["userName"].ToString();
                    Email = json["email"].ToString();
                    GradeCount = int.Parse(json["grade"].ToString());
                    isVerified = bool.Parse(json["isVerified"].ToString());
                    TotalCoins = int.Parse(json["walletCoin"].ToString());

                    #endregion
                    StartCoroutine(getGameDetails());
                    //#region Getting levelCount via GameID
                    //string gameDetails = json["gameDetails"].ToString();
                    //JArray gameDetailsArray = JArray.Parse(gameDetails);
                    ////  Debug.Log(gameDetailsArray.Count);
                    //for (int i = 0; i < gameDetailsArray.Count; i++)
                    //{
                    //    string gameid = gameDetailsArray[i]["gameId"].ToString();
                    //    if (APIController.gameID == gameid)
                    //    {
                    //        LevelCount = int.Parse(gameDetailsArray[i]["level"].ToString());
                    //        // Debug.Log(LevelCount);
                    //        isReady = true;
                    //        break;
                    //    }
                    //}
                    //#endregion
                }
            }
        }));

    }

    public IEnumerator getGameDetails()
    {
        yield return new WaitUntil(() => APIController.instance.isReady);
        yield return new WaitForSeconds(0f);
        string data = null;

        StartCoroutine(APIController.instance.GetGameDetails((status) =>
        {
            if (status != null && !string.IsNullOrEmpty(status.ToString()))
            {
                powerDataList.Clear();
                data = status;
                if (data != null)
                {
                    var json = JObject.Parse(data);
                    //   Debug.Log(json["gameDetail"]["gameId"]);

                    LevelCount = int.Parse(json["gameDetail"]["level"].ToString());
                    #region Getting powerDta via GameID
                    string powerDetails = json["gameDetail"]["powerUps"].ToString();
                    JArray powerDataArray = JArray.Parse(powerDetails);
                    for (int i = 0; i < powerDataArray.Count; i++)
                    {
                        if (int.Parse(powerDataArray[i]["count"].ToString()) != 0)
                        {
                            PlayerPowerData powerData = new PlayerPowerData();
                            powerData.powerID = powerDataArray[i]["id"].ToString();
                            powerData.powerName = "";

                            powerData.powerCount = int.Parse(powerDataArray[i]["count"].ToString());
                            powerDataList.Add(powerData);
                        }
                    }
                    StartCoroutine(setPowerName());
                    //  Debug.Log(gameDetailsArray.Count);

                    #endregion
                }
            }
        }));

    }

    IEnumerator setPowerName()
    {
        yield return new WaitForSeconds(0f);

        StartCoroutine(APIController.instance.GetPowerBYGameId((status) =>
        {
            if (status != null && !string.IsNullOrEmpty(status.ToString()))
            {
                var jsonData = JObject.Parse(status);
               // Debug.Log(jsonData["Items"].Count());
                JToken jArray = jsonData["Items"];
               // JArray jArray = JArray.Parse((string)jsonData["Items"]);
                int count = jArray.Count();
                for (int i = 0; i <count; i++)
                {
                    powerDataList.ForEach((t) =>
                    {

                        if (t.powerID == jArray[i]["id"].ToString())
                        {
                            t.powerName = jArray[i]["powerName"].ToString();
                        }
                    });
                }
                isReady = true;
                setPowerCount();
            }

        }));
    }

    private void setPowerCount()
    {
        powerDataList.ForEach((t) =>
        {

            if (t.powerName == Save.magnetPower)
            {
                magnetCount = t.powerCount;

            }
            if (t.powerName == Save.sloMoPower)
            {
                slowMoCount = t.powerCount;

            }
            if (t.powerName == Save.hulkPower)
            {
                hulkCount = t.powerCount;

            }
            if (t.powerName == Save.bikerPower)
            {
                bikeCount = t.powerCount;

            }
            if (t.powerName == Save.flyingPower)
            {
                flyingCount = t.powerCount;

            }
            if (t.powerName == Save.skatePower)
            {
                skateCount = t.powerCount;

            }
        });

    }

    public void UsePowerUP(string powerName)
    {
        if (APIController.instance.serverStatus == APIController.ServerStatus.Online)
        {
            string powerID = null;
            powerDataList.ForEach((t) =>
            {
                if (t.powerName == powerName)
                {
                    powerID = t.powerID;
                    StartCoroutine(APIController.instance.UsePurchasePower(powerID, (status) =>
                    {
                        if (status != null && !string.IsNullOrEmpty(status.ToString()))
                        {
                            Debug.Log(powerName + " : Use Power Successfull");
                            StartCoroutine(getGameDetails());
                        }

                    }));
                }
            });
        }
    }



    public IEnumerator GenTransactionID()
    {
      
        if (APIController.instance.serverStatus == APIController.ServerStatus.Online && APIController.userID != null)
        {
            
            yield return StartCoroutine(APIController.instance.GetTransactionID((status) =>
            {
               
                if (status != null && !string.IsNullOrEmpty(status))
                {
                    string str = null;
                    var json = JObject.Parse(status);
                    Debug.Log("Earn TID: "+ json["newTransactionId"]);
                    if (!string.IsNullOrEmpty(json["newTransactionId"].ToString()))
                    {
                        str = json["newTransactionId"].ToString();
                        saveCoinsToServer(str);
                    }

                }


            }));

        }

    }

    private void saveCoinsToServer(string transactionID)
    {

        string transactionId = transactionID;
        string creditAmount = CurrentCoins.ToString();
        string entryType = "Earn";



        StartCoroutine(APIController.instance.CreditBalance(transactionId, creditAmount, entryType, (status) =>
         {

             if (status != null)
             {
                 Debug.Log("Amount Credited: " + status);
             }


         }));

    }


    public void SendScoreToServer()
    {
        if (APIController.instance.serverStatus == APIController.ServerStatus.Online)
        {

            string totalScore = TotalScore.ToString();

            StartCoroutine(APIController.instance.RegisterScore(totalScore, (status) =>
            {
                if (status != null && !string.IsNullOrEmpty(status.ToString()))
                {
                    Debug.Log("Scrore Registered: " + status);
                }

            }));
        }
    }

    #endregion

    private void FixedUpdate()
    {

        UpdateUI();
    }
    private void UpdateUI()
    {

        ScreenController.instance.CoinsText.text = TotalCoins.ToString();
        ScreenController.instance.leveltext.text = "x" + LevelCount.ToString();
        if (GameController.instance.isGameStart)
            ScreenController.instance.totalScoreText.text = TotalScore.ToString();
    }

    #region Offline Modules


    #region PowerLoadData
    public void loadPowerData()
    {
        magnetCount = GetMagnetData();
        bikeCount = GetBikeData();
        slowMoCount = GetSlowMoData();
        hulkCount = GetHulkData();

    }

    private int GetMagnetData()
    {

        return Save.instance.GetInt(Save.magnetPower);

    }

    private int GetBikeData()
    {

        return Save.instance.GetInt(Save.bikerPower);

    }
    private int GetSlowMoData()
    {

        return Save.instance.GetInt(Save.sloMoPower);

    }
    private int GetHulkData()
    {

        return Save.instance.GetInt(Save.hulkPower);

    }

    public void SetPowerData(string s, int j)
    {
        //  Save.instance.SetInt(s, j);

    }

    #endregion

    #region Player Score Data SAVE   
    public void saveCoinstoLocal()
    {
        // Save.instance.SetInt(Save.TotalCoins, TotalCoins);
    }




    public void SaveTotalScoreData()
    {
        // Save.instance.SetInt(Save.TotalScore, TotalScore);
    }

    #endregion

    #region Player Score Data LOAD
    public void loadCoinsDataFromLocal()
    {
        // LoadCoinsFromServer();
        //TotalCoins = Save.instance.GetInt(Save.TotalCoins);
        //ScreenController.instance.CoinsText.text = TotalCoins.ToString();

    }

    #endregion
    #endregion
}
[Serializable]
public class PlayerPowerData
{
    public string powerID;
    public string powerName;
    public int powerCount;

}
