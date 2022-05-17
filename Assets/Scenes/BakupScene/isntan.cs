using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isntan : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
        
    }

    private void OnEnable()
    {
        Debug.Log("Enable");
    }
    void Start()
    {
        Debug.Log("Start");
    }

    private void FixedUpdate()
    {
        Debug.Log("Fixed");
    }
    private void Update()
    {
        Debug.Log("Update");
    }
    private void LateUpdate()
    {
        Debug.Log("late");
    }
    private void OnGUI()
    {
        Debug.Log("Gui");

    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quit");
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy");
    }
    private void OnDisable()
    {
        Debug.Log("Disable");
    }


}
