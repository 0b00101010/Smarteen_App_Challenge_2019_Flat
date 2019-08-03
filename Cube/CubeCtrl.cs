using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject gameCube;

    private Vector3 rotateVector;
    private Vector3 moveVector;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveVector = Vector3.forward;
            rotateVector = Vector3.right;   
            MoveAndRotate();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveVector = Vector3.back;
            rotateVector = Vector3.left;
            MoveAndRotate();      

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveVector = Vector3.left;
            rotateVector = Vector3.forward;
            MoveAndRotate();


        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveVector = Vector3.right;
            rotateVector = Vector3.back;
            MoveAndRotate();

        }
    }


    private void MoveAndRotate()
    {

        // colorState.CurColor.ColorDebug();
        gameCube.transform.Rotate(rotateVector * 90, Space.World);
        gameObject.transform.Translate(moveVector, Space.World);

        moveVector = Vector3.zero;
        rotateVector = Vector3.zero;
    }
}
