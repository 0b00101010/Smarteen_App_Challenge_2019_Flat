using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    private Rigidbody rigidBody;

    private ColorState colorState;

    private int count = 90;

    private int upCount = 0;
    private int downCount = 0;

    private Vector3 moveVector;
    private Vector3 rotateVector;

    private void Start()
    {
        colorState = gameObject.GetComponent<ColorState>();
        rigidBody = gameObject.GetComponent<Rigidbody>();

        moveVector = Vector3.zero;
        rotateVector = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //moveVector = Vector3.forward;
            rotateVector = Vector3.left;
            colorState.CurColor = colorState.CurColor.Up();
            MoveAndRotate();
       
            upCount++;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //moveVector = Vector3.back;
            rotateVector = Vector3.right;
            colorState.CurColor = colorState.CurColor.Down();
            MoveAndRotate();
            downCount++;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //moveVector = Vector3.left;
            rotateVector = Vector3.forward;
            colorState.CurColor = colorState.CurColor.Left();
            MoveAndRotate();

            switch(colorState.)

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //moveVector = Vector3.right;
            rotateVector = Vector3.back;
            colorState.CurColor = colorState.CurColor.Right();
            MoveAndRotate();
            
        }
    }

    private void MoveAndRotate()
    {
        // colorState.CurColor.ColorDebug();
        cube.transform.Rotate(rotateVector * 90,Space.World);
        cube.transform.Translate(moveVector, Space.World);

        moveVector = Vector3.zero;
        rotateVector = Vector3.zero;
    }



}
