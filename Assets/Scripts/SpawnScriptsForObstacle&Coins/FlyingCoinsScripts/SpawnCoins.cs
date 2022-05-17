using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private GameObject coin;

    private void OnEnable()
    {
        GameObject go = Instantiate(coin,transform);
    }

    private void OnDisable()
    {
        
    }
}
