using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform parent;

  

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "Coin")
        {

           Destroy(other.gameObject);
        
        }

        if (other.gameObject.tag == "Traps")
        {
           // Debug.Log("collide");
            Destroy(other.gameObject);

        }
    }

}
