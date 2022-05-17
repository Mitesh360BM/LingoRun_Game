using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    [Header("Cost Settings")]
    public int perMagnetCost;
    public int perFruitCost;
    public int score2XMultiplierCost;

    [Header("Purchase Amount Limt")]
    public int magnetPurchaseLimit;
    public int fruitPurchaseLimit;
    public int score2XmultiplierPurchaseLimit;

    [Header("Points & Consumable Information")]
    public int total_Points;
    public int total_Magnet;
    public int total_Fruit;
    public int total_Score2xMultiplier;


    [Header("UI Information")]
    public Text totalPointstxt;
    public Text totlaMagnettxt;
    public Text totalFruittxt;
    public Text total2XMultipliertxt;
    private void Start()
    {
        total_Points = Save.instance.Get_Score();
        load_ConsumableInformation();
        updateUI();
    }

    private void OnEnable()
    {
        total_Points = Save.instance.Get_Score();
        load_ConsumableInformation();
        updateUI();
    }

    public void purchase_magnet_Fn()
    {
     
        if (total_Points >= perMagnetCost)
        {
            if (total_Magnet >= 0 && total_Magnet != magnetPurchaseLimit)
            {
                total_Points = total_Points - perMagnetCost;
                total_Magnet += 1;
                save_MagnetInformation();
                updateTotalScore();
                updateUI();
            }

        }
        else
        {
            Debug.Log("Not enough points");
        }


    }



    public void purchase_fruit_Fn()
    {
     
        if (total_Points >= perFruitCost)
        {
            if (total_Fruit>=0 && total_Fruit != fruitPurchaseLimit)
            {
                total_Points = total_Points - perFruitCost;
                total_Fruit += 1;
                save_FruitInformation();
                updateTotalScore();
                updateUI();
            }

        }
        else
        {
            Debug.Log("Not enough points");
        }

    }


    public void purchase_2XMultiplier_Fn()
    {
       
        if (total_Points >= score2XMultiplierCost)
        {
            if ( total_Score2xMultiplier>=0 && total_Score2xMultiplier != score2XmultiplierPurchaseLimit)
            {
                total_Points = total_Points - score2XMultiplierCost;
                total_Score2xMultiplier += 1;
                save_MultiplierInformation();
                updateTotalScore();
                updateUI();
            }

        }
        else
        {
            Debug.Log("Not enough points");
        }
    }

    //void getScore()
    //{
    //    total_Points = GameInstance.instance.totalScore;

    //}

    void updateTotalScore()
    {

      //  GameInstance.instance.totalScore = total_Points;
      //  GameInstance.instance.SaveTotalScore();
       // GameInstance.instance.LoadScore();
    }


    void save_MagnetInformation()
    {
        Save.instance.Set_Magnet_Info(total_Magnet);
       // PlayerPrefs.SetInt("totalMagnet", total_Magnet);
    
    }

    void save_FruitInformation()
    {
        Save.instance.Set_Fruit_Info(total_Fruit);
       // PlayerPrefs.SetInt("totalFruit",total_Fruit);
    }

    void save_MultiplierInformation()
    {
        //  PlayerPrefs.SetInt("totalscore2x",total_Score2xMultiplier);
        Save.instance.Set_Multiplier_Info(total_Score2xMultiplier);
    }


    void load_ConsumableInformation()
    {
        // total_Magnet = PlayerPrefs.GetInt("totalMagnet");
        // total_Fruit = PlayerPrefs.GetInt("totalFruit");
        //  total_Score2xMultiplier = PlayerPrefs.GetInt("totalscore2x");
        total_Magnet = Save.instance.Get_Magnet_Info();
        total_Fruit = Save.instance.Get_Fruit_Info();
        total_Score2xMultiplier = Save.instance.Get_Multiplier_Info();
    }

    void updateUI()
    {
        totalPointstxt.text = total_Points.ToString() ;
        totlaMagnettxt.text = total_Magnet.ToString();
        totalFruittxt.text = total_Fruit.ToString() ;
        total2XMultipliertxt.text = total_Score2xMultiplier.ToString();
    
    
    }


    public void RewardedAd_fn()
    {
        total_Points = total_Points+ 50;
       
        updateTotalScore();
        updateUI();


    }
}

