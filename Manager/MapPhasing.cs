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

    private void Start(){
        mapDataFile = Resources.Load<TextAsset>("MapData/" + GameManager.instance.nextRound + "/0" + GameManager.instance.nextStageNumber.ToString());
        string[] str = mapDataFile.text.Split('\n');
        
        for(int i =0 ; i < str.Length; i++){
            mapDatas.Add(str[i]);
        }

        // for(int i =0; i < mapDatas.Count; i++){
        //     string[] temp = mapDatas[i].Split(cutChar);
        //     z = 0;
        //     for(int j = 0; i < temp.Length; j++){
        //         Debug.Log(x+",0,"+z);
        //         z++;
        //     }
        //     x--;
        // }

        
        // for문을 돌려서 ','로 각각 문자 분리
        // 0,targetPos,0 좌표 점점 증가
        
        // 같은 방법으로 맵 말고 오브젝트 생성 추가.
        // 앞에 수식어로 targetPos 판단
        
    }
    
}
