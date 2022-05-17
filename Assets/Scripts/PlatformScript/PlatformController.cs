using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformController : MonoBehaviour
{

    public static PlatformController instance;

    [Header("Platform Manager")]
    public GameObject[] startStage;
    public float PlatformZLength;

    [Header("PrefabList")]
    public List<GameObject> townStage;
    public List<GameObject> cityStage;
    public List<GameObject> forestStage;

    [Header("Restricted Variables")]
    public float zOffset;


    public int currentEnv = 0;

    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void currentEnvironment()
    {
        if (currentEnv != 2)
        {
            currentEnv += 1;
        }
        else
        {

            currentEnv = 0;
        }

    }


    void Start()
    {        
        CustomStart();
        InvokeRepeating("currentEnvironment", 10, 30);
    }

    public void CustomStart()
    {
        zOffset = 0;
        for (int k = 0; k < townStage.Count; k++)
        {
            townStage[k].SetActive(false);
            cityStage[k].SetActive(false);
            forestStage[k].SetActive(false);
            if (k == townStage.Count - 1)
            {
                SetPlatform();
            }

        }



    }


    void SetPlatform()
    {

        for (int k = 0; k < startStage.Length; k++)
        {
            startStage[k].SetActive(true);
            startStage[k].transform.position = new Vector3(0, 0, zOffset);
            zOffset += PlatformZLength;

        }


    }


    public void RecycleGameObject()
    {
        if (currentEnv == 0)
        {
            int i = Random.Range(0, townStage.Count);
            if (!townStage[i].activeInHierarchy)
            {
                townStage[i].SetActive(true);
                townStage[i].transform.position = new Vector3(0, 0, zOffset);
                zOffset += PlatformZLength;
            }
            else
            {
                RecycleGameObject();
            }
        }
        else if (currentEnv == 1)
        {
            int i = Random.Range(0, cityStage.Count);
            if (!cityStage[i].activeInHierarchy)
            {
                cityStage[i].SetActive(true);
                cityStage[i].transform.position = new Vector3(0, 0, zOffset);
                zOffset += PlatformZLength;
            }
            else
            {
                RecycleGameObject();
            }
        }

        else if (currentEnv == 2)
        {
            int i = Random.Range(0, forestStage.Count);
            if (!forestStage[i].activeInHierarchy)
            {
                forestStage[i].SetActive(true);
                forestStage[i].transform.position = new Vector3(0, 0, zOffset);
                zOffset += PlatformZLength;
            }
            else
            {
                RecycleGameObject();
            }
        }
    }




}
