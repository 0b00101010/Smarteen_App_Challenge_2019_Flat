using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TouchManager : MonoBehaviour
{
    private Vector2 touchedPos;
    private Touch tempTouch;
    private Vector2 swipeDirection;
    private Vector2 multiSwipeDirection;
    private bool isTouch;
    private bool isSwiped;
    private bool isMultiSwiped;
    private float minSwipeDist;
    private Action<Vector2> actionOnSwipeDetected;
    public bool IsTouch { get => isTouch; set => isTouch = value; }
    public bool IsSwiped { get => isSwiped; set => isSwiped = value; }
    public bool IsMultiSwiped { get => isMultiSwiped; set => isMultiSwiped = value; } 
    public Vector2 SwipeDirection { get => swipeDirection; set => swipeDirection = value; }
    public Vector2 MultiSwipeDirection { get => multiSwipeDirection; set => multiSwipeDirection = value;}
    // Update is called once per frame
    public void Awake()
    {
        minSwipeDist = Mathf.Max(Screen.width, Screen.height) / 4f;
    }

    public void ProcessMobileInput()
    {
        // Debug.Log(Input.touchCount);
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            tempTouch = Input.GetTouch(0);
            if (tempTouch.phase == TouchPhase.Began)
            {
                touchedPos = new Vector2(tempTouch.position.x, tempTouch.position.y);
                isTouch = true;
                isSwiped = false;
            }
            else if (tempTouch.phase == TouchPhase.Moved)
            {
                Vector2 currentTouchPos = new Vector2(tempTouch.position.x, tempTouch.position.y);
                bool swipeDetected = CheckSwipe(touchedPos, currentTouchPos);
                SwipeDirection = (currentTouchPos - touchedPos).normalized;
                if (swipeDetected)
                {
                    onSwipeDetected(SwipeDirection);
                }
            }else if (tempTouch.phase == TouchPhase.Ended)
            {
                swipeDirection = new Vector2(0.0f, 0.0f);
                isTouch = false;
            }


            
        }
        else if(Input.touchCount > 1){
            tempTouch = Input.GetTouch(1);

            if(tempTouch.phase == TouchPhase.Began){
                touchedPos = new Vector2(tempTouch.position.x, tempTouch.position.y);
                isMultiSwiped = false;
            }
            else if (tempTouch.phase == TouchPhase.Moved){
                Vector2 currentTouchPos = new Vector2(tempTouch.position.x, tempTouch.position.y);
                bool swipeDetected = CheckSwipe(touchedPos, currentTouchPos);
                MultiSwipeDirection = (currentTouchPos - touchedPos).normalized;
                if(swipeDetected){
                    isMultiSwiped= true;
                }
            }
            else if (tempTouch.phase == TouchPhase.Ended){
                multiSwipeDirection = new Vector2(0.0f, 0.0f);
                isMultiSwiped = false;
            }
        }
    }

    private bool CheckSwipe(Vector2 downPos, Vector2 currentPos)
    {

        if (isSwiped)
            return false;

        Vector2 currentSwipe = currentPos - downPos;
        if (currentSwipe.magnitude >= minSwipeDist)
        {
            //Debug.Log("SwipeDetected : " + currentSwipe);
            return true;
        }
        return false;

    }

    public void SetOnSwipeDectected(Action<Vector2> SwipeDetected)
    {
        actionOnSwipeDetected = SwipeDetected;
    }

    private void onSwipeDetected(Vector2 Direction)
    {
        isSwiped = true;
        //actionOnSwipeDetected(Direction);
    }

    public Vector2 GetPosition()
    {
        return touchedPos;
    }
}
