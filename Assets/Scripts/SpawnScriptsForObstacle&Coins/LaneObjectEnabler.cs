using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneObjectEnabler : MonoBehaviour
{
    [Header("Obstacle Objects")]
    [SerializeField]
    private List<GameObject> ObstacleGameObjects = new List<GameObject>();

    [Header("Obstacle Objects")]
    [SerializeField]
    private List<GameObject> CoinGameObjects = new List<GameObject>();

    [Header("Power Objects")]
    [SerializeField]
    private List<GameObject> PowerGameObjects = new List<GameObject>();

   


    [SerializeField]
    private int ObstacleMin, ObstacleMax;

    private bool isQuizStarted = false,isFlying=false;

    private void Start()
    {
        EventController.instance.gameContinueEvent += gameContinueEvent;

    }



    private void gameContinueEvent()
    {
        ObstacleGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });

        CoinGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });
        PowerGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });

    }

    private void OnEnable()
    {

        StartCoroutine(Obstacle());
    }

    private void OnDisable()
    {
        ObstacleGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });

        CoinGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });
        PowerGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });

    }
    private IEnumerator Obstacle()
    {
        ObstacleGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });

        CoinGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });
        PowerGameObjects.ForEach((t) =>
         {
             t.gameObject.SetActive(false);
         });
        yield return new WaitUntil(() => QuizController.instance != null);
        isQuizStarted = QuizController.instance.isQuestionVisible;
        isFlying = FindObjectOfType<Player>().playerClass.onFly;
        if (!isQuizStarted && !isFlying)
        {
            int loopLength = 0;
            if (DifficultyController.instance.isMedium || DifficultyController.instance.isHard)
            {
                // obstacle2.SetActive(true)
             loopLength = Random.Range(3, ObstacleMax);
               
            }
            else
            {
                loopLength = Random.Range(ObstacleMin, ObstacleMax);
            }

            for (int j = 0; j < loopLength; j++)
            {
                int t = Random.Range(0, ObstacleGameObjects.Count);
                if (!ObstacleGameObjects[t].activeInHierarchy)
                {
                    ObstacleGameObjects[t].SetActive(true);
                }

            }
        }
        bool b = false;
        for (int k = 0; k < ObstacleGameObjects.Count; k++)
        {

            if (!ObstacleGameObjects[k].activeInHierarchy)
            {
                b = true;

            }
        }



        yield return new WaitUntil(() => b);
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            StartCoroutine(CoinsEnable());

        }
        else if (i >= 1)
        {
            StartCoroutine(PowerEnable());

        }
    }


    private IEnumerator CoinsEnable()
    {
        CoinGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });

        int random = Random.Range(PowerUPController.instance.RandomMin, PowerUPController.instance.RandomMax);

        if (PowerUPController.instance.SpawnRangeMin < random && PowerUPController.instance.SpawnRangeMax > random)
        {


            for (int i = 0; i < ObstacleGameObjects.Count; i++)
            {
                if (!ObstacleGameObjects[i].activeInHierarchy)
                {

                    CoinGameObjects[i].SetActive(true);

                }
            }
        }
        yield return null;
    }

    private IEnumerator PowerEnable()
    {

        PowerGameObjects.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        });

        int random = Random.Range(PowerUPController.instance.RandomMin, PowerUPController.instance.RandomMax);

        if (PowerUPController.instance.SpawnRangeMin < random && PowerUPController.instance.SpawnRangeMax > random)
        {

            for (int i = 0; i < ObstacleGameObjects.Count; i++)
            {
                if (!ObstacleGameObjects[i].activeInHierarchy)
                {
                    PowerGameObjects[i].SetActive(true);
                    break;
                }
            }
        }


        yield return null;
    }


}
