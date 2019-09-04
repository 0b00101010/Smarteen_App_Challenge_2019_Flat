using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCtrl : MonoBehaviour
{
    [SerializeField]
    private CubeMove MoveCommand;
    
    
    #region Ver_0.1

    // private void Update()
    // {

    //     if(isMove)
    //         return;

    //     // #if UNITY_STANDALONE_OSX
    //     if (Input.GetKeyDown(KeyCode.W))
    //     {
                
    //     }

    //     else if (Input.GetKeyDown(KeyCode.S))
    //     {
            
    //     }

    //     else if (Input.GetKeyDown(KeyCode.A))
    //     {
            
    //     }

    //     else if (Input.GetKeyDown(KeyCode.D))
    //     {
            
    //     }
        
    //     else if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         CameraNumber--;
    //         ChangeCamera();
    //     }

    //     else if (Input.GetKeyDown(KeyCode.E)){
    //         CameraNumber++;
    //         ChangeCamera();
    //     }
    //     // #endif

    //     // #if UNITY_ANDROID
    //     if(Input.touchCount < 2){
    //         if(GameManager.instance.touchManager.SwipeDirection.x > 0 && GameManager.instance.touchManager.SwipeDirection.y > 0){
    //             moveVector = Vector3.forward;
    //             rotateVector = Vector3.right;   
    //             isMove = true;
    //             StartCoroutine(MoveAndRotate());
    //         }
    //         else if(GameManager.instance.touchManager.SwipeDirection.x < 0 && GameManager.instance.touchManager.SwipeDirection.y < 0 ){
    //             moveVector = Vector3.back;
    //             rotateVector = Vector3.left;
    //             isMove = true;
    //             StartCoroutine(MoveAndRotate());

    //         }
    //         else if(GameManager.instance.touchManager.SwipeDirection.x < 0 && GameManager.instance.touchManager.SwipeDirection.y > 0 ){
    //             moveVector = Vector3.left;
    //             rotateVector = Vector3.forward;
    //             isMove = true;            
    //             StartCoroutine(MoveAndRotate());

    //         }
    //         else if(GameManager.instance.touchManager.SwipeDirection.x > 0 && GameManager.instance.touchManager.SwipeDirection.y < 0 ){
    //             moveVector = Vector3.right;
    //             rotateVector = Vector3.back;
    //             isMove = true;
    //             StartCoroutine(MoveAndRotate());
                
    //         }   
    //     }else if(Input.touchCount > 1){
    //         if(GameManager.instance.touchManager.SwipeDirection.x < 0){
    //             CameraNumber++;
    //             ChangeCamera();
    //         }else if(GameManager.instance.touchManager.SwipeDirection.x > 0){
    //             CameraNumber--;
    //             ChangeCamera();
    //         }

    //     }
    //     // #endif
    // }
    #endregion Ver_0.1
   
    private void Update(){
        if (Input.GetKeyDown(KeyCode.W)){
            MoveCommand.CubeUp();
        }

        else if (Input.GetKeyDown(KeyCode.S)){
            MoveCommand.CubeDown();
        }

        else if (Input.GetKeyDown(KeyCode.A)){
            MoveCommand.CubeLeft();
        }

        else if (Input.GetKeyDown(KeyCode.D)){
            MoveCommand.CubeRight();
        }
        
        else if (Input.GetKeyDown(KeyCode.Q)){
            MoveCommand.CameraTurnLeft();
        }

        else if (Input.GetKeyDown(KeyCode.E)){
            MoveCommand.CameraTurnRight();
        }

        if(Input.touchCount < 2){
            if(GameManager.instance.touchManager.SwipeDirection.x > 0 && GameManager.instance.touchManager.SwipeDirection.y > 0){
                MoveCommand.CubeUp();
            }
            else if(GameManager.instance.touchManager.SwipeDirection.x < 0 && GameManager.instance.touchManager.SwipeDirection.y < 0 ){
                MoveCommand.CubeDown();
            }
            else if(GameManager.instance.touchManager.SwipeDirection.x < 0 && GameManager.instance.touchManager.SwipeDirection.y > 0 ){
                MoveCommand.CubeLeft();
            }
            else if(GameManager.instance.touchManager.SwipeDirection.x > 0 && GameManager.instance.touchManager.SwipeDirection.y < 0 ){
                MoveCommand.CubeRight();
            }   
        }else if(Input.touchCount > 1){
            if(GameManager.instance.touchManager.SwipeDirection.x < 0){
                MoveCommand.CameraTurnLeft();
            }else if(GameManager.instance.touchManager.SwipeDirection.x > 0){
                MoveCommand.CameraTurnRight();
            }

        }
    }

    public void SubscribeObserver(IObserver observer){
        MoveCommand.SubscribeObserver(observer);
    }

}


