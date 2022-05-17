using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEnable : MonoBehaviour
{
    public List<PowerEnableData> powerObjects = new List<PowerEnableData>();

    public List<PowerEnableData> powerEnableGameActive = new List<PowerEnableData>();
    public List<PowerEnableData> powerEnableGameInActive = new List<PowerEnableData>();
    public Transform ActiveTrans, NotActiveTrans;

    private void OnEnable()
    {
        if (APIController.instance.serverStatus == APIController.ServerStatus.Online)
        {
            StartCoroutine(EnablePowerInGamePlay());
        }

        //  startCoolDown();
    }

    public IEnumerator EnablePowerInGamePlay()
    {
        yield return new WaitUntil(() => PlayerDataController.instance.isReady);

        powerObjects.ForEach((t) => {

            t.powerObject.transform.SetParent(NotActiveTrans);
        
        
        });


        //powerEnableGameActive.Clear();
       
        powerObjects.ForEach((t) =>
        {

            PlayerDataController.instance.powerDataList.ForEach((POWER) =>
            {

                if (t.powerName == POWER.powerName)
                {

                    if (POWER.powerCount > 0)
                    {
                            t.powerObject.transform.SetParent(ActiveTrans);
                    }



                }
                


            });


        });

       


        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(EnablePowerInGamePlay());

        //StartCoroutine(APIController.instance.GetPowerBYGameId((status) =>
        //{
        //    if (status != null && !string.IsNullOrEmpty(status.ToString()))
        //    {
        //        JArray jArray = JArray.Parse(status);

        //        for (int i = 0; i < jArray.Count; i++)
        //        {

        //            powerEnableGameObjects.ForEach((t)=> {

        //                if (t.powerName == jArray[i]["powerName"].ToString())
        //                {
        //                    t.powerObject.transform.SetParent(ActiveTrans);

        //                }
        //                //else
        //                //{
        //                //    t.powerObject.transform.SetParent(NotActiveTrans);

        //                //}
        //            });
        //           // if(jArray[i]["powerName"].ToString()==)
        //        }
        //    }

        //}));
    }


}
[Serializable]
public class PowerEnableData
{
    public string powerName;
    public GameObject powerObject;
}