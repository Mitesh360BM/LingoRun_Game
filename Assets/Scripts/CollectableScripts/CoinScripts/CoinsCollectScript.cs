using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollectScript : MonoBehaviour
{


    public Animator animator;

    public Transform playerTransform;
    public float moveSpeed = 17f;

    public CoinMove coinMoveScript;

    private void Awake()
    {
        coinMoveScript = gameObject.GetComponent<CoinMove>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CoinDetecter")
        {
            animator.enabled = false;
            coinMoveScript.enabled = true;
        }



        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            EventController.instance.coinCollectEvent_fn();
        }

    }




}
