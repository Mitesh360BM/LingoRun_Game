using UnityEngine;
using System.Collections;

[ExecuteAlways]
public class CurvedControls : MonoBehaviour
{
    public bool BendOn;

    [Header("Bending Materials")]
    public Material[] Mats;


   // public Vector4 Offset;

    [Header("Bend Settings")]
    public float minBend;
    public int randomNumber;
    public float bendSpeed;
   // [Range(-100f, 100f)]
    public float X, Y, Z, W;

    private void Start()
    {
        InvokeRepeating("XPosvalue", 0f, 0.01f);
    }

    private void Update()
    {
        if (BendOn)
        {
            foreach (Material M in Mats)
            {

                // M.SetVector("_QOffset", Offset);
                M.SetVector("_QOffset", new Vector4(X, Y, Z, W));
            }
        }
        else
        {

            foreach (Material M in Mats)
            {
                M.SetVector("_QOffset", new Vector4(0, 0, 0, 0));
            }

        }
    }

    bool isActive;
    void XPosvalue()
    {
        if (X <= minBend)
        {
            X += bendSpeed;

        }
        else
        {
            InvokeRepeating("XNeg", 0.01f, 0.01f);
            CancelInvoke("XPosvalue");

        }


    }

    void XNeg()
    {

        if (X >= -minBend)
        {
            X -= bendSpeed;

        }
        else
        {
            InvokeRepeating("XPosvalue", 0.01f, 0.01f);
            CancelInvoke("XNeg");

        }

    }
}