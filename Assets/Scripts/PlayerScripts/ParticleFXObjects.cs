using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFXObjects : MonoBehaviour
{

    public GameObject CoinColletVFX;
    public GameObject MagnetVFX;
    public GameObject bikeVfx;
    public GameObject hulkVFX;
    public GameObject scoreMultiplierVFX;
    public GameObject explosionVFX,debriVFX;
    public GameObject bikeObject,bikeObject_1;
    public GameObject jetpack;
    public GameObject skateObject;


    [Header("Objects")]
    public GameObject magnetCollider;



    private void Start()
    {
        EventController.instance.coinCollectEvent += CoinVFX;
        EventController.instance.magentEvent += magnetVFX_fn;
        EventController.instance.bikeEvent += bikeVFX_fn;
        EventController.instance.hulkEvent += hulkVFX_fn;
        EventController.instance.flyingEvent += FlyingPower_flyingEvent;
        EventController.instance.explosionEvent += explsion_VFX;
        EventController.instance.skateEvent += skateVfx_fn;
      
    }

    private void FlyingPower_flyingEvent(bool obj)
    {
        jetpack.SetActive(obj);
        bikeVfx.SetActive(obj);
        bikeVfx.GetComponent<ParticleSystem>().Play();
    }

    private void OnDestroy()
    {
        EventController.instance.coinCollectEvent -= CoinVFX;
        EventController.instance.magentEvent -= magnetVFX_fn;
        EventController.instance.bikeEvent -= bikeVFX_fn;
        EventController.instance.hulkEvent -= hulkVFX_fn;
        EventController.instance.flyingEvent -= FlyingPower_flyingEvent;
        EventController.instance.explosionEvent -= explsion_VFX;
        EventController.instance.skateEvent += skateVfx_fn;
    }

    private void CoinVFX()
    {
       
        CoinColletVFX.GetComponent<ParticleSystem>().Play();
        CoinColletVFX.GetComponent<ParticleSystem>().playbackSpeed = 6;
        CoinColletVFX.GetComponent<AudioSource>().Play();
    }
   

    private void magnetVFX_fn(bool status)
    {
        MagnetVFX.SetActive(status);
        //MagnetVFX.GetComponent<ParticleSystem>().Play();
        magnetCollider.SetActive(status);
       
    }
    private void bikeVFX_fn(bool status)
    {
        gameObject.GetComponent<PlayerAnimator>().setCharaterAnimationBike(status);
        bikeVfx.SetActive(status);
        bikeVfx.GetComponent<ParticleSystem>().Play();
        bikeObject.SetActive(status);
        bikeObject_1.SetActive(status);
        // playerAnimator.SetBool("bike", status);


    }

    private void skateVfx_fn(bool status)
    {
        Debug.Log("_Skate" + status);
        gameObject.GetComponent<PlayerAnimator>().setCharaterAnimationSkate(status);
        bikeVfx.SetActive(status);
        bikeVfx.GetComponent<ParticleSystem>().Play();
        skateObject.SetActive(status);

    }

    private void scoreMultiplierVFX_fn(bool status)
    {
        scoreMultiplierVFX.SetActive(status);
        scoreMultiplierVFX.GetComponent<ParticleSystem>().Play();

    }

    private void hulkVFX_fn(bool b)
    {
        hulkVFX.SetActive(b);
        hulkVFX.GetComponent<ParticleSystem>().Play();

    }


    private void explsion_VFX()
    {
        explosionVFX.GetComponent<ParticleSystem>().Play();
        debriVFX.GetComponent<ParticleSystem>().Stop();
        debriVFX.GetComponent<ParticleSystem>().Play();
    }
    private void debri_VFX()
    {
        
    }

    
}
