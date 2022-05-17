using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnObjectEnable : MonoBehaviour
{
    public UnityEvent OnEnableEvent, OnDisableEvent;

    private void OnEnable()
    {
        OnEnableEvent?.Invoke();
        
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke();
    }
}
