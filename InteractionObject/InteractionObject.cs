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
        RaycastHit hit;

       if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Cube")))
        {
            if (hit.collider != null && hit.collider.CompareTag("Side"))
            {
                return hit.collider.GetComponent<CubeColor>().SideColor;
            }
        }
        return -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(ray.origin,ray.direction);
    }
}
