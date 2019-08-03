using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCtrl : MonoBehaviour
{
    private Camera rayCamera;

    private Ray ray;

    [SerializeField]
    private GameObject gameCube;

    private Vector3 rotateVector;
    private Vector3 moveVector;

    private void Start()
    {
        rayCamera = GameObject.FindWithTag("RayCamera").GetComponent<Camera>();
        ray = new Ray();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveVector = Vector3.forward;
            rotateVector = Vector3.right;   
            MoveAndRotate();
            Debug.Log(GetColorIndex());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveVector = Vector3.back;
            rotateVector = Vector3.left;
            MoveAndRotate();      
            Debug.Log(GetColorIndex());

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveVector = Vector3.left;
            rotateVector = Vector3.forward;
            MoveAndRotate();
            Debug.Log(GetColorIndex());


        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveVector = Vector3.right;
            rotateVector = Vector3.back;
            MoveAndRotate();
            Debug.Log(GetColorIndex());

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


    public int GetColorIndex()
    {
        ray.origin = rayCamera.transform.position;
        ray.direction = Vector3.up;

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            if (hit.collider != null && hit.collider.CompareTag("Side"))
            {
                Debug.Log(hit.collider.transform.name);
                return hit.collider.GetComponent<CubeColor>().SideColor;
            }
        }

        return -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(ray.origin,ray.direction);
    }
}
