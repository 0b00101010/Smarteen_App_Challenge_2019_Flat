using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPhasing : MonoBehaviour
{
    private TextAsset mapDataFile;
    private readonly char cutChar = ',';
    private List<string> mapDatas = new List<string>();
    private int x = 0;
    private int z = 0;

    private GameObject button;
    private GameObject tile;
    private GameObject wall;

    [SerializeField]
    private GameObject invisibleWall;
    [SerializeField]
    private GameObject cube;
 
    private void Start(){

        tile = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Tile/Tile");

        mapDataFile = Resources.Load<TextAsset>("MapData/" + GameManager.instance.nextRound + "/0" + GameManager.instance.nextStageNumber.ToString());
        string[] str = mapDataFile.text.Split('\n');
        
        for(int i =0 ; i < str.Length; i++){
            mapDatas.Add(str[i]);
        }

        for(int i = 0 ; i< mapDatas.Count; i++){
            str = mapDatas[i].Split(cutChar);
            x--;
            for(int j = 0 ; j < str.Length; j++){
                Instantiate(tile,new Vector3(x,0,z),Quaternion.identity);        
                z++;
            }
            z = 0;
        }        

        


        // for문을 돌려서 ','로 각각 문자 분리
        // 0,targetPos,0 좌표 점점 증가
        
        // 같은 방법으로 맵 말고 오브젝트 생성 추가.
        // 앞에 수식어로 targetPos 판단
        
    }
    
}
