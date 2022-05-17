using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTypeEnableScript : MonoBehaviour
{
    private enum ObjectType { 
    
        Coin,
        PowerUp,
        Obstacle

    
    }
    [Header("Object Type")]
    [SerializeField]
    private ObjectType objectType;


    [Header("Objects")]
    [SerializeField]
    private List<GameObject> ObstacleTypeGameObjects = new List<GameObject>();

    [SerializeField]
    private List<GameObject> CoinTypeGameObjects = new List<GameObject>();

    [SerializeField]
    private List<GameObject> PowerTypeGameObjects = new List<GameObject>();

    private void OnEnable()
    {
        if (objectType == ObjectType.Obstacle)
        {
            ObstacleType();
        }
        else if (objectType == ObjectType.Coin)
        {
            CoinType();
        
        }
        else if (objectType == ObjectType.PowerUp)
        {
            PowerType();

        }

    }


    private void ObstacleType()
    {
        if (ObstacleTypeGameObjects.Count == 0)
        {
            for (int c = 0; c < transform.childCount; c++)
            {
                ObstacleTypeGameObjects.Add(transform.GetChild(c).gameObject);

            }


        }


        ObstacleTypeGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });

        int i = Random.Range(0, ObstacleTypeGameObjects.Count);

        ObstacleTypeGameObjects[i].SetActive(true);

    }

    private void CoinType()
    {

        int i = Random.Range(0, CoinTypeGameObjects.Count);

       
        GameObject go = Instantiate(CoinTypeGameObjects[i], this.transform);

    }

    private void PowerType()
    {
        int i = Random.Range(0, PowerTypeGameObjects.Count);

        GameObject go = Instantiate(PowerTypeGameObjects[i], this.transform);
    }

}
