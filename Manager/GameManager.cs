using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TouchManager touchManager;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        touchManager.ProcessMobileInput();
    }

    private void myOnSwipeDectected(Vector2 SwipeDirection)
    {
        Debug.DrawLine((Vector2)transform.position, (Vector2)transform.position + SwipeDirection, Color.red, 4.0f);
    }
}
