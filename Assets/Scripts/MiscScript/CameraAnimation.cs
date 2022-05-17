using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraAnimation : MonoBehaviour
{
    [Header("Component")]
    public Animator CameraAnimator;

    public   bool isPlayed;
    public float animationTime;

    public Transform playerTranform;
    public Transform initTransform;
    public Vector3 offset;

    public GameObject WonRotate;

    private void Start()
    {
       // initTransform = this.transform;
    }

    private void Update()
    {
        if (!isPlayed)
        {
            if (GameController.instance.isPressedStart )
            {
                isPlayed = true;
                // CameraAnimator.SetBool("play",true);
                //  StartCoroutine(waitforPlayback());
                Anim();
            }
        }

        if (!GameController.instance.isPressedStart && isPlayed)
        {
            isPlayed = false;
            transform.position = initTransform.position;
            transform.rotation = initTransform.rotation;
            // StartCoroutine(Resettrans());
            Debug.Log("Reset transform");
          // transform = initTransform;
            //CameraAnimator.enabled = true;
            // CameraAnimator.SetBool("play", false);
        }
        //if (WonRotate.activeInHierarchy)
        //{
        //    transform.position = GameObject.Find("WonRotate").transform.position;
        //    transform.rotation = GameObject.Find("WonRotate").transform.rotation;

        //}
    }

    IEnumerator waitforPlayback()
    {
        yield return new WaitForSeconds(0f);
        CameraAnimator.enabled = false;

    }

    public void Anim()
    {

        this.transform.DOMove(playerTranform.position+ offset, animationTime).easePeriod=animationTime;
        this.transform.DORotate(new Vector3(10, 0, 0), animationTime).easePeriod=animationTime;
        
    
    }

    IEnumerator Resettrans()
     {
        yield return new WaitForSeconds(1f);
        transform.position = initTransform.position;
        transform.rotation = initTransform.rotation;


    }
}
