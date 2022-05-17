using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerPurchaseAtStart : MonoBehaviour
{

    [Header("Panel Settings")]
    [SerializeField]
    private float timeToDisable;

    [Header("PowerList")]

    [SerializeField]
    private List<PowerClass> powerClass = new List<PowerClass>();


    [Header("Reference")]
    [SerializeField] private Image powerImage;
    [SerializeField] private Image powerNameImage;

    [SerializeField] private Image buttonSprite;

    [SerializeField] private Sprite freeSprite;

    [SerializeField] private Sprite buySprite;

    [SerializeField] private bool isFree;

    [SerializeField] private string power;
    [SerializeField] private int cost;


    public void setPower(string powerName, bool isFree, int cost)
    {
        this.power = powerName;
        isFree = this.isFree;
        cost = this.cost;

        SetPowerData();
    }

    private void OnEnable()
    {
        //setPower(Save.magnetPower, true, 0);
        PowerUPController.instance.canUsePower = false;

        gameObject.transform.DOMoveY(26f, 1f).OnComplete(() =>
        {

            Invoke("Animate", timeToDisable);


        });

    }

    private void Animate()
    {
        gameObject.transform.DOMoveY(-124f, 1f).OnComplete(() =>
        {

            PowerUPController.instance.canUsePower = true;
            gameObject.SetActive(false);

        });



    }

    private void SetPowerData()
    {
        powerClass.ForEach((data) =>
        {
            if (data.powerName == power)
            {
                powerImage.sprite = data.powerImageSprite;
                powerNameImage.sprite = data.powerNameSprite;
                if (isFree)
                {
                    buttonSprite.sprite = freeSprite;
                }
                else if (!isFree)
                {
                    buttonSprite.sprite = buySprite;
                }
            }
        });
    }

    public void OnClick_Buy_UsePower()
    {
        if (isFree)
        {

        }
        else if (!isFree)
        {



        }
        if (power == Save.magnetPower)
        {
            PowerUPController.instance.OnRoadmagnet();
        }
        else if (power == Save.hulkPower)
        {
            PowerUPController.instance.OnRoadHulk();
        }
        else if (power == Save.flyingPower)
        {
            PowerUPController.instance.OnRoadFlying();
        }
        else if (power == Save.bikerPower)
        {
            PowerUPController.instance.OnRoadBike();
        }
        else if (power == Save.sloMoPower)
        {
            PowerUPController.instance.OnRoadSloMo();
        }
        else if (power == Save.skatePower)
        {
            PowerUPController.instance.OnRoadSkate();
        }

        gameObject.SetActive(false);
    }
}

[Serializable]
public class PowerClass
{
    public string powerName;
    public Sprite powerImageSprite;
    public Sprite powerNameSprite;

}