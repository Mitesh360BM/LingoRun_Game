using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PowerUPBuyEnabler : MonoBehaviour
{
    [Header("PowerUP Objects")]
    public List<GameObject> PowerObject;

    private string data;

    public TextMeshProUGUI textmesh;
   

    
    private void OnEnable()
    {
      //  textmesh.text = "<p>Find the <strong><em><u>Adjective</u></em></strong></p>";
        StartCoroutine(checkForPower());
        
    }

    IEnumerator checkForPower()
    {
        PowerObject.ForEach((t) => {

            t.SetActive(false);
        
        });

        yield return new WaitUntil(() => APIController.instance.isReady);
        if (APIController.instance.serverStatus == APIController.ServerStatus.Online)
        {
            enablePowerBuyViaAPI();

        }
    }
    private void enablePowerBuyViaAPI()
    {
        StartCoroutine(APIController.instance.GetPowerBYGameId((status) =>
        {
        if (status != null && !string.IsNullOrEmpty(status.ToString()))
        {
            data = status;
            JObject keyValuePairs = JObject.Parse(data);
            Debug.Log(keyValuePairs["Items"]);
               
               // JArray jArray = JArray.Parse(data);
                var jsonData = keyValuePairs["Items"].OfType<JObject>().ToList();
                if (jsonData.Count != 0)
                {
                    Debug.Log(jsonData[0]["powerName"]);

                    checkPowerUP(jsonData);
                }
            }
        }));

    }

    private void checkPowerUP(List<JObject> data)
    {
        string gameID = APIController.gameID;
        if (data.Count != 0)
        {
            for (int i = 0; i < data.Count; i++)
            {
                string _gameID = data[i]["gameId"].ToString();
                string j_powerName = data[i]["powerName"].ToString();
              
                if (gameID == _gameID)
                {
                    bool b = bool.Parse(data[i]["isEnable"].ToString());
                    if (b)
                    {
                        Debug.Log(j_powerName);
                        PowerObject.ForEach((power) =>
                        {
                           
                            string p_powerName = power.GetComponent<PowerPurchase>().powerName;
                            if (j_powerName == p_powerName)
                            {
                                power.GetComponent<PowerPurchase>().powerId = data[i]["id"].ToString();
                                power.GetComponent<PowerPurchase>().powerCost = int.Parse(data[i]["coins"].ToString());
                                power.GetComponent<PowerPurchase>().buyLimit = int.Parse(data[i]["limitCount"].ToString());
                                power.GetComponent<PowerPurchase>().readyToPurchase = true;
                                power.SetActive(true);
                            }

                        });

                    }
                }
            }
        }
    }


}
