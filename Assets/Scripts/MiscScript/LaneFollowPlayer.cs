using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneFollowPlayer : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x,player.transform.position.y,player.transform.position.z);
    }
}
