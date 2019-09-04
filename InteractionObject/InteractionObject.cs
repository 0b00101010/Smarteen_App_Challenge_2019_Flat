using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InteractionObject : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem interactionParticle;


    [Header("Color Number")]
    [Dropdown("colorNumbers")]
    [SerializeField]
    public int colorNumber;

    private MaterialsCtrl materialsCtrl;

    private int[] colorNumbers = {0,1,2,3,4,5};
    
    private Ray ray = new Ray();

    private void Start()
    {
        materialsCtrl = gameObject.GetComponent<MaterialsCtrl>();
        Vector3 pos = new Vector3(gameObject.transform.position.x ,-2, gameObject.transform.position.z);
        ray.origin = pos;
        ray.direction = Vector3.up;
        if(materialsCtrl != null)
            gameObject.transform.GetComponent<MeshRenderer>().material = materialsCtrl.GetColorMaterials(colorNumber) ?? gameObject.transform.GetComponent<MeshRenderer>().material;
    }
    protected void SetRay(){
        Vector3 pos = new Vector3(gameObject.transform.position.x ,-2, gameObject.transform.position.z);
        ray.origin = pos;
        ray.direction = Vector3.up;
    }

    protected void Effect(){
        if(interactionParticle != null)
            Instantiate(interactionParticle,gameObject.transform.position, interactionParticle.transform.rotation);
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
