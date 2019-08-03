using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    private Ray ray = new Ray();

    private void Start()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x ,-2, gameObject.transform.position.z);
        ray.origin = pos;
        ray.direction = Vector3.up;

    }

    protected virtual void Interaction() { }

    protected int GetColorIndex()
    {
        Debug.Log("asdfasdf");
        RaycastHit hit;
        //if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Cube")))
        {
            if (hit.collider != null && hit.collider.CompareTag("Side"))
            {
                Debug.Log(hit.collider.transform.name);
                return hit.collider.GetComponent<CubeColor>().SideColor;
            }
            Debug.Log(hit.collider.transform.name);
        }
        return -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ray.origin,ray.direction);
    }
}
