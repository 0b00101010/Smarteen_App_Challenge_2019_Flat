using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MaterialsCtrl : MonoBehaviour
{
    [Header("Color Materials")]
    [SerializeField]   
    private List<Material> materials = new List<Material>();
    public Material GetColorMaterials(int index)
    {
        return materials[index];
    }
}