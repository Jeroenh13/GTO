using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{
    private float startTime = 0.0f;
    private Vector2 startPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 40.0f;
    private float maxSwipeTime = 0.5f;

    public enum_SwipeState SwipeState;

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            SwipeState = enum_SwipeState.LEFT;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            SwipeState = enum_SwipeState.UP;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            SwipeState = enum_SwipeState.RIGHT;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            SwipeState = enum_SwipeState.DOWN;
        }

        if(Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                switch(t.phase)
                {
                    case TouchPhase.Began :
                        isSwipe = true;
                        startTime = Time.time;
                        startPos = t.position;
                        break;

                    case TouchPhase.Canceled :
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended :
                        float gestureTime = Time.time - startTime;
                        float gestureDist = (t.position - startPos).magnitude;

                        if(isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = t.position - startPos;
                            Vector2 swipeType = Vector2.zero;

                            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    SwipeState = enum_SwipeState.RIGHT;
                                }
                                else
                                {
                                    SwipeState = enum_SwipeState.LEFT;
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    SwipeState = enum_SwipeState.UP;
                                }
                                else
                                {
                                    SwipeState = enum_SwipeState.DOWN;
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
    
}