using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFov : MonoBehaviour
{
    public Camera camera;

    // Update is called once per frame
    void Update()
    {
        if (BikePower.isBikeActive || SkatePower.isSkateActive || FlyingPower.isFlyingActive)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 70f, 0.1f);

        }
        else
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 60f, 0.1f);

        }
    }
}
