using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableEnableScript : MonoBehaviour
{

    public List<GameObject> consumable_gameObject;
    
    void Start()
    {
        StartCoroutine(EnableConsumable());
       
    }

   IEnumerator EnableConsumable()
    {
        yield return new WaitForSeconds(0f);
        foreach (Transform child in transform)
        {
            consumable_gameObject.Add(child.gameObject);
        }

        yield return new WaitForSeconds(1f);
        int i = Random.Range(0, consumable_gameObject.Count);
       
            consumable_gameObject[i].gameObject.SetActive(true);
       



    }

   
}
