using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTypeScript : MonoBehaviour
{
   

    public PowerType powerType;

    private void Start()
    {
      
    }

    private void Update()
    {
        if (PowerUPController.instance.canUsePower)
        {
            if (powerType == PowerType.Magnet)
            {
                if (MagnetPower.isMagnetActive)
                {
                    Destroy(gameObject);
                }

            }

            if (powerType == PowerType.Bicycle)
            {
                if (BikePower.isBikeActive || FlyingPower.isFlyingActive || SkatePower.isSkateActive || HulkPower.ishulkActive)
                {
                    Destroy(gameObject);
                }

            }
            if (powerType == PowerType.JetPlane)
            {
                if (BikePower.isBikeActive || FlyingPower.isFlyingActive || QuizController.instance.isQuestionVisible || SkatePower.isSkateActive || HulkPower.ishulkActive)
                {
                    Destroy(gameObject);
                }

            }
            if (powerType == PowerType.Skateboard)
            {
                if (BikePower.isBikeActive || FlyingPower.isFlyingActive || SkatePower.isSkateActive || HulkPower.ishulkActive)
                {
                    Destroy(gameObject);
                }

            }
            if (powerType == PowerType.Hulk)
            {
                if (BikePower.isBikeActive || FlyingPower.isFlyingActive || SkatePower.isSkateActive || QuizController.instance.isQuestionVisible || HulkPower.ishulkActive)
                {
                    Destroy(gameObject);
                }

            }
        }
        else {

            Destroy(gameObject);

        }
    }

   
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            if (powerType == PowerType.Magnet)
            {
                PowerUPController.instance.OnRoadmagnet();
            }
            if (powerType == PowerType.Slow_motion)
            {
                PowerUPController.instance.OnRoadSloMo();
            }
            if (powerType == PowerType.Bicycle)
            {
                PowerUPController.instance.OnRoadBike();
            }
            if (powerType == PowerType.Hulk)
            {
                PowerUPController.instance.OnRoadHulk();
            }
            if (powerType == PowerType.JetPlane)
            {
                PowerUPController.instance.OnRoadFlying();
            }
          
            if (powerType == PowerType.Skateboard)
            {
                PowerUPController.instance.OnRoadSkate();
            }
        }
    }


    private void objectVisibility(bool b)
    { 
    
    
    
    }


}
