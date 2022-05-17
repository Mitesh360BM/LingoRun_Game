using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsScript : MonoBehaviour
{


    bool eventInvoked;

    public GameObject MeshOb;
  
    public enum TrapType
    {

        Train,
        Car,
        Barricade,
        PitHole

    }
    public TrapType trapType;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(SkatePower.isSkateActive +"/"+ HulkPower.ishulkActive + "/" + BikePower.isBikeActive);
        if (other.gameObject.tag == "Player" && !GameController.instance.isPlayerDead && !eventInvoked)
        {
          
            if (SkatePower.isSkateActive || HulkPower.ishulkActive || BikePower.isBikeActive /*&& trapType != TrapType.PitHole*/)
            {
                eventInvoked = true;
                EventController.instance.explosionEvent_fn();

                //if (trapType == TrapType.Train)
                //{
                //    EventController.instance.playerDead_fn();
                //}
                if (trapType == TrapType.Barricade || trapType == TrapType.Train || trapType == TrapType.Car)
                {
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
                    MeshOb.SetActive(false);
                    StartCoroutine(enableBarrier());
                }
            }
            else if(!SkatePower.isSkateActive || !HulkPower.ishulkActive || !BikePower.isBikeActive)
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(enableBarrier());
                eventInvoked = true;
                if (trapType == TrapType.Barricade || trapType == TrapType.Train || trapType == TrapType.Car /*&& eventInvoked == false*/)
                {
                    // eventInvoked = true;
                    EventController.instance.playerDead_fn();
                    Debug.Log("Dead");
                   // StartCoroutine(enableBarrier());
                }
                else if (trapType == TrapType.PitHole/* && eventInvoked == false*/)
                {
                  
                    Debug.Log("Stunned");
                    EventController.instance.stumble_fn();
                }
                //else if (trapType == TrapType.Train)
                //{
                //    EventController.instance.playerDead_fn();
                //    // gameObject.GetComponent<CarMover>().isEnable = false;
                //    // this.gameObject.SetActive(false);
                //}
            }


        }
        //if (other.gameObject.tag == "Traps" && trapType == TrapType.Train)
        //{
        //    // gameObject.GetComponent<CarMover>().isEnable = false;
        //    //  StartCoroutine(enableBarrier());
        //}
    }

    private void OnCollisionExit(Collision collision)
    {

        eventInvoked = false;
    }

    private void OnTriggerEnter(Collider other)
    {



    }



    private void OnTriggerExit(Collider other)
    {
    }


    IEnumerator enableBarrier()
    {
       // yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitUntil(() => !GameController.instance.isPlayerDead);
        yield return new WaitForSeconds(2f);
      //  this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitUntil(() => !GameController.instance.isContinueGame);
        yield return new WaitForSeconds(1f);
       // this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        MeshOb.SetActive(true);
    }

}
