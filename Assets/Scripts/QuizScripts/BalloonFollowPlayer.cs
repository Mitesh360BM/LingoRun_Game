using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFollowPlayer : MonoBehaviour
{

    public GameObject playerObject;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y+ 53, playerObject.transform.position.z+42);
        
    }
}
