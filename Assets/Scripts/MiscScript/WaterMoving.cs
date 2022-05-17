using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMoving : MonoBehaviour
{
    
    public Renderer rend;
    public float speed;
    float step;
    float temp;


    private void Update()
    {
        step = speed + temp;
        temp = step;

       

        rend.material.mainTextureOffset = new Vector2(step,0);
        
    }
}
