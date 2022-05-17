using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController instance;
    public bool EventControllerReady { get; set; }
    #region TrapsEvent
    public event Action explosionEvent;
    public event Action stumbleEvent;
    public event Action playerDeadEvent;
    public event Action playerLifeEvent;
    public event Action gameContinueEvent;
    public event Action CameraShakeEvent;
    #endregion
    public event Action<bool> quizEnableEvent;

    #region CoinCollect
    public event Action coinCollectEvent;
    #endregion

    #region Magnet
    public event Action<bool> magentEvent;
    #endregion

    public event Action<bool> bikeEvent;
    public event Action<bool> hulkEvent;
    public event Action<bool> flyingEvent;
    public event Action<bool> skateEvent;
    public event Action<bool> slowMoEvent;
    public event Action<bool> canUsePower;


    private void Awake()
    {
        instance = this;
    }

    public void canUsePower_ev(bool b)
    {

        canUsePower(b);
    }

    public void explosionEvent_fn()
    {
        explosionEvent?.Invoke();
        CameraShakeEvent?.Invoke();
    }
    public void playerDead_fn()
    {
       
        playerDeadEvent?.Invoke();
        CameraShakeEvent?.Invoke();
    }
    public void playerLifeEvent_fn()
    {
        playerLifeEvent?.Invoke();
    }
    public void stumble_fn()
    {
        stumbleEvent?.Invoke();
        CameraShakeEvent?.Invoke();
    }

    public void coinCollectEvent_fn()
    {
        coinCollectEvent?.Invoke();
    
    }

    public void gameContinueEvent_Fn()
    {
        gameContinueEvent?.Invoke();
        
    
    }

    public void quizContniueEvent_Fn(bool b)
    {
        quizEnableEvent(b);
    
    }
    public void cameraShakeEvent_fn()
    {

        
        
    }

    public void magnetEvent_fn(bool b)
    {
        magentEvent(b);
    
    }
    public void slowMoEvent_fn(bool b)
    {
       // slowMoEvent(b);
    }
    public void bikeEvent_fn(bool b)
    {
        bikeEvent(b);
    }
    public void skateEvent_fn(bool b)
    {
        skateEvent(b);
    }
    public void hulkEvent_fn(bool b)
    {
        hulkEvent(b);
    }
    public void flyingEvent_fn(bool b)
    {
        flyingEvent(b);
    }
}
