using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ColorButton : InteractionObject
{
  
    [Header("Walls")]
    [SerializeField]
    private List<GameObject> walls = new List<GameObject>();
    private TileBlock underBlock;
    
    private int otherObjectColor;

    private bool isCollision = false;
    private bool isEnter = false;
    private void Start()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 5, gameObject.transform.position.z);
        ray.direction = Vector3.up;
       if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Tile")))
        {
            underBlock = hit.collider?.GetComponent<TileBlock>() ?? null;
        }

        SetRay();
    }

    public void GetWall(){
        Wall[] temp = Resources.FindObjectsOfTypeAll<Wall>();

        for(int i =0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<Wall>().colorNumber.Equals(colorNumber))
                walls.Add(temp[i].gameObject);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        otherObjectColor = GetColorIndex();
        // otherObjectColor = other.GetComponent<CubeColor>().SideColor;
        if (otherObjectColor.Equals(colorNumber) && !isCollision)
        {
            Debug.Log("Button::TriggerEnter Side Number : " + otherObjectColor);
            Interaction();
            isCollision = true;
            
            StartCoroutine(CollisionWait());
        }
    }

    private IEnumerator CollisionWait()
    {
        yield return new WaitForSeconds(0.35f);
        isCollision = false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    otherObjectColor = GetColorIndex();
    //    if (otherObjectColor != -1)
    //        Interaction();
    //}


    [Button("Interaction")]
    protected override void Interaction()
    {
        // Debug.Log("Interaction");
        
        for (int i = 0; i < walls.Count; i++)
            walls[i].SetActive(!walls[i].activeInHierarchy);


        if(!isEnter){
            if(underBlock != null)
                underBlock.ChangeMaterials(colorNumber);
        }
        else{
            if(underBlock != null)
               underBlock.ChangeMaterials(6);
        }
        base.Interaction();
    }

}
