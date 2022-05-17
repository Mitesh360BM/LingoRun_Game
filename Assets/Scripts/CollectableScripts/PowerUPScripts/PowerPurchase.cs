using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPurchase : MonoBehaviour
{
    [Header("Power Details")]
    public string powerId;
    public string powerName;
    public int powerCost;
    public int buyLimit;
    public int currentCount;

    public Text countText;
    public Text costText;

    public bool isTest;

    public bool readyToPurchase =true;

    private void OnEnable()
    {
        #region Deprecated
        //PlayerDataController.instance.loadPowerData();

        //if (powerName == Save.magnetPower)
        //{
        //    if (buyLimit > currentCount)
        //    {
        //        currentCount = PlayerDataController.instance.magnetCount;
        //    }

        //}
        //if (powerName == Save.bikerPower)
        //{
        //    if (buyLimit > currentCount)
        //    {
        //        currentCount = PlayerDataController.instance.bikeCount;

        //    }

        //}
        //if (powerName == Save.hulkPower)
        //{
        //    if (buyLimit > currentCount)
        //    {
        //        currentCount = PlayerDataController.instance.hulkCount;

        //    }

        //}
        //if (powerName == Save.sloMoPower)
        //{
        //    if (buyLimit > currentCount)
        //    {
        //        currentCount = PlayerDataController.instance.slowMoCount;
        //    }

        //}
        //countText.text = currentCount.ToString();
        //costText.text = powerCost.ToString();
        #endregion

        if (powerName == Save.magnetPower)
        {

            currentCount = PlayerDataController.instance.magnetCount;


        }
        if (powerName == Save.bikerPower)
        {

            currentCount = PlayerDataController.instance.bikeCount;



        }
        if (powerName == Save.hulkPower)
        {
            currentCount = PlayerDataController.instance.hulkCount;



        }
        if (powerName == Save.sloMoPower)
        {

            currentCount = PlayerDataController.instance.slowMoCount;


        }
        if (powerName == Save.flyingPower)
        {

            currentCount = PlayerDataController.instance.flyingCount;


        }
        if (powerName == Save.skatePower)
        {

            currentCount = PlayerDataController.instance.skateCount;


        }
        countText.text = currentCount.ToString();
        costText.text = powerCost.ToString();
    }
    public void OnClick_Purchase()
    {
        #region Deprecated
        //if (PlayerDataController.instance.TotalCoins >= 0 || isTest)
        //{
        //    if (PlayerDataController.instance.TotalCoins >= powerCost)
        //    {
        //        PlayerDataController.instance.TotalCoins -= powerCost;
        //        PlayerDataController.instance.saveCoinstoLocal();
        //        PlayerDataController.instance.loadCoinsDataFromLocal();

        //        if (powerName == Save.magnetPower)
        //        {
        //            if (buyLimit > currentCount)
        //            {
        //                currentCount += 1;
        //                PlayerDataController.instance.magnetCount = currentCount;
        //                PlayerDataController.instance.SetPowerData(Save.magnetPower, currentCount);


        //            }

        //        }
        //        if (powerName == Save.bikerPower)
        //        {
        //            if (buyLimit > currentCount)
        //            {
        //                currentCount += 1;
        //                PlayerDataController.instance.bikeCount = currentCount;
        //                PlayerDataController.instance.TotalCoins -= powerCost;
        //                PlayerDataController.instance.SetPowerData(Save.bikerPower, currentCount);

        //            }

        //        }
        //        if (powerName == Save.hulkPower)
        //        {
        //            if (buyLimit > currentCount)
        //            {
        //                currentCount += 1;
        //                PlayerDataController.instance.hulkCount = currentCount;
        //                PlayerDataController.instance.TotalCoins -= powerCost;
        //                PlayerDataController.instance.SetPowerData(Save.hulkPower, currentCount);

        //            }

        //        }
        //        if (powerName == Save.sloMoPower)
        //        {
        //            if (buyLimit > currentCount)
        //            {
        //                currentCount += 1;
        //                PlayerDataController.instance.slowMoCount = currentCount;
        //                PlayerDataController.instance.TotalCoins -= powerCost;
        //                PlayerDataController.instance.SetPowerData(Save.sloMoPower, currentCount);

        //            }

        //        }
        //        countText.text = currentCount.ToString();
        //        costText.text = powerCost.ToString();
        //      //  DebitWalletFromServer();
        //    }
        //}
        #endregion
        if (readyToPurchase)
        {
            if (PlayerDataController.instance.TotalCoins >= 0 || isTest)
            {
                if (PlayerDataController.instance.TotalCoins >= powerCost)
                {
                    readyToPurchase = false;
                    if (powerName == Save.magnetPower)
                    {
                        if (buyLimit > currentCount)
                        {
                            currentCount += 1;
                            PlayerDataController.instance.TotalCoins -= powerCost;
                            PlayerDataController.instance.magnetCount = currentCount;
                            PurchasePower();
                        }

                    }
                    if (powerName == Save.bikerPower)
                    {
                        if (buyLimit > currentCount)
                        {
                            currentCount += 1;
                            PlayerDataController.instance.bikeCount = currentCount;
                            PlayerDataController.instance.TotalCoins -= powerCost;
                            PurchasePower();
                        }

                    }
                    if (powerName == Save.hulkPower)
                    {
                        if (buyLimit > currentCount)
                        {
                            currentCount += 1;
                            PlayerDataController.instance.hulkCount = currentCount;
                            PlayerDataController.instance.TotalCoins -= powerCost;
                            PurchasePower();
                        }

                    }
                    if (powerName == Save.sloMoPower)
                    {
                        if (buyLimit > currentCount)
                        {
                            currentCount += 1;
                            PlayerDataController.instance.slowMoCount = currentCount;
                            PlayerDataController.instance.TotalCoins -= powerCost;
                            PurchasePower();
                        }

                    }
                    if (powerName == Save.flyingPower)
                    {
                        if (buyLimit > currentCount)
                        {
                            currentCount += 1;
                            PlayerDataController.instance.flyingCount = currentCount;
                            PlayerDataController.instance.TotalCoins -= powerCost;
                            PurchasePower();
                        }

                    }
                    if (powerName == Save.skatePower)
                    {
                        if (buyLimit > currentCount)
                        {
                            currentCount += 1;
                            PlayerDataController.instance.skateCount = currentCount;
                            PlayerDataController.instance.TotalCoins -= powerCost;
                            PurchasePower();
                        }

                    }
                    countText.text = currentCount.ToString();
                    costText.text = powerCost.ToString();
                  
                }
            }
        }
    }

    private void PurchasePower()
    { 
        StartCoroutine(APIController.instance.PurchasePower(powerId,"1",(status)=>{

            if (status != null && !string.IsNullOrEmpty(status.ToString()))
            {
                Debug.Log("Successfull"+ status);
                StartCoroutine(PlayerDataController.instance.getGameDetails());
               
               StartCoroutine( GenTransactionID());
            
            }
        }));
    }
    private IEnumerator GenTransactionID()
    {
        if (APIController.instance.serverStatus == APIController.ServerStatus.Online)
        {

            yield return StartCoroutine(APIController.instance.GetTransactionID((status) =>
            {

                if (status != null && !string.IsNullOrEmpty(status))
                {

                    var json = JObject.Parse(status);
                    Debug.Log(json["newTransactionId"] + " Spend TID");
                    string str = json["newTransactionId"].ToString();

                    DebitWalletFromServer(str);

                }


            }));

        }

    }

    private void DebitWalletFromServer(string tID)
    {
        string transactionId = tID;
        string debitAmount = powerCost.ToString();
        
        string entryType = "Spend";


        StartCoroutine(APIController.instance.DebitBalance(transactionId,debitAmount, entryType, (status) =>
        {
            if (status != null && !string.IsNullOrEmpty(status.ToString()))
            {
                Debug.Log("Amount Debited: " + status);
                readyToPurchase = true;

            }


        }));

    }

}
