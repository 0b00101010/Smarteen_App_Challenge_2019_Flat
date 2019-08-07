using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MaterialsCtrl : MonoBehaviour
{
    [Header("Color Materials")]
    [SerializeField]   
    private List<Material> materials = new List<Material>();

    private void Start(){
        materials.Add(Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Tile/Materials/Lime"));
        materials.Add(Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Tile/Materials/Blue"));
        materials.Add(Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Tile/Materials/Red"));
        materials.Add(Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Tile/Materials/Magenta"));
        materials.Add(Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Tile/Materials/Orange"));
        materials.Add(Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Tile/Materials/Yellow"));
        materials.Add(Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Tile/Materials/White"));

    }

    public Material GetColorMaterials(int index)
    {
        return materials[index];
    }
}