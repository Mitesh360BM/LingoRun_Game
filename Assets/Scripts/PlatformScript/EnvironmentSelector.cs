using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSelector : MonoBehaviour
{
    public List<GameObject> envList = new List<GameObject>();


    private void OnEnable()
    {
        StartCoroutine(changeEnv());
       
        
    }

    private IEnumerator changeEnv()
    {
        yield return new WaitUntil(()=>PlatformController.instance != null);
        for (int i = 0; i < envList.Count; i++)
        {
            if (i == PlatformController.instance.currentEnv)
            {
                envList[i].SetActive(true);
            }
            else
            {

                envList[i].SetActive(false);
            }

        }

    }

}
