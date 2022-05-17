using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFlyingCoinGroup : MonoBehaviour
{
    [Header("Flying Object Group")]
    [SerializeField]
    private GameObject CoinGroup;

    [SerializeField] private GameObject obstacle2;
    private void Update()
    {
        CoinGroup.SetActive(FlyingPower.isFlyingActive);
    }

    private void OnEnable()
    {
        if(DifficultyController.instance.isMedium)
        {
           // obstacle2.SetActive(true)
;        }
    }
}
