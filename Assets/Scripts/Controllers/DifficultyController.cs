using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public static DifficultyController instance;
    public bool testMode;

    public float defaultSpeed = 0.4f;
    [Header("Easy")]
    public float easySpeedLimit;
    public float easySpeedIncrement;
    public int easyTimeThreshold;
    public bool isEasy;

    [Header("Medium")]
    public float mediumSpeedLimit;
    public float mediumSpeedIncrement;
    public int mediumTimeThreshold;
    public bool isMedium;

    [Header("Hard")]
    public float hardSpeedLimit;
    public float hardSpeedIncrement;
    public int hardTimeThreshold;
    public bool isHard;

    [Header("Player Gameplay Values")]
    public float moveZSpeed;
    public float timePassed;
    public float currentSpeed;

    //private Dictionary<string, float> speedVal = new Dictionary<string, float>
    //{

    //    {"1",0.4f },
    //    {"2",0.6f },
    //    {"3",0.7f },
    //    {"4",0.8f },
    //    {"5",0.9f },
    //    {"6",1f },
    //};




    private Dictionary<string, float> speedVal = new Dictionary<string, float>
    {
         {"0",0.4f },
        {"30",0.5f },
        {"60",0.6f },
        {"90",0.7f },
        {"120",0.8f },
        {"150",0.9f },
        {"180",1f },
    };


    private void Awake()
    {
        instance = this;
    }


    private void FixedUpdate()
    {
        if (GameController.instance.isGameStart)
            timePassed += Time.deltaTime;
    }
    private string timeSaver;
    private void Update()
    {
        if (GameController.instance.isPlayerDead)
        {
            moveZSpeed = 0;
          
        }
        if (!testMode)
        {
            //  int score = PlayerDataController.instance.CurrentCoins;
            //Debug.Log(Mathf.Floor(timePassed));
         
            string timeString = Mathf.Floor(timePassed).ToString();

            if (speedVal.ContainsKey(timeString))
            {
                 timeSaver = timeString;
                Debug.Log(timeString + "/" + speedVal[timeString]);

            }

                moveZSpeed = speedVal[timeSaver];

            //if (timePassed > easyTimeThreshold && timePassed < mediumTimeThreshold)
            //{
            //    Easy();
            //    isEasy = true;
            //    isMedium = false;
            //    isHard = false;
            //}
            //if (timePassed > mediumTimeThreshold && timePassed < hardTimeThreshold)
            //{
            //    Medium();
            //    isEasy = false;
            //    isMedium = true;
            //    isHard = false;
            //}
            //if (timePassed > hardTimeThreshold)
            //{
            //    Hard();
            //    isEasy = false;
            //    isMedium = false;
            //    isHard = true;
            //}
        }


    }

    private void Easy()
    {
        if (moveZSpeed >= easySpeedLimit)
        {


        }
        else
        {
            moveZSpeed += easySpeedIncrement;
        }


    }
    private void Medium()
    {
        if (moveZSpeed >= mediumSpeedLimit)
        {


        }
        else
        {
            moveZSpeed += mediumSpeedIncrement;
        }

    }
    private void Hard()
    {
        if (moveZSpeed >= hardSpeedLimit)
        {


        }
        else
        {
            moveZSpeed += hardSpeedIncrement;
        }

    }

}
