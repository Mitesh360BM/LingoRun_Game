using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSwitchFix : MonoBehaviour
{
    [SerializeField]  private Player player;

    [SerializeField]  private bool isLeft,isMiddle,isRight;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Traps")
        {
            if (isLeft)
            {

                player.canSwitchLeft = false;
            }
            else if (isRight)
            {
                player.canSwitchRight = false;
            
            }
            else if(isMiddle)
            {

                player.canSwitchMiddle = false;
            }
       // GetComponent<MeshRenderer>().enabled = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Traps")
        {
            if (isLeft)
            {

                player.canSwitchLeft = false;
            }
            else if (isRight)
            {
                player.canSwitchRight = false;

            }
            else if (isMiddle)
            {

                player.canSwitchMiddle = false;
            }
          //  GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
       
            if (isLeft)
            {

                player.canSwitchLeft = true;
            }
            else if (isRight)
            {
                player.canSwitchRight = true;

            }
            else if (isMiddle)
            {

                player.canSwitchMiddle = true;
            }
          //  GetComponent<MeshRenderer>().enabled = false;

        
    }
}
