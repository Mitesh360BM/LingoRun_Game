using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScroll : MonoBehaviour
{

   



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
           // Debug.Log("Collider");
            StartCoroutine(RecycleFloor());

        }

    }

    IEnumerator RecycleFloor()
    {

        if (!GameController.instance.isPlayerDead)
            PlatformController.instance.RecycleGameObject();
        yield return new WaitForSeconds(0.5f);
        if (!GameController.instance.isPlayerDead)
            this.gameObject.SetActive(false);



    }



}
