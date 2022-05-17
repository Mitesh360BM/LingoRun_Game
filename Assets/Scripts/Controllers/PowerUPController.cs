using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPController : MonoBehaviour
{

    public static PowerUPController instance;

    public int maxPowerInUse;
    public bool canUsePower;

    [Header("PowerUP GameObjects")]
    public GameObject magnetObject;
    public GameObject slowMotionObject;
    public GameObject bikeObject;
    public GameObject hulkObject;
    public GameObject flyingObject;
    public GameObject skateObject;

    //[Header("PowerUP Status")]
    //public bool isMagnetActive;
    //public bool isBikeActive;
    //public bool isFlyingActive;

    [Header("Consumable Spawn Settings")]
    public int RandomMin;
    public int RandomMax;
    public int SpawnRangeMin;
    public int SpawnRangeMax;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        canUsePower = true;
    }

    public void setMaxPowerInUse()
    {
        maxPowerInUse += 1;
        checkMaxPowerinUse();
    }

    public void setMinPowerInUse()
    {
        maxPowerInUse -= 1;
        checkMaxPowerinUse();
    }
    private void checkMaxPowerinUse()
    {
        if (maxPowerInUse <= 0)
        {
            maxPowerInUse = 0;
            canUsePower = true;
        }
        else if(maxPowerInUse<2)
        {
            canUsePower = true;

        }
        else if (maxPowerInUse >= 2)
        {
            maxPowerInUse = 2;
            canUsePower = false;
        }


    }

    public void OnRoadmagnet()
    {
        magnetObject.GetComponent<MagnetPower>().UseByController();
        // enableObject(magnetObject);
    }

    public void OnRoadSloMo()
    {
        // enableObject(slowMotionObject);
        slowMotionObject.GetComponent<SlowMotionPower>().UseByController();
    }
    public void OnRoadBike()
    {
        //  enableObject(bikeObject);
        bikeObject.GetComponent<BikePower>().UseByController();

    }
    public void OnRoadHulk()
    {

        // enableObject(hulkObject);
        hulkObject.GetComponent<HulkPower>().UseByController();

    }
    public void OnRoadFlying()
    {
        // enableObject(flyingObject);
        flyingObject.GetComponent<FlyingPower>().UseByController();
    }

    public void OnRoadSkate()
    {
        skateObject.GetComponent<SkatePower>().UseByController();

    }
    private void enableObject(GameObject gameObject)
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);

    }

}




