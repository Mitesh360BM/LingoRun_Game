using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSelector : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> gameObjects = new List<GameObject>();

    private void OnEnable()
    {
        if (gameObjects.Count == 0)
        {
            for (int c = 0; c < transform.childCount; c++)
            {
                gameObjects.Add(transform.GetChild(c).gameObject);

            }


        }


        gameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });


        int i = Random.Range(0, gameObjects.Count);

        gameObjects[i].SetActive(true);

    }





}
