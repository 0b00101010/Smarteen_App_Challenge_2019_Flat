using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClolorButtons : InteractionObject
{
    private Ray ray = new Ray();

    [Header("Color Number")]
    [SerializeField]
    private int colorNumber;

    [Header("Walls")]
    [SerializeField]
    private GameObject[] walls;

    private int otherObjectColor;

    private void OnTriggerEnter(Collider other)
    {
        otherObjectColor = GetColorIndex();
        if (otherObjectColor != -1)
            Interaction();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    otherObjectColor = GetColorIndex();
    //    if (otherObjectColor != -1)
    //        Interaction();
    //}

    protected override void Interaction()
    {
        for (int i = 0; i < walls.Length; i++)
            walls[i].SetActive(false);
    }

    private void DestroyWall() { }
}
