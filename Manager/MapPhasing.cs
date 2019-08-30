using System;
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
    // Tile Wall Object
    [SerializeField]
    [Tooltip("0 button, 1 spike, 2 portal")]
    private GameObject[] interactionObject = new GameObject[2]; // 0 spike, 1 portal
    private GameObject[] buttons = new GameObject[6];
    private GameObject tile;
    private GameObject wall;

    private string[] inGameInformations = new string[7];
    // StageNumber, StageType, Limit, LimitValue, Mission, MissionValue, Theme

    [SerializeField]
    private GameObject invisibleWall;
    [SerializeField]
    private GameObject cube;
 
    public void Phasing(){

        tile = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Tile/Tile");
        wall = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Wall/Wall");

        interactionObject[0] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Spike/Spike") ?? null;
        interactionObject[1] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Portal/Portal") ?? null;

        buttons[0] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Button/Button_Lime") ?? null;
        buttons[1] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Button/Button_Blue") ?? null;        
        buttons[2] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Button/Button_Red") ?? null;        
        buttons[3] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Button/Button_Magenta") ?? null;        
        buttons[4] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Button/Button_Orange") ?? null;        
        buttons[5] = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Button/Button_Yellow") ?? null;        

        cube = GameObject.Find("CUBE");

        mapDataFile = Resources.Load<TextAsset>("MapData/" + GameManager.instance.nextRound + "/0" + GameManager.instance.nextStageNumber.ToString());
        Debug.Log("MapData/" + GameManager.instance.nextRound + "/0" + GameManager.instance.nextStageNumber.ToString());
        // 한 표식을 기점으로 오브젝트 데이타와 맵 내부 시스템 데이타로 구분하기
        string[] fileData = mapDataFile.text.Split(new string[] {"-"}, System.StringSplitOptions.None);

        string[] frontData = fileData[0].Split(Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries); // 인 게임 셋팅
        string[] middleData = fileData[1].Split(Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries); // 타일 정도 
        string[] backData = fileData[2].Split(Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries); // 오브젝트 정보
        string[] mapData;
        
        for(int i =0 ; i < frontData.Length; i++){
            mapDatas.Add(frontData[i]);
            mapDatas[i].Trim();
        }
            
        for(int i = 0; i < mapDatas.Count; i++){
            string[] gameSystemInformation = mapDatas[i].Split(':'); 
            MapInformationSetting(gameSystemInformation[0], gameSystemInformation[1]);
        }
        
        StageManager.instance.GameSetting(int.Parse(inGameInformations[0]),inGameInformations[1],inGameInformations[2],int.Parse(inGameInformations[3]),inGameInformations[4],int.Parse(inGameInformations[5]),int.Parse(inGameInformations[6]));

        mapDatas.Clear();
 
        for(int i = 0; i < middleData.Length; i++){         
            mapDatas.Add(middleData[i]);
            mapDatas[i].Trim();
            
        }
        
        // 타일 설치
        for(int i = 1 ; i< mapDatas.Count; i++){
            mapData = mapDatas[i].Split(cutChar);
            x++;
            for(int j = 0; j < mapData.Length; j++){
                z--;
                if(mapData[j].Equals("1"))
                    Instantiate(tile,new Vector3(x,0,z),Quaternion.identity);       
                else if (mapData[j].Equals("0"))
                    Instantiate(invisibleWall,new Vector3(x,1,z),Quaternion.identity);
            }
            z = 0;
        }        
        
        x = 1;
        z = -1;
        
        mapDatas.Clear();
        
        for(int i = 0; i < backData.Length; i++){                 
            mapDatas.Add(backData[i]);
        }

        // 오브젝트 생성
        for(int i = 1 ; i < mapDatas.Count; i++){
            mapData = mapDatas[i].Split(cutChar);
            x++;
            
            for(int j = 0; j < mapData.Length; j++){
            
                Debug.Log(j + "_"+ mapData[j]);
                z--;
                if(mapData[j].Equals("0") || mapData[j].Equals(0)){
                    Debug.Log("[Zero] :" + x + "," + z);
                }
                else if(mapData[j].Equals("Start")){
                    cube.transform.position = new Vector3(x,1.0f,z);
                    Debug.Log("[StartPosition] :" + x + "," + z);
                }
                else{
                    string[] colorAndPosition = mapData[j].Split('_');
                    CreateInteractionObject(colorAndPosition[0],colorAndPosition[1], new Vector3(x,0,z));
                    //Debug.Log(x.ToString() + "," + z.ToString());

                    Debug.Log("[Object] :" + x + "," + z);
                }
            }
            z = -1;
        }        


       

    }

    private void CreateInteractionObject(string  colorName,  string objectName, Vector3 createPosition){
        
        int objectIndex = -1; // Spike/0, Portal/1
        int colorIndex = int.Parse(colorName);
        createPosition.y = interactionObjectCreatePositions[2].position.y;
        GameObject createdObject;
        switch(objectName){
            case "Button":
                objectIndex = -1;
                createdObject = Instantiate(buttons[colorIndex], createPosition, Quaternion.identity);                  
                return;
            break;
            
            case "Spike":
                objectIndex = 0;
            break;

            case "Portal":
                objectIndex = 1;   
            break;

            case "Wall":
                createPosition.y = 1;
                createdObject = Instantiate(wall, createPosition, Quaternion.identity);      
                createdObject.GetComponentInParent<InteractionObject>().colorNumber = colorIndex;            
            return;
            
            case "OffWall":
                createPosition.y = 1;
                createdObject = Instantiate(wall, createPosition, Quaternion.identity);      
                createdObject.GetComponentInParent<InteractionObject>().colorNumber = colorIndex;  
                createdObject.SetActive(false);          
            return;
            
            default : 
                objectIndex = -1;
            break;
        }
        

        if(objectIndex != -1){
            createdObject = Instantiate(interactionObject[objectIndex], createPosition, Quaternion.identity);
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
            if(value.Contains("/")){
                inGameInformations[2] = (value.Split('/')[0]) ?? value;
                inGameInformations[3] = value.Split('/')[1];
            }
            else{
                inGameInformations[2] = value;
                inGameInformations[3] = "0";
            }
            break;
            case "Mission":
            if(value.Contains("/")){
                inGameInformations[4] = (value.Split('/')[0]) ?? value;
                inGameInformations[5] = value.Split('/')[1];
            }
            else{
                inGameInformations[4] = value;
                inGameInformations[5] = "0";
            }
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
