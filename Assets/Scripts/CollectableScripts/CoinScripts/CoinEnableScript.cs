using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEnableScript : MonoBehaviour
{
    [Header("Prefab")]
    public List<GameObject> coin_Prefab;

    public Transform coinPivot;

    private void OnEnable()
    {
        StartCoroutine(SpawnCoin());
    }

   

    IEnumerator SpawnCoin()
    {
        for (int t = 0; t < transform.childCount; t++)
        {

            if (transform.GetChild(t).name == "CoinPivot")
            {
                coinPivot = transform.GetChild(t);

            }

        }

        yield return new WaitForSeconds(0f);

        int i = Random.Range(0,2);

        if (coin_Prefab.Count > i)
        {
            if (coinPivot != null)
            {
                GameObject go = Instantiate(coin_Prefab[i], coinPivot);
            }
            else
            {
                GameObject go = Instantiate(coin_Prefab[i], this.transform);

            }
            //  coin_Prefab[i].gameObject.SetActive(true);
        }

    }



}
