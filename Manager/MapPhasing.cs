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
 
    private void Start(){

        tile = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/Tile/Tile");

        mapDataFile = Resources.Load<TextAsset>("MapData/" + GameManager.instance.nextRound + "/0" + GameManager.instance.nextStageNumber.ToString());
        
        string[] str = mapDataFile.text.Split('\n');
        string[] mapData;

        for(int i =0 ; i < str.Length; i++){
            mapDatas.Add(str[i]);
        }
        // 블럭 오브젝트들 설치
        for(int i = 0 ; i< mapDatas.Count; i++){
            mapData = mapDatas[i].Split(cutChar);
            x--;

            for(int j = 0 ; j < mapData.Length; j++){
                if(!IsExited(mapData[j], '_').Equals(-1)){ 
                    string[] colorAndPosition = mapData[j].Split('_');
                    Vector3 createPosition =  new Vector3(x,0,z);
                    CreateInteractionObject(colorAndPosition[0],colorAndPosition[1], createPosition);
                }else{
                    Instantiate(tile,new Vector3(x,0,z),Quaternion.identity);        
                }
                z++;
            }
            z = 0;
        }        

        // 주미션설정
        // 부가미션설정
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

    private int IsExited(string tempString, char findChar){
        return tempString.IndexOf(findChar);
    }

}
