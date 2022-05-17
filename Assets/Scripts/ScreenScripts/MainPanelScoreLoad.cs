using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelScoreLoad : MonoBehaviour
{
    [Header("Points & Consumable Information")]
    public int total_Points;
    public int total_Magnet;
    public int total_Fruit;
    public int total_Score2xMultiplier;

    [Header("UI Components")]
    public Text totalPointstxt;
    public Text totlaMagnettxt;
    public Text totalFruittxt;
    public Text total2XMultipliertxt;
    private void OnEnable()
    {
        Invoke("load_Score", 0.1f);
    }
    void load_Score()
    {
        total_Points = Save.instance.Get_Score();
        total_Magnet = Save.instance.Get_Magnet_Info();
        total_Fruit = Save.instance.Get_Fruit_Info();
        total_Score2xMultiplier = Save.instance.Get_Multiplier_Info();
        updateUI();
    }

    void updateUI()
    {
        totalPointstxt.text = total_Points.ToString();
        totlaMagnettxt.text = total_Magnet.ToString();
        totalFruittxt.text = total_Fruit.ToString();
        total2XMultipliertxt.text = total_Score2xMultiplier.ToString();


    }

}
