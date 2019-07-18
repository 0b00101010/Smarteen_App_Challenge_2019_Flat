using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    private Rigidbody rigidBody;

    private Vector3 moveVector;

    private int state;
  

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            moveVector.z = 1;
            StartCoroutine(Move());

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveVector.z = -1;
            StartCoroutine(Move());

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveVector.x = 1;
            StartCoroutine(Move());

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveVector.x = -1;
            StartCoroutine(Move());

        }

    }

    private IEnumerator Move()
    {


        for (int i = 0; i < 90; i++)
        {
            cube.transform.Rotate(moveVector,Space.World);
            yield return new WaitForSeconds(0.01f);
        }

        moveVector = new Vector3(0,0,0);
    }

}
