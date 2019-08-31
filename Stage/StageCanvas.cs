using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageCanvas : MonoBehaviour{

    [SerializeField]
    private Canvas stageButtonCanvas;
    
    [SerializeField]
    private StageSelectSceneManager sceneManager;

    [SerializeField]
    private Canvas stageCanvas;

    [SerializeField]
    private Text getStarText;
    
    [SerializeField]
    private string roundName;
    private Ray ray = new Ray();

    private StageButton[] stageButtons;
    private int getStar;
    private int maxStar;

    private void Start(){
        stageButtons = stageButtonCanvas?.GetComponentsInChildren<StageButton>();
        maxStar = stageButtons.Length;

        for(int i = 0; i < stageButtons.Length; i++){
            if(stageButtons[i].IsStar)
                getStar++;
        }


        getStarText.text = getStar.ToString() + " / " + maxStar.ToString();
    }

    public void OpenButtons(){
        stageCanvas.gameObject.SetActive(false);
        stageButtonCanvas.gameObject.SetActive(true);
    } 

    public void CloseButtons(){
        stageCanvas.gameObject.SetActive(true);
        stageButtonCanvas.gameObject.SetActive(false);
    }

    private void Update(){
        if(stageButtonCanvas.gameObject.activeInHierarchy && (Input.GetMouseButtonDown(0) || GameManager.instance.touchManager.IsTouch))
            CastRay();
    }
    
    private void CastRay(){
            
            // ray.origin = GameManager.instance.touchManager.GetPosition();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // ray.origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ray.direction = Vector3.forward;
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction,Mathf.Infinity,LayerMask.GetMask("UI"));
            
            if(hit.collider == null)
                CloseButtons();
            // RaycastHit hit; 
            // if(Physics.Raycast(ray,out hit,Mathf.Infinity,LayerMask.GetMask("UI"))){
            //    Debug.Log(hit.collider?.transform.name);
            // }

            
    }

    public void LoadScene(int nextStageNumber){
        GameManager.instance.nextRound = roundName;
        GameManager.instance.nextStageNumber = nextStageNumber;
        StartCoroutine(LoadSceneCoroutine());
    }

    public IEnumerator LoadSceneCoroutine(){
        yield return StartCoroutine(sceneManager.FadeIn());
        SceneManager.LoadScene("LoadingScene");   
    }
}