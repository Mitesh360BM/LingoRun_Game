using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float camera_zOffset = 17.57f;
    public float camera_yOffset = 8.56f;

    public GameObject playerGameObject;
    public GameObject Camera;

    public GameObject wonRotate;
    private void FixedUpdate()
    {
        CameraFollowPlayer();
    }
    private void LateUpdate()
    {
        
    }
    void CameraFollowPlayer()
    {
        if (GameController.instance.isGameStart || GameController.instance.isContinueGame )
        {

            Camera.transform.position =
                    new Vector3(Mathf.Lerp(Camera.transform.position.x, playerGameObject.transform.position.x, 0.1f),
                    Mathf.Lerp(Camera.transform.position.y, transform.position.y + camera_yOffset, 0.05f),
                    Mathf.Lerp(Camera.transform.position.z, transform.position.z - camera_zOffset, 0.5f));
          //  Camera.transform.eulerAngles = new Vector3 (10, 0, 0);
        }

    }
}
