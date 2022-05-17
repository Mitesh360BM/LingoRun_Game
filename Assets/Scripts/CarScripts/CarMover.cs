using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    public DifficultyController playerDifficulty;
    public float moveSpeed, lifetime;
   
   public bool isEnable;
    private void OnEnable()
    {
        playerDifficulty = GameObject.Find("PlayerDifficulty").GetComponent<DifficultyController>();
        isEnable = true;
        if (playerDifficulty.isEasy)
        {
            moveSpeed = 1.0f;
            lifetime = 10f;

        }
        if (playerDifficulty.isMedium)
        {
            moveSpeed = 1.15f;
            lifetime =5f;
        }

        if (playerDifficulty.isHard)
        {


            moveSpeed = 1.25f;
            lifetime = 2f;
        }
        StartCoroutine(disable());
    }

    private void FixedUpdate()
    {
        if (isEnable)
        {

           
            var move = new Vector3(0, 0, -1);
            transform.position += move * moveSpeed;
        }
    }
    IEnumerator disable()
    {
        yield return new WaitForSeconds(lifetime);
        //this.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        Destroy(gameObject, 0.5f);
    }

}
