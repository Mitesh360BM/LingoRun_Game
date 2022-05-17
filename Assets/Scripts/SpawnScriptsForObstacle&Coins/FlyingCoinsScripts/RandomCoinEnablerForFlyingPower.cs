using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCoinEnablerForFlyingPower : MonoBehaviour
{
    [SerializeField] private List<GameObject> coinGroup = new List<GameObject>();
   


    private void OnEnable()
    {
      
        Enable_Coins();
       
    }

    private void Enable_Coins()
    {
        int i = Random.Range(0, coinGroup.Count);
        coinGroup[i].SetActive(true);
    
    
    }

    private void OnDisable()
    {
        coinGroup.ForEach((t)=> {

            t.SetActive(false);
        
        });
    }

}