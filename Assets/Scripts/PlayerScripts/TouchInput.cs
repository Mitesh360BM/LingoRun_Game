using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    [Header("SWIPE SETTINGS")]
    public float swipeEndTime;
    private Vector2 initialPos;


    public bool ifSwipeLeft { get;  set; }
    public bool IsSwipedUp { get;  set; }
    public bool IfSwipeRight { get;  set; }
    public bool isSwipeDown { get;  set; }


    
       
    


    public void swipe_Fn()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                initialPos = Input.mousePosition;
                // Debug.Log("CLICK");
                StartCoroutine(MouseUP());
            }
        }

    }

    IEnumerator MouseUP()
    {
        yield return new WaitForSeconds(swipeEndTime);
        Calculate(Input.mousePosition);
        yield return new WaitForSeconds(swipeEndTime + 0.1f);

       ifSwipeLeft = false;
       IfSwipeRight = false;
        IsSwipedUp = false;
        isSwipeDown = false;
    }


    void Calculate(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);
        if (disX > 0 || disY > 0)
        {
            if (disX > disY)
            {
                if (initialPos.x > finalPos.x)
                {
                    Debug.Log("Swipe Left");
                    //  isLeft = true;
                   ifSwipeLeft = true;

                }
                else
                {
                    Debug.Log("Swipe Right");

                    //  isRight = true;
                   IfSwipeRight = true;
                }
            }
            else
            {
                if (initialPos.y > finalPos.y)
                {
                    Debug.Log("Swipe Down");

                    //isDown = true;
                    IsSwipedUp = false;
                    isSwipeDown = true;

                }
                else
                {
                    Debug.Log("Swipe Up");

                    //  isUp = true;

                    IsSwipedUp = true;
                    isSwipeDown = false;
                }
            }
        }
    }
}
