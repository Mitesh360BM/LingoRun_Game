using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpawnScript : MonoBehaviour
{
    [Header("Prefab")]
    public List<GameObject> consumable_Prefab;




    [Header("Consumable Settings")]
    public int RandomMin;
    public int RandomMax;
    public int SpawnRangeMin;
    public int SpawnRangeMax;

    
    private void Awake()
    {
        consumable_Prefab.Clear();
        int c = Resources.LoadAll("Prefab").Length;
        for (int i = 0; i < c; i++)
        {

            consumable_Prefab.Add(Resources.LoadAll("Prefab")[i] as GameObject);
        }
    }


    private void Start()
    {
       
    }

    private void OnEnable()
    {
        RandomMin = PowerUPController.instance.RandomMin;
        RandomMax = PowerUPController.instance.RandomMax;
        SpawnRangeMin = PowerUPController.instance.SpawnRangeMin;
        SpawnRangeMax = PowerUPController.instance.SpawnRangeMax;
       StartCoroutine( SpawnConsumable());
    }

   IEnumerator SpawnConsumable()
    {
        yield return new WaitForSeconds(0.5f);

        int i = Random.Range(RandomMin, RandomMax);

        if (i >= SpawnRangeMin && i <= SpawnRangeMax)
        {
            int j = Random.Range(0, consumable_Prefab.Count);
            Instantiate(consumable_Prefab[j],transform.position,Quaternion.identity);
        }
    }


}
