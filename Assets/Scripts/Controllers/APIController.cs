using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
public class APIController : MonoBehaviour
{
    public static APIController instance;
    public bool isTestServer;
    public bool isReady { get; private set; }

    public static string gameID = "nDiny1GkmGtp99kvoTQN";
    public static string userID {get;set;}
    private static string IP_ADDRESS = "3.108.149.41";
    public enum TransactionType { 
    
        Purchase,
        Spend,
        Earn
    
    }
    public TransactionType transactionType;

    public enum ServerStatus
    {

        Online,
        Offline

    }

    public ServerStatus serverStatus;


    [System.Serializable]

    private class API_Route
    {
        public string GetServerStatus = "http://"+IP_ADDRESS+"/";
        public string GetQuestionListByID = "http://" + IP_ADDRESS + "/api/singleque/get/";
        public string GetPowerUPBy_GameID = "http://" + IP_ADDRESS + "/api/power-ups/get-powerup/";
        public string UpatePowerUP = "http://" + IP_ADDRESS + "/api/power-ups/" + userID + "/update-power-up/";
        public string GetWalletBalanceByUserId = "http://" + IP_ADDRESS + "/api/wallet/getBalance/";
        public string CreditWallet = "http://" + IP_ADDRESS + "/api/transaction/";
        public string DebitWallet = "http://" + IP_ADDRESS + "/api/transaction/";
        public string RegisterScore = "http://" + IP_ADDRESS + "/api/leaderboard/register";
        public string GetUserDetailById = "http://" + IP_ADDRESS + "/api/user/searchById/";
        public string GetGameDetailsBy_UserID_GameID = "http://" + IP_ADDRESS + "/api/user/getGameDetails/";
        public string PurchasePowerBY_UserID_PowerID_Number = "http://" + IP_ADDRESS + "/api/power-ups/purchase-powerup/";
        public string UsePowerUpBy_UserID_PowerID = "http://" + IP_ADDRESS + "/api/power-ups/use-powerup/";
        public string UpdateLevelBy_UserID_GameID = "http://" + IP_ADDRESS + "/api/user/updateLevel/";
        public string GetTransactionIDBy_UserID_GameID = "http://" + IP_ADDRESS + "/api/transactionId/";
        public string SubmitAnser = "http://" + IP_ADDRESS + "/api/answer/";
        public string Transaction = "http://" + IP_ADDRESS + "/api/transaction/";
        public string GetPlayerUserIDBYUniqueID = "http://" + IP_ADDRESS + "/api/user/searchByUniqueid/";

    }
 
   [HideInInspector]
   [SerializeField]
    private API_Route apiRoute;



    private void Awake()
    {
        instance = this;
        IP_ADDRESS = "3.108.149.41";
       // gameID = "nDiny1GkmGtp99kvoTQN";

    }
    private void Start()
    {
        // StartCoroutine();

        // API_RequestTest();
        if (isTestServer)
        {
            isReady = true;
             // serverStatus = ServerStatus.Offline;
        }
        else
        {


            StartCoroutine(GetServerStatus((status) =>
            {

                if (status != null)
                {
                    var data = JObject.Parse(status);
                    if (data["message"].ToString() == "Server is Up")
                    {

                        serverStatus = ServerStatus.Online;
                        isReady = true;

                    }
                    else
                    {
                        serverStatus = ServerStatus.Offline;
                        isReady = true;

                    }

                }



            }));
        }
      
    }

    public IEnumerator GetServerStatus(System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.GetServerStatus;
        Debug.Log(URL);
        UnityWebRequest request = UnityWebRequest.Get(URL);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);



    }

    public IEnumerator GetUserIDByUniqueID(string uniqueID,System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.GetPlayerUserIDBYUniqueID+uniqueID;
        UnityWebRequest request = UnityWebRequest.Get(URL);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);
    }


    #region Question API
   // http://3.108.149.41:3000/api/singleque/get/2/1/nDiny1GkmGtp99kvoTQN?limit=10&userId=yLq3LvjqWBtU12K7Q6KS
    public IEnumerator GetQuestions(int grade, int level, System.Action<string> callback)
    {
        string data = null;
        yield return new WaitForSeconds(1f);
        string limit = "?limit=6&userId="+userID;
        string URL = apiRoute.GetQuestionListByID + grade + "/" + level + "/" + gameID+limit;
        //Debug.Log(URL);
        UnityWebRequest request = UnityWebRequest.Get(URL);
        //request.SetRequestHeader("Content-Type", "application/json");
        //request.SetRequestHeader("Accept", "*/*");
        //request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(_jsonData));
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Question Data Null");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }
    #endregion


    #region API PowerUP
    public IEnumerator GetPowerBYGameId(System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.GetPowerUPBy_GameID + gameID;
        UnityWebRequest request = UnityWebRequest.Get(URL);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }
    public IEnumerator UpdatePoweUP(string powerName, System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.UpatePowerUP;
        WWWForm form = new WWWForm();
        form.AddField("_id", gameID);
        form.AddField("userId", userID);
        form.AddField("powerName", powerName);
        UnityWebRequest request = UnityWebRequest.Post(URL, form);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }
    #endregion


    #region WalletAPI
    public IEnumerator GetWalletBalance(System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.GetWalletBalanceByUserId + userID;
        yield return new WaitUntil(() => !string.IsNullOrEmpty(URL));
        UnityWebRequest request = UnityWebRequest.Get(URL);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            // Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }
   

    public IEnumerator CreditBalance(string transactionId,string coins, string transaction, System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.Transaction;
        WWWForm form = new WWWForm();
        form.AddField("transactionId", transactionId);
        form.AddField("userId", userID);
        form.AddField("coins", coins);
        form.AddField("type", transaction);


        UnityWebRequest request = UnityWebRequest.Post(URL, form);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }

    public IEnumerator DebitBalance(string transactionId, string coins, string transaction, System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.Transaction;
        WWWForm form = new WWWForm();
        form.AddField("transactionId", transactionId);
        form.AddField("userId", userID);
        form.AddField("coins", coins);
        form.AddField("type", transaction);

        UnityWebRequest request = UnityWebRequest.Post(URL, form);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }
    #endregion

    #region Register Score
    public IEnumerator RegisterScore(string score, System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.RegisterScore;
        WWWForm form = new WWWForm();
        form.AddField("userId", userID);
        form.AddField("gameId", gameID);
        form.AddField("score", score);


        UnityWebRequest request = UnityWebRequest.Post(URL, form);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }
    #endregion


    #region
    public IEnumerator GetUserDataById(System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.GetUserDetailById + userID;
        yield return new WaitUntil(() => !string.IsNullOrEmpty(URL));
        UnityWebRequest request = UnityWebRequest.Get(URL);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //

            data = jsonData.ToString();
        }
        
        Debug.Log(data);
        callback(data);
    }

    public IEnumerator GetGameDetails(System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.GetGameDetailsBy_UserID_GameID + userID + "/" + gameID;
        // Debug.Log(URL);
        yield return new WaitUntil(() => !string.IsNullOrEmpty(URL));
        UnityWebRequest request = UnityWebRequest.Get(URL);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            // Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);
    }

    public IEnumerator PurchasePower(string powerId, string powerCount, System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.PurchasePowerBY_UserID_PowerID_Number + userID + "/" + powerId + "/" + powerCount;
        Debug.Log(URL);
        yield return new WaitUntil(() => !string.IsNullOrEmpty(URL));
        UnityWebRequest request = UnityWebRequest.Post(URL,"");
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            // Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);
    }
    public IEnumerator UsePurchasePower(string powerId, System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.UsePowerUpBy_UserID_PowerID + userID + "/" + powerId;
        Debug.Log(URL);
        yield return new WaitUntil(() => !string.IsNullOrEmpty(URL));
        UnityWebRequest request = UnityWebRequest.Get(URL);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            // Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);
    }


    public IEnumerator UpdateLevel( System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.UpdateLevelBy_UserID_GameID + userID + "/" + gameID;
        Debug.Log(URL);
        yield return new WaitUntil(() => !string.IsNullOrEmpty(URL));
        UnityWebRequest request = UnityWebRequest.Get(URL);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            // Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);
    }
    public IEnumerator GetTransactionID(System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.GetTransactionIDBy_UserID_GameID;
        WWWForm form = new WWWForm();
        form.AddField("userId", userID);
        form.AddField("gameId", gameID);

        UnityWebRequest request = UnityWebRequest.Post(URL, form);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }
    public IEnumerator SubmitAnswer(string questionId,string answer,System.Action<string> callback)
    {
        string data = null;
        string URL = apiRoute.SubmitAnser;
        WWWForm form = new WWWForm();
        form.AddField("questionId", questionId);
        form.AddField("grade", PlayerDataController.instance.GradeCount);
        form.AddField("level", PlayerDataController.instance.LevelCount);
        form.AddField("userId", userID);
        form.AddField("gameId", gameID);
        form.AddField("answer", answer);

        UnityWebRequest request = UnityWebRequest.Post(URL, form);
        UnityWebRequestAsyncOperation async = request.SendWebRequest();

        while (!async.isDone)
        {
            yield return null;
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Network Error");
            data = null;

        }
        else
        {
            var jsonData = request.downloadHandler.text;
            //Debug.Log(jsonData);

            data = jsonData.ToString();
        }
        callback(data);

    }
    #endregion
}
