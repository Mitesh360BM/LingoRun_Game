using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameReset : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isPlayerDead)
        {
            Destroy(this.gameObject);
        
        }
        
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
        {

            Destroy(gameObject);
        }
    }
}
