using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    private void OnEnable()
    {
        Debug.Log("Enable");
    }
    public GameObject go;
    private void Start()
    {
        
       GameObject g= Instantiate(go, transform);
      //  g.SetActive(false);
    }
}
