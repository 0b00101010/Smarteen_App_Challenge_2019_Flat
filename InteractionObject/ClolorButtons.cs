using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClolorButtons : InteractionObject
{

    [Header("Color Number")]
    [SerializeField]
    private int colorNumber;

    [Header("Walls")]
    [SerializeField]
    private List<GameObject> walls = new List<GameObject>();

    private bool isInvisible = false;

    private int otherObjectColor;

    private bool isCollision = false;

    private void OnEnable()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Wall");

        for(int i =0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<Wall>().ColorNumber.Equals(colorNumber))
                walls.Add(temp[i]);
        }

        switch (gameObject.GetComponent<Material>().name)
        {
            case "Lime":
                colorNumber = 0;
                break;
            case "Blue":
                colorNumber = 1;
                break;
            case "Red":
                colorNumber = 2;
                break;
            case "Magenta":
                colorNumber = 3;
                break;
            case "Orange":
                colorNumber = 4;
                break;
            case "Yellow":
                colorNumber = 5;
                break;

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
    }

    private IEnumerator CollisionWait()
    {
        yield return new WaitForSeconds(0.1f);
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
        }
        else
        {
            for (int i = 0; i < walls.Count; i++)
                walls[i].SetActive(true);

        }

        isInvisible = !isInvisible;
    }

  
}
