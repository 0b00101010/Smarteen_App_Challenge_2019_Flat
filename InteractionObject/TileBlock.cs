using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBlock : MonoBehaviour
{
   
    private MaterialsCtrl materialsCtrl;
    private int colorIndex;
    // number code 6 : Original Color 
    private int nowColor;

    public int NowColor {get => nowColor;}

    private void Start(){
        materialsCtrl = gameObject.GetComponent<MaterialsCtrl>();
    }

    public void ChangeMaterials(int index){
        colorIndex = index;
        nowColor = colorIndex;
        gameObject.GetComponent<MeshRenderer>().material = materialsCtrl.GetColorMaterials(index);
        StartCoroutine(ChangeOtherBlock());
    }

    private IEnumerator ChangeOtherBlock(){
        Ray ray = new Ray();
        Vector3[] rayDirections = {Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
        RaycastHit hit;
        var waitingTime = new WaitForSeconds(0.15f);

        ray.origin = gameObject.transform.position;
        

        for (int i = 0; i < rayDirections.Length; i++){
            ray.direction = rayDirections[i];

            if(Physics.Raycast(ray.origin,ray.direction,out hit,Mathf.Infinity)){
                if(hit.collider != null && hit.collider.CompareTag("Tile") && hit.collider.GetComponent<TileBlock>().NowColor != colorIndex){
                    hit.collider.GetComponent<TileBlock>().ChangeMaterials(colorIndex);
                    yield return waitingTime;
                }
            }
            yield return null;
        }
    }
}
