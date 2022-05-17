using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableInstance : MonoBehaviour
{
    public static ConsumableInstance instance;
    public Player playerScript;
    [Header("UI Information")]
    public Text totlaMagnettxt;
    public Text totalFruittxt;
    public Text total2XMultipliertxt;    
    public GameObject blockPanel;
    public Button magnetBT;
    public Button fruitBT;
    public Button scoreMultiplierBT;




    [Header("Magnet Settings")]
    public float magnettime;
    public float magnetTimeCountdown;
    public bool isMagnetActive;
    public GameObject coinDetecterobject;
    public GameObject magnetImage;
    public Text magneticTimeText;

    [Header("Fruit Settings")]
    public float fruitTime;
    public float fruitCountDownTime;
    public bool isFruitActive;
    public GameObject fruitImage;
    public Text fruitText;
    public Camera camera;

    [Header("ScoreMultiplier Settings")]
    public float scoreMultiplierTime;
    public float scoreMultiplierCountdown;
    public bool isScoreMultiplierActive;
    public GameObject scoreMultiplierImage;
    public Text scoreMultiplierTimeText;

    [Header("Restricted Settings")]
    public bool isConsumbaleActive;
    public float coolDownTime;
    public int total_Magnet;
    public int total_Fruit;
    public int total_Score2xMultiplier;
    public AudioSource source;


    [Header("Consumable Spawn Settings")]
    public int RandomMin;
    public int RandomMax;
    public int SpawnRangeMin;
    public int SpawnRangeMax;


    


    public Text GodMode;

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


    }
    private void Start()
    {
        load_Consumable();
    }

    private void Update()
    {
        if (isFruitActive)
        {

            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView  , 110f, 0.1f);
        
        
        }
        if (!isFruitActive)
        {

            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 90f, 0.1f);


        }
    }
    public void use_Magnet()
    {
        if (total_Magnet != 0)
        {
            Magnet_fn();
            total_Magnet -= 1;
            Save.instance.Set_Magnet_Info(total_Magnet);
           
            load_Consumable();
        }


    }

    public void use_Fruit()
    {
        if (total_Fruit != 0)
        {
            fruit_fn();
            total_Fruit -= 1;
            Save.instance.Set_Fruit_Info(total_Fruit);
            load_Consumable();

        }


    }

    public void use_ScoreMultiplier()
    {
        if (total_Score2xMultiplier != 0)
        {

            scoreMultiplier_Fn();
            total_Score2xMultiplier -= 1;
            Save.instance.Set_Multiplier_Info(total_Score2xMultiplier);
            load_Consumable();

        }

    }




    public void Magnet_fn()
    {
        magnetTimeCountdown = magnettime;
        coinDetecterobject.SetActive(true);
        magnetImage.SetActive(true);
        isMagnetActive = true;
       // playerScript.magnetVFX_fn(true);
        source.Play();
        InvokeRepeating("DisplayMagnetTime", 0f, 1f);    

      

    }   
      
    void DisplayMagnetTime()
    {
        if (!GameController.instance.isPlayerDead)
        {
            magnetTimeCountdown -= 1;
            if (magnetTimeCountdown < 0)
            {
                if (total_Magnet == 0)
                {
                    magnetBT.interactable = false;

                }
                else
                {
                    magnetBT.interactable = true;
                }
                coinDetecterobject.SetActive(false);
                magnetImage.SetActive(false);
                isMagnetActive = false;
              //  playerScript.magnetVFX_fn(false);

                CancelInvoke("DisplayMagnetTime");

            }
            else
            {
                magnetBT.interactable = false;
                magneticTimeText.text = "00:" + magnetTimeCountdown.ToString();


            }
        }

        else
        {

            if (total_Magnet == 0)
            {
                magnetBT.interactable = false;

            }
            else
            {
                magnetBT.interactable = true;
            }
            magnetTimeCountdown = 0;
            coinDetecterobject.SetActive(false);
            magnetImage.SetActive(false);
            isMagnetActive = false;
           // playerScript.magnetVFX_fn(false);

            CancelInvoke("DisplayMagnetTime");
        }
    }


    public void fruit_fn()
    {
        
        
        fruitCountDownTime = fruitTime;
        isFruitActive = true;
        fruitImage.SetActive(true);
       // playerScript.InvincibilityVFX_fx(true);
        source.Play();
        InvokeRepeating("fruitCountDown",0f,1f);     

    
    
    }

    void fruitCountDown()
    {
        if (!GameController.instance.isPlayerDead)
        {
            fruitCountDownTime -= 1;
            if (fruitCountDownTime < 0)
            {
                if (total_Fruit == 0)
                {
                    fruitBT.interactable = false;

                }
                if (total_Fruit != 0)
                {
                    fruitBT.interactable = true;

                }
                isFruitActive = false;
                fruitImage.SetActive(false);
              //  playerScript.InvincibilityVFX_fx(false);
                CancelInvoke("fruitCountDown");

            }
            else
            {
                fruitBT.interactable = false;
                fruitText.text = "00:" + fruitCountDownTime.ToString();

            }
        }
        else
        {
            resetFruit();

            
        }
    }

    public void resetFruit()
    {

        fruitCountDownTime = 0;

        if (total_Fruit == 0)
        {
            fruitBT.interactable = false;

        }
        if (total_Fruit != 0)
        {
            fruitBT.interactable = true;

        }
        isFruitActive = false;
        fruitImage.SetActive(false);
       // playerScript.InvincibilityVFX_fx(false);
        CancelInvoke("fruitCountDown");


    }

    public void scoreMultiplier_Fn()
    { 
        scoreMultiplierCountdown = scoreMultiplierTime;
        isScoreMultiplierActive = true;
        scoreMultiplierImage.SetActive(true);
       // playerScript.scoreMultiplierVFX_fn(true);
        source.Play();
        InvokeRepeating("ScoreMultiplierCountdown",0f,1f);
       
    }

    void ScoreMultiplierCountdown()
    {
        if (!GameController.instance.isPlayerDead)
        {
            scoreMultiplierCountdown -= 1;
            if (scoreMultiplierCountdown < 0)
            {
                if (total_Score2xMultiplier == 0)
                {
                    scoreMultiplierBT.interactable = false;

                }
                else
                {
                    scoreMultiplierBT.interactable = true;

                }
                isScoreMultiplierActive = false;
                scoreMultiplierImage.SetActive(false);
              //  playerScript.scoreMultiplierVFX_fn(false);
                CancelInvoke("ScoreMultiplierCountdown");

            }
            else
            {
                scoreMultiplierBT.interactable = false;
                scoreMultiplierTimeText.text = "00:" + scoreMultiplierCountdown.ToString();

            }

        }
        else
        {


            scoreMultiplierCountdown =0;
           
                if (total_Score2xMultiplier == 0)
                {
                    scoreMultiplierBT.interactable = false;

                }
                else
                {
                    scoreMultiplierBT.interactable = true;

                }
                isScoreMultiplierActive = false;
                scoreMultiplierImage.SetActive(false);
              //  playerScript.scoreMultiplierVFX_fn(false);
                CancelInvoke("ScoreMultiplierCountdown");

            



        }
    }




    void coolDown_Fn()
    {
        StartCoroutine(CoolDownTime());
    
    }
    IEnumerator CoolDownTime()
    {
        blockPanel.SetActive(true);
        yield return new WaitForSeconds(coolDownTime);
        blockPanel.SetActive(false);
    
    }

    public void load_Consumable()
    {

        total_Magnet = Save.instance.Get_Magnet_Info();
        total_Fruit = Save.instance.Get_Fruit_Info();
        total_Score2xMultiplier = Save.instance.Get_Multiplier_Info();
        updateUI();

    }
    public void updateUI()
    {

        totlaMagnettxt.text = total_Magnet.ToString();
        totalFruittxt.text = total_Fruit.ToString();
        total2XMultipliertxt.text = total_Score2xMultiplier.ToString();
        if (total_Magnet == 0)
        {
            magnetBT.interactable = false;

        }
        if (total_Magnet != 0)
        {
            magnetBT.interactable = true;
        }

        if (total_Fruit == 0)
        {
            fruitBT.interactable = false;
        
        }
        if (total_Fruit != 0)
        {
            fruitBT.interactable = true;

        }

        if (total_Score2xMultiplier == 0)
        {
           scoreMultiplierBT.interactable = false;
        
        }
        if (total_Score2xMultiplier != 0)
        {
            scoreMultiplierBT.interactable = true;

        }

    }


    public void godMode()
    {
        if (isFruitActive)
        {

            isFruitActive = false;
            GodMode.text = "God Mode Off";
        }
        else if (!isFruitActive)
        {

            isFruitActive = true;
            GodMode.text = "God Mode On";
        }
    
    }
}
