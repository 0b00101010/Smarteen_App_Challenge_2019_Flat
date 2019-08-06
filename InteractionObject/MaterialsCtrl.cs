using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MaterialsCtrl : MonoBehaviour
{
    [Header("Color Materials")]
    [SerializeField]
    private Material[] materials;

    public Material GetColorMaterials(int index)
    {
        return materials[index];
    }
}
