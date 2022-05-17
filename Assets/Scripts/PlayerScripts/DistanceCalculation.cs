using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculation : MonoBehaviour
{
    public GameObject staticObject;
    private float distance;
    // Update is called once per frame
    void Update()
    {
        float f = Vector3.Distance(staticObject.transform.position, transform.position);
       // Debug.Log("Distance: " + (f/10));
        distance = f;
        ScreenController.instance.DistanceText.text = distance.ToString("0.0");
        PlayerDataController.instance.DistanceCovered = int.Parse(distance.ToString("0"));
    }
}
