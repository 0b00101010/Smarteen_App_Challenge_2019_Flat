using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClolorButtons : InteractionObject
{
    [Header("Walls")]
    [SerializeField]
    private List<GameObject> walls = new List<GameObject>();
    private TileBlock underBlock;
    private bool isInvisible = false;

    private int otherObjectColor;

    private bool isCollision = false;

    private void OnEnable()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Wall");

        for(int i =0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<Wall>().colorNumber.Equals(colorNumber))
                walls.Add(temp[i]);
        }

        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 5, gameObject.transform.position.z);
        ray.direction = Vector3.up;
       if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Tile")))
        {
            underBlock = hit.collider?.GetComponent<TileBlock>() ?? null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        otherObjectColor = GetColorIndex();
        if (otherObjectColor.Equals(colorNumber) && !isCollision)
        {
            Interaction();
            isCollision = true;
            StartCoroutine(CollisionWait());
        }

        if(underBlock == null && other.transform.CompareTag("Tile")){
            underBlock = other.GetComponent<TileBlock>();
        }
    }

    private IEnumerator CollisionWait()
    {
        yield return new WaitForSeconds(0.3f);
        isCollision = false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    otherObjectColor = GetColorIndex();
    //    if (otherObjectColor != -1)
    //        Interaction();
    //}

    protected override void Interaction()
    {
        // Debug.Log("Interaction");
        
        if (!isInvisible)
        {
            for (int i = 0; i < walls.Count; i++)
                walls[i].SetActive(false);

            if(underBlock != null)
                underBlock.ChangeMaterials(colorNumber);
        }
        else
        {
            for (int i = 0; i < walls.Count; i++)
                walls[i].SetActive(true);

            if(underBlock != null)
                underBlock.ChangeMaterials(6);
        }

        isInvisible = !isInvisible;
    }

  
}
