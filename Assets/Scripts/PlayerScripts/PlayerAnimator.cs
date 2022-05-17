using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField]
    private Animator parentAnimator;

    [SerializeField]
    private Animator boyAnimator;

    [SerializeField]
    private Animator girlAnimator;

    [SerializeField]
    private string runStr, jumpStr, collideStr, slideStr, playerLeftStr, playerRightStr, bikeStr,skateStr,flyStr;


    public void setParentAnimationLeft()
    {
        parentAnimator.Play(playerLeftStr);

    }
    public void setParentAnimationRight()
    {
        parentAnimator.Play(playerRightStr);

    }


    public void setCharacterAnimationRun(bool b)
    {
        boyAnimator.SetBool(runStr, b);
        girlAnimator.SetBool(runStr, b);
    }
    public void setCharacterAnimationJump(bool b)
    {
        boyAnimator.SetBool(jumpStr, b); girlAnimator.SetBool(jumpStr, b);

    }
    public void setCharacterAnimationCollide(bool b)
    {
        boyAnimator.SetBool(collideStr, b); girlAnimator.SetBool(collideStr, b);

    }
    public void setCharacterAnimationSlide(bool b)
    {
        boyAnimator.SetBool(slideStr, b); girlAnimator.SetBool(slideStr, b);
        if (b)
        {
            StartCoroutine(sliderRoutine());
        }
    }

    private IEnumerator sliderRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<CapsuleCollider>().center = GetComponent<Player>().playerClass.ColliderSliderPos;
        GetComponent<CapsuleCollider>().height = GetComponent<Player>().playerClass.colliderHeightSlide;
        PlayerAudioManager.instance.PlayerAudio(PlayerAudioManager.instance.slide, false);
       
        yield return new WaitForSeconds(0.6f);
        GetComponent<CapsuleCollider>().center = GetComponent<Player>().playerClass.ColliderDefaultPos;
        GetComponent<CapsuleCollider>().height = GetComponent<Player>().playerClass.colliderHeightDefault;
        GetComponent<Player>().playerClass.isSliding = false;

    }

    public void setCharaterAnimationBike(bool b)
    {
        boyAnimator.SetBool(bikeStr, b); girlAnimator.SetBool(bikeStr, b);

    }

    public void setCharacterAnimationFly(bool b)
    {
        boyAnimator.SetBool(flyStr, b); girlAnimator.SetBool(flyStr, b);
    
    }

    public void setCharaterAnimationSkate(bool b)
    {
        boyAnimator.SetBool(skateStr, b); girlAnimator.SetBool(skateStr, b);

    }
}
