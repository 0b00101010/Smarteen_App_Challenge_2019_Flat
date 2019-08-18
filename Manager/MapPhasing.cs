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

    [SerializeField]
    private Transform[] interactionObjectCreatePositions;
    [SerializeField]
    [Tooltip("0 button, 1 spike, 2 portal")]
    private GameObject[] interactionObject; // 0 button, 1 spike, 2 portal
    private GameObject tile;
    private GameObject wall;

    [SerializeField]
    private GameObject invisibleWall;
    [SerializeField]
    private GameObject cube;
 
    public void Phasing(){

        tile = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Tile/Tile");

        mapDataFile = Resources.Load<TextAsset>("MapData/" + GameManager.instance.nextRound + "/0" + GameManager.instance.nextStageNumber.ToString());
            
        // 한 표식을 기점으로 오브젝트 데이타와 맵 내부 시스템 데이타로 구분하기
        string[] fileData = mapDataFile.text.Split('-');
        string[] frontData = fileData[0].Split('\n');
        string[] backData = fileData[1].Split('\n');
        string[] mapData;

        for(int i =0 ; i < frontData.Length; i++){
            mapDatas.Add(frontData[i]);
        }
        // 블럭 오브젝트들 설치
        for(int i = 0 ; i< mapDatas.Count; i++){
            mapData = mapDatas[i].Split(cutChar);
            x--;

            for(int j = 0 ; j < mapData.Length; j++){
                if(!IsExited(mapData[j], '_').Equals(-1)){ 
                    // 오브젝트 생성할때 좌표 초기화
                    string[] colorAndPosition = mapData[j].Split('_');
                    Vector3 createPosition =  new Vector3(x,0,z);
                    CreateInteractionObject(colorAndPosition[1],colorAndPosition[0], createPosition);
                }else{
                    Instantiate(tile,new Vector3(x,0,z),Quaternion.identity);        
                }
                z++;
            }
            z = 0;
        }        

        mapDatas.RemoveRange(0,mapDatas.Count);

        for(int i = 0; i < backData.Length; i++){
            mapDatas.Add(backData[i]);
        }

    }

    private void CreateInteractionObject(string  objectName,  string colorName, Vector3 createPosition){
        
        int objectIndex = -1; // Button/0, Spike/1, Portal/2
        int colorIndex = int.Parse(colorName);

        switch(objectName){
            case "Button":
                objectIndex = 0;
            break;

            case "Spike":
                objectIndex = 1;
            break;

            case "Portal":
                objectIndex = 2;   
            break;

            default : 
                objectIndex = -1;
            break;
        }

        createPosition.y = interactionObjectCreatePositions[objectIndex].position.y;

        if(objectIndex != -1){
            GameObject createdObject = Instantiate(interactionObject[objectIndex], createPosition, Quaternion.identity);
            createdObject.GetComponent<InteractionObject>().colorNumber = colorIndex;

        }else{
            Debug.Log("Can't find object, Invalid format name.");
        }

    }

    private void MapInformationSetting(){

    }

    private int IsExited(string tempString, char findChar){
        return tempString.IndexOf(findChar);
    }

}
