using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCtrl : MonoBehaviour
{
    [SerializeField]
    private CubeMove MoveCommand;
    
    private void Update(){
        if(MoveCommand.IsMove)
            return;

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
            CameraTurnLeft();
        }

        else if (Input.GetKeyDown(KeyCode.E)){
            CameraTurnRight();
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
        }  
    }

    public void CameraTurnLeft(){
        MoveCommand.CameraTurnLeft();
    }

    public void CameraTurnRight(){
        MoveCommand.CameraTurnRight();
    }

    public void SubscribeObserver(IObserver observer){
        MoveCommand.SubscribeObserver(observer);
    }

}


