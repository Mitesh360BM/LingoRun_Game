using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyStreakScript : MonoBehaviour
{
    [SerializeField]
    private DateTime CurrentDate, PreviousDate;

    [SerializeField]
    private int dayDifference;

    public int currentDailyStreak { get; private set; }

    private void Awake()
    {
       // Debug.Log(DateTime.Today);
        StartCoroutine(checkForSavedPreviousDate());

    }

   
    IEnumerator checkForSavedPreviousDate()
    {
        yield return new WaitUntil(() => Save.instance != null);
        if (Save.instance.HasKey(Save.ds_previousDate))
        {
            PreviousDate = DateTime.Parse(Save.instance.GetString(Save.ds_previousDate));
            calculateDayDiff();

        }
        else
        {
            PreviousDate = DateTime.Today;
            Save.instance.SetString(Save.ds_previousDate, PreviousDate.ToString());
            // dayDifference = 0;
            currentDailyStreak = 1;
            Save.instance.SetInt(Save.ds_dailyStreak, currentDailyStreak);
            setDailyStreak();
        }
    }

    private void calculateDayDiff()
    {
        if (PreviousDate != DateTime.Today)
        {
            CurrentDate = DateTime.Today;
            dayDifference = int.Parse((CurrentDate - PreviousDate).TotalDays.ToString());
            if (dayDifference > 0)
            {
                checkForDailyStreaks();
            }

        }
        else if (PreviousDate == DateTime.Today)
        {
            currentDailyStreak = int.Parse(Save.instance.GetInt(Save.ds_dailyStreak).ToString());
            setDailyStreak();
        }

    }


    private void checkForDailyStreaks()
    {
        if (Save.instance.HasKey(Save.ds_dailyStreak))
        {

            currentDailyStreak = int.Parse(Save.instance.GetInt(Save.ds_dailyStreak).ToString());

        }



        if (dayDifference <= 7 && dayDifference >= 1)
        {
            if (currentDailyStreak <= 10)
            {
                currentDailyStreak += 1;
            }
        }
        else if (dayDifference > 7)
        {
            currentDailyStreak = 1;
        }

        Save.instance.SetString(Save.ds_previousDate, DateTime.Today.ToString());
        Save.instance.SetInt(Save.ds_dailyStreak, currentDailyStreak);
        setDailyStreak();

    }

    private void setDailyStreak()
    {

        PlayerDataController.instance.DailyStreak = currentDailyStreak;
    
    }

    private void OnApplicationFocus(bool focus)
    {
       // Debug.Log(focus);
        if (!focus)
        {


        }
    }
}