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
        string[] fileData = mapDataFile.text.Split(new string[] {"\n-\n"}, System.StringSplitOptions.None);

        for(int i = 0; i < fileData.Length; i++)
            Debug.Log(fileData[i]);
        
        string[] frontData = fileData[0].Split('\n'); // 인 게임 셋팅
        string[] middleData = fileData[1].Split('\n'); // 타일 정도 
        string[] backData = fileData[2].Split('\n'); // 오브젝트 정보
        string[] mapData;

        for(int i =0 ; i < frontData.Length; i++){
            if(!frontData[i].Equals("\n"))
                mapDatas.Add(frontData[i]);
        }

        
        for(int i = 0; i < mapDatas.Count; i++){
            string[] gameSystemInformation = mapDatas[i].Split(':'); 
            Debug.Log(gameSystemInformation[0]);
            Debug.Log(gameSystemInformation[1]);
            MapInformationSetting(gameSystemInformation[0], gameSystemInformation[1]);
        }
        
        StageManager.instance.GameSetting(int.Parse(inGameInformations[0]),inGameInformations[1],inGameInformations[2],int.Parse(inGameInformations[3]),inGameInformations[4],int.Parse(inGameInformations[5]),int.Parse(inGameInformations[6]));

        mapDatas.Clear();
        
        for(int i = 0; i < middleData.Length; i++){
            if(!middleData[i].ToString().Contains("\n"))
               mapDatas.Add(middleData[i]);
        }
  
        
        // 타일 설치
        for(int i = 0 ; i< mapDatas.Count; i++){
            mapData = mapDatas[i].Split(cutChar);
            x++;
            for(int j = 0 ; j < mapData.Length; j++){
                if(!mapData[j].Equals("0"))
                    Instantiate(tile,new Vector3(x,0,z),Quaternion.identity);        
                z--;
            }
            z = 0;
        }        

        x = 0;
        z = 0;
        
        mapDatas.Clear();
        
        for(int i = 0; i < backData.Length; i++){
            if(!backData[i].Equals("\n"))
                mapDatas.Add(backData[i]);
        }

        // 오브젝트 생성
        for(int i = 0 ; i < mapDatas.Count; i++){
            mapData = mapDatas[i].Split(cutChar);
            x++;
            for(int j = 0; j < mapData.Length; j++){
                if(mapData[j].Equals("0"))
                    continue;

                if(mapData[j].Equals("Start")){
                    cube.transform.position = new Vector3(x,1.5f,z);
                    continue;
                }

                string[] colorAndPosition = mapData[j].Split('_');
                CreateInteractionObject(colorAndPosition[0],colorAndPosition[1], new Vector3(x,0,z));
                Debug.Log(x.ToString() + "," + z.ToString());
                z--;
            }
            z = 0;
        }        

       

    }

    private void CreateInteractionObject(string  colorName,  string objectName, Vector3 createPosition){
        
        int objectIndex = -1; // Spike/0, Portal/1
        int colorIndex = int.Parse(colorName);
        createPosition.y = interactionObjectCreatePositions[2].position.y;

        switch(objectName){
            case "Button":
                objectIndex = -1;
                GameObject createdObject = Instantiate(buttons[colorIndex], createPosition, Quaternion.identity);                  
                return;
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
            inGameInformations[3] = value.Split('/')[0].Equals("None") ? "0" : value.Split('\n')[1];
            break;
            case "Mission":
            inGameInformations[4] = value.Split('/')[0];
            inGameInformations[5] = value.Split('/')[0].Equals("None") ? "0" : value.Split('\n')[1];
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
