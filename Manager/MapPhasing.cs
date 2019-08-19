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

    private string[] inGameInformations;
    // StageNumber, StageType, Limit, LimitValue, Mission, MissionValue, Theme

    [SerializeField]
    private GameObject invisibleWall;
    [SerializeField]
    private GameObject cube;
 
    public void Phasing(){

        tile = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Tile/Tile");
        interactionObject[0] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Button/Button");
        interactionObject[1] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Spike/Spike");
        interactionObject[2] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Portal/Portal");
        mapDataFile = Resources.Load<TextAsset>("MapData/" + GameManager.instance.nextRound + "/0" + GameManager.instance.nextStageNumber.ToString());
            
        // 한 표식을 기점으로 오브젝트 데이타와 맵 내부 시스템 데이타로 구분하기
        string[] fileData = mapDataFile.text.Split('-');
        string[] frontData = fileData[0].Split('\n'); // 타일 정보
        string[] middleData = fileData[1].Split('\n'); // 오브젝트들 위치 
        string[] backData = fileData[2].Split('\n'); // 인 게임 셋팅
        string[] mapData;

        for(int i =0 ; i < frontData.Length; i++)
            mapDatas.Add(frontData[i]);
        
        // 타일 설치
        for(int i = 0 ; i< mapDatas.Count; i++){
            mapData = mapDatas[i].Split(cutChar);
            x++;
            for(int j = 0 ; j < mapData.Length; j++){
                Instantiate(tile,new Vector3(x,0,z),Quaternion.identity);        
                z--;
            }
            z = 0;
        }        

        mapDatas.Clear();
        
        for(int i = 0; i < middleData.Length; i++)
            mapDatas.Add(middleData[i]);

        x = 0;
        z = 0;


        // 오브젝트 생성
        for(int i = 0 ; i < mapDatas.Count; i++){
            mapData = mapDatas[i].Split(cutChar);
            x++;
            for(int j = 0; j < mapData.Length; j++){
                string[] colorAndPosition = mapData[j].Split('_');
                Vector3 createPosition =  new Vector3(x,0,z);
                CreateInteractionObject(colorAndPosition[1],colorAndPosition[0], createPosition);
                z--;
            }
            z = 0;
        }        

        mapDatas.Clear();
        
        for(int i = 0; i < backData.Length; i++)
            mapDatas.Add(backData[i]);
        
        for(int i = 0; i < mapDatas.Count; i++){
            string[] gameSystemInformation = mapDatas[i].Split(':'); 
            MapInformationSetting(gameSystemInformation[0], gameSystemInformation[1]);
        }

        StageManager.instance.GameSetting(int.Parse(inGameInformations[0]),inGameInformations[1],inGameInformations[2],int.Parse(inGameInformations[3]),inGameInformations[4],int.Parse(inGameInformations[5]),inGameInformations[6]);
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
        
    private void MapInformationSetting(string information, string value){
        switch(information){
            case "StageNumber":
            inGameInformations[0] = value;
            break;
            case "StageType":
            inGameInformations[1] = value;
            break;
            case "Limit":
            inGameInformations[2] = value.Split('/')[0];
            inGameInformations[3] = value.Split('/')[1];
            break;
            case "Mission":
            inGameInformations[4] = value.Split('/')[0];
            inGameInformations[5] = value.Split('/')[1];
            break;
            case "Theme":
            inGameInformations[6] = value;
            break;

        }
    }

    private int IsExisted(string tempString, char findChar){
        return tempString.IndexOf(findChar);
    }

}
