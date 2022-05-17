using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerLifeInstance : MonoBehaviour
{
    public static PlayerLifeInstance instance;

   
    [Header("Life Setttings")]
    public int life_Current;
    public int life_Max;
    public float time_PerLife;
    public float timer_counter;
    //  public Text time;
    public Text lifetxt;




    [Header("Restricted Class")]
    public bool isLifeRegenerating;
    public bool hasMaxLife;
    public bool isAwake;

    public string lifeUpdateTime;
    public string timerCounter;

    private void Awake()
    {

        if (instance != this)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);

        }
        //  Debug.Log("Awake");
        //if (!PlayerPrefs.HasKey("life"))
        //{
        //    PlayerPrefs.SetString("LifeUpdateTime", DateTime.Now.ToString());
        //    PlayerPrefs.SetInt("life", life_Current);
        //    PlayerPrefs.SetFloat("timer", timer_counter);

        //    // Save.instance.SetLifeUpdateTime(DateTime.Now.ToString());
        //    // Save.instance.Set_Life(life_Current);
        //}
        //if (PlayerPrefs.HasKey("life"))
        //{

        //    life_Current = PlayerPrefs.GetInt("life");
        //    lifetxt.text = life_Current.ToString();
        //}
        //if (life_Max > life_Current)
        //{
        //    isAwake = true;
        //    timer_counter = PlayerPrefs.GetFloat("timer");
        //    float timerToAdd = (float)(System.DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("LifeUpdateTime"))).TotalSeconds;

        //    UpdateLivesForAwake(timerToAdd);

        //}
    }


    void OnApplicationPause(bool isPause)
    {
        //if (isPause)
        //{
        //    PlayerPrefs.SetString("LifeUpdateTime", DateTime.Now.ToString());
        //    PlayerPrefs.SetFloat("timer", timer_counter);


        //}

        //  Debug.Log(isPause+"Pause");
    }

    private void OnApplicationFocus(bool focus)
    {
        //if (focus && !isAwake)
        //{

        //    timer_counter = PlayerPrefs.GetFloat("timer");
        //    float timerToAdd = (float)(System.DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("LifeUpdateTime"))).TotalSeconds;

        //    UpdateLivesForPause(timerToAdd);
        //    // Time.timeScale = 1f;

        //}
        //if (!focus)
        //{
        //    PlayerPrefs.SetString("LifeUpdateTime", DateTime.Now.ToString());
        //    PlayerPrefs.SetFloat("timer", timer_counter);
        //}
        // Debug.Log(focus+"Focus");
    }
    private void Start()
    {
        //lifeUpdateTime = PlayerPrefs.GetString("LifeUpdateTime");
        timerCounter = PlayerPrefs.GetFloat("timer").ToString();
        // InvokeRepeating("regeneratLife",0f,1f);
       customStart();

        EventController.instance.playerDeadEvent += Instance_playerDeadEvent;
        EventController.instance.playerLifeEvent += Instance_playerLifeEvent;
    }

    private void Instance_playerLifeEvent()
    {
      
        StartCoroutine(PlayerDataController.instance.GenTransactionID());
        customStart();
        DecreaseLife();
    }

    public void Instance_playerDeadEvent()
    {
        
        StartCoroutine(PlayerDataController.instance.GenTransactionID());
        customStart();
        DecreaseLife();
       // if (life_Current == 0)
         
    }

    private void OnDestroy()
    {
        EventController.instance.playerDeadEvent -= Instance_playerDeadEvent;
        EventController.instance.playerLifeEvent -= Instance_playerLifeEvent;
    }

    public void customStart()
    {
        //life_Current = PlayerPrefs.GetInt("life");
        lifetxt.text = life_Current.ToString();


    }


    private void Update()
    {
        //if (!isAwake)
        //{
        //    regeneratLife();
        //}


        //if (life_Current == life_Max)
        //{
        //    //time.text = "Full";

        //}
        //else
        //{
        //    //   time.text = showLifeTimeInMinutes();

        //}

        //lifeUpdateTime = PlayerPrefs.GetString("LifeUpdateTime");
        //timerCounter = PlayerPrefs.GetFloat("timer").ToString();
    }




    private void regeneratLife()
    {

        if (life_Current < life_Max)
        {
            isLifeRegenerating = true;
            hasMaxLife = false;
            timer_counter += Time.deltaTime;
            if (timer_counter > time_PerLife)
            {
                life_Current += 1;
                if (life_Current > life_Max)
                {
                    life_Current = life_Max;

                    timer_counter = 0;
                }
                PlayerPrefs.SetInt("life", life_Current);
                life_Current = PlayerPrefs.GetInt("life");
                lifetxt.text = life_Current.ToString();
                //Save.instance.Set_Life(life_Current);
                timer_counter = 0;
                //  UpdateLives(timer_counter);

            }


        }
        else
        {
            isLifeRegenerating = false;
            hasMaxLife = true;

        }




    }

    void UpdateLivesForPause(float timerToAdd)
    {
        //if (life_Current < life_Max)
        //{
        //    int livesToAdd = Mathf.FloorToInt(timerToAdd / time_PerLife);
        //   //  timer_counter = (float)timerToAdd % time_PerLife;
        //    life_Current += livesToAdd;

        //    if (life_Current > life_Max)
        //    {
        //        life_Current = life_Max;
        //        lifetxt.text = life_Current.ToString();
        //        timer_counter = 0;
        //    }
        //    PlayerPrefs.SetInt("life", life_Current);
        //   PlayerPrefs.SetString("LifeUpdateTime", DateTime.Now.ToString());

        //}

        if (timerToAdd > timer_counter)
        {
            int livesToAdd = Mathf.FloorToInt(timerToAdd / time_PerLife);
            life_Current += livesToAdd;

            if (life_Current > life_Max)
            {
                life_Current = life_Max;
                lifetxt.text = life_Current.ToString();
                timer_counter = 0;
            }
        }
        else
        {
            timer_counter += timerToAdd;
        }
        lifetxt.text = life_Current.ToString();
        //PlayerPrefs.SetInt("life", life_Current);
        //life_Current = PlayerPrefs.GetInt("life");
    }

    void UpdateLivesForAwake(float timerToAdd)
    {
        //if (life_Current < life_Max)
        //{
        //    int livesToAdd = Mathf.FloorToInt(timerToAdd / time_PerLife);
        //   //  timer_counter = (float)timerToAdd % time_PerLife;
        //    life_Current += livesToAdd;

        //    if (life_Current > life_Max)
        //    {
        //        life_Current = life_Max;
        //        lifetxt.text = life_Current.ToString();
        //        timer_counter = 0;
        //    }
        //    PlayerPrefs.SetInt("life", life_Current);
        //   PlayerPrefs.SetString("LifeUpdateTime", DateTime.Now.ToString());

        //}

        if (timerToAdd > timer_counter)
        {
            int livesToAdd = Mathf.FloorToInt(timerToAdd / time_PerLife);
            life_Current += livesToAdd;

            if (life_Current > life_Max)
            {
                life_Current = life_Max;
                lifetxt.text = life_Current.ToString();
                timer_counter = 0;
            }
        }
        else
        {
            timer_counter = timerToAdd;
        }

        isAwake = false;
        lifetxt.text = life_Current.ToString();
        PlayerPrefs.SetInt("life", life_Current);
        life_Current = PlayerPrefs.GetInt("life");
    }





    public string showLifeTimeInMinutes()
    {
        float timeLeft = time_PerLife - (float)timer_counter;
        int min = Mathf.FloorToInt(timeLeft / 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        return min + ":" + sec.ToString("00");
    }


    public void RewardLife_Ad_FN()
    {
        if (life_Current != life_Max)
        {
            life_Current += 1;
            PlayerPrefs.SetInt("life", life_Current);

            life_Current = PlayerPrefs.GetInt("life");

        }
    }

    public void DecreaseLife()
    {
        int i =life_Current;
        i -= 1;
        Debug.Log(i + "=Life Decrease");
        //PlayerPrefs.SetInt("life", i);
        life_Current = i;
        lifetxt.GetComponent<Animator>().Rebind();
        lifetxt.GetComponent<Animator>().Update(0);
        lifetxt.GetComponent<Animator>().Play("lifeTextAnim");
        lifetxt.text = life_Current.ToString();
    }


}
