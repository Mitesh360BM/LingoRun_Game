using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUIinstance : MonoBehaviour
{
    public bool testMode;
    public Image powerSprite;

    public float powerTime;
    public Text TimeText;
    private float exeTime;
    private void OnEnable()
    {

        InvokeRepeating("startTimer", 0f, 1f);
       // StartCoroutine(startTime());
    }

    private IEnumerator startTime()
    {
        if (GameController.instance.isPlayerDead)
        {
            StopAllCoroutines();
            DestroyImmediate(gameObject);
        }

        while(powerTime>0)
        {
            powerTime -= 1;
            yield return new WaitForSecondsRealtime(1f);
            TimeText.text = powerTime.ToString();
        }
        yield return new WaitUntil(() => powerTime == 0);
        TimeText.text = powerTime.ToString();
        StopAllCoroutines();
        DestroyImmediate(gameObject);

    }

    private void startTimer()
    {
        if(GameController.instance.isPlayerDead)
            DestroyImmediate(gameObject);
        if (powerTime > 0)
        {
            powerTime -= 1;
            TimeText.text = powerTime.ToString();
        }
        else
        {
            TimeText.text = powerTime.ToString();
            if (!testMode)
                DestroyImmediate(gameObject);


        }


    }
    private IEnumerator Starttimer()
    {
       
        while (powerTime != 0)
        {
            yield return new WaitForSeconds(1f);
           

        }
       
           


    }


}
