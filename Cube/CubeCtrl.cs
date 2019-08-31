using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject gameCube;

    private Ray ray;
    private bool isMove = false;
    private Vector3 rotateVector;
    private Vector3 moveVector;

    private IObserver CubeMoveObserver;

    private void Start()
    {
        ray.origin = gameObject.transform.position;
    }

    public void SubscribeObserver(IObserver observer){
        CubeMoveObserver = observer;
    }

    private void Update()
    {

        if(isMove)
            return;

        // #if UNITY_STANDALONE_OSX
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveVector = Vector3.forward;
            rotateVector = Vector3.right;
            isMove = true;           
            StartCoroutine(MoveAndRotate());
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveVector = Vector3.back;
            rotateVector = Vector3.left;
            isMove = true;        
            StartCoroutine(MoveAndRotate());

        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveVector = Vector3.left;
            rotateVector = Vector3.forward;
            isMove = true;        
            StartCoroutine(MoveAndRotate());
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveVector = Vector3.right;
            rotateVector = Vector3.back;
            isMove = true;        
            StartCoroutine(MoveAndRotate());
        }

        // #endif

        // #if UNITY_ANDROID
        if(GameManager.instance.touchManager.SwipeDirection.x > 0 && GameManager.instance.touchManager.SwipeDirection.y > 0 ){
            moveVector = Vector3.forward;
            rotateVector = Vector3.right;   
            isMove = true;
            StartCoroutine(MoveAndRotate());
        }
        else if(GameManager.instance.touchManager.SwipeDirection.x < 0 && GameManager.instance.touchManager.SwipeDirection.y < 0 ){
             moveVector = Vector3.back;
            rotateVector = Vector3.left;
            isMove = true;
            StartCoroutine(MoveAndRotate());

        }
        else if(GameManager.instance.touchManager.SwipeDirection.x < 0 && GameManager.instance.touchManager.SwipeDirection.y > 0 ){
             moveVector = Vector3.left;
            rotateVector = Vector3.forward;
            isMove = true;            
            StartCoroutine(MoveAndRotate());

        }
        else if(GameManager.instance.touchManager.SwipeDirection.x > 0 && GameManager.instance.touchManager.SwipeDirection.y < 0 ){
            moveVector = Vector3.right;
            rotateVector = Vector3.back;
            isMove = true;
            StartCoroutine(MoveAndRotate());
            
        }
        // #endif
    }

    private bool DetectedWall()
    {
       ray.origin = gameObject.transform.position;
       ray.direction = moveVector;
       RaycastHit hit;
       
       if(Physics.Raycast(ray,out hit, 1, LayerMask.GetMask("Wall")))
        {
            return true;
        }

        return false;
    }
    private IEnumerator MoveAndRotate()
    {

        if (!DetectedWall()){
        // colorState.CurColor.ColorDebug();
            for(int i  = 0 ; i < 10; i++){
                gameCube.transform.Rotate(rotateVector * 9, Space.World);
                gameObject.transform.Translate(moveVector / 10, Space.World);
                yield return null;
            }
            isMove = false;
            CubeMoveObserver.Notify();
        }
        else {
            isMove = false;
        }
        moveVector = Vector3.zero;
        rotateVector = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin,ray.direction);
    }
}
