using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeMove : MonoBehaviour{


    [SerializeField]
    private GameObject gameCube;


    [SerializeField]
    private AudioClip moveSfx;

    [SerializeField]
    private int movePower;

    private Ray ray;

    private bool isMove = false;
    public bool IsMove { get => isMove; }
    private Vector3 rotateVector;
    private Vector3 moveVector;

    private Vector3 upVector = new Vector3(0,0.01f,0);
    private Vector3 downVector = new Vector3(0,-0.01f,0);
    private IObserver CubeMoveObserver; 
    [SerializeField]
    private GameObject[] cinemachines;
    private int cameraNumber = 0;
    public int CameraNumber{
        get => cameraNumber;

        set {
            cameraNumber = value;
            
            if(cameraNumber > 3)
                cameraNumber = 0;
            
            else if(cameraNumber < 0)
                cameraNumber = 3;
            
        }
    }

    private ICommand Up;
    private ICommand Down;
    private ICommand Left;
    private ICommand Right;


    public void Start(){
        ray.origin = gameObject.transform.position;
        
        Up = new MoveUp();
        Down = new MoveDown();
        Left = new MoveLeft();
        Right = new MoveRight();
    }

    public void CubeUp(){
        Up.Excute(out moveVector, out rotateVector);
        StartCoroutine(MoveAndRotate());
    }

    public void CubeDown(){
        Down.Excute(out moveVector, out rotateVector);
        StartCoroutine(MoveAndRotate());
    }

    public void CubeLeft(){
        Left.Excute(out moveVector, out rotateVector);
        StartCoroutine(MoveAndRotate());
    }

    public void CubeRight(){
        Right.Excute(out moveVector,out rotateVector);
        StartCoroutine(MoveAndRotate());
    }

    private bool DetectedWall()
    {

       ray.origin = gameObject.transform.position;
       ray.direction = moveVector;
       RaycastHit hit;
       
       if(Physics.Raycast(ray,out hit, movePower, LayerMask.GetMask("Wall")))
        {
            return true;
        }
        return false;
    }
    private IEnumerator MoveAndRotate()
    {
        isMove = true;



        if (!DetectedWall()){
            for(int i  = 0 ; i < 10; i++){
                gameCube.transform.Rotate(rotateVector * 9, Space.World);
                gameObject.transform.Translate((moveVector / 10) * movePower , Space.World);
                
                // if(i < 5)
                //     gameObject.transform.Translate(upVector);
                // else
                //     gameObject.transform.Translate(downVector);
                
                yield return null;
            }
            isMove = false;
            GameManager.instance.soundManager.SFXOneShot(moveSfx);
            CubeMoveObserver?.Notify();
        }
        else {
            isMove = false;
        }
        moveVector = Vector3.zero;
        rotateVector = Vector3.zero;
    }

    public void CameraTurnLeft(){
        CameraNumber++;
        ChangeCamera();

        ICommand command = Up;
        Up =  Left;
        Left = Down;
        Down = Right;
        Right = command;
    }
    public void CameraTurnRight(){
        CameraNumber--;
        ChangeCamera();

        ICommand command = Up;
        Up = Right;
        Right = Down;
        Down = Left;
        Left = command;

    }

    public void SubscribeObserver(IObserver observer){
        CubeMoveObserver = observer;
    }

    private void ChangeCamera(){
        for(int i = 0; i < cinemachines.Length; i++){
            cinemachines[i].gameObject.SetActive(false);
        }

        cinemachines[cameraNumber].gameObject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin,ray.direction);
    }
}

public class MoveUp : CubeMove, ICommand
{
    public void Excute(out Vector3 moveVector, out Vector3 rotateVector){
        moveVector = Vector3.forward;
        rotateVector = Vector3.right;
    }
}
public class MoveDown : CubeMove, ICommand
{
    public void Excute(out Vector3 moveVector, out Vector3 rotateVector){
        moveVector = Vector3.back;
        rotateVector = Vector3.left;
    }
}

public class MoveLeft : CubeMove, ICommand
{
    public void Excute(out Vector3 moveVector, out Vector3 rotateVector){
        moveVector = Vector3.left;
        rotateVector = Vector3.forward;
    }
}

public class MoveRight : CubeMove, ICommand
{
    public void Excute(out Vector3 moveVector, out Vector3 rotateVector){
        moveVector = Vector3.right;
        rotateVector = Vector3.back;
    }
}