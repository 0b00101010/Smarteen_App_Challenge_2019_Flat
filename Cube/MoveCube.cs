using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    private Rigidbody rigidBody;

    private Vector3 moveVector;

    private Vector3 moveForward;

    private int state;
    private ColorState colorState;

    private void Start()
    {
        colorState = gameObject.GetComponent<ColorState>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            moveVector.z = 3;
            moveForward.x = -1;
            StartCoroutine(Move());
            colorState.CurColor = colorState.CurColor.Left();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveVector.z = -3;
            moveForward.x = 1;
            StartCoroutine(Move());
            colorState.CurColor = colorState.CurColor.Right();

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveVector.x = 3;
            moveForward.z = 1;
            StartCoroutine(Move());
            colorState.CurColor = colorState.CurColor.Up();

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveVector.x = -3;
            moveForward.z = -1;
            StartCoroutine(Move());
            colorState.CurColor = colorState.CurColor.Down();

        }

    }

    private IEnumerator Move()
    {


        for (int i = 0; i < 30; i++)
        {
            cube.transform.Rotate(moveVector,Space.World);
            cube.transform.Translate(moveForward / 30,Space.World);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        moveForward = new Vector3(0, 0, 0);
        moveVector = new Vector3(0,0,0);
    }

}
