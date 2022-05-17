using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RotationAnim : MonoBehaviour
{
    public bool x, y, z;
    public float speed;

    private void FixedUpdate()
    {
        if (x)
        {
            y = false;
            z = false;
            transform.Rotate(Vector3.left * speed);
        
        }
        else if(y)
        {

            x = false;
            z = false;

            transform.Rotate(Vector3.up * speed);
        }
        else if (z)
        {

            y = false;
            x = false;

            transform.Rotate(Vector3.forward * speed);
        }
    }

}
