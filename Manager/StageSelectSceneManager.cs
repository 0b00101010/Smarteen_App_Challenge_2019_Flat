using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectSceneManager : MonoBehaviour
{
    [SerializeField]
    private StageCanvas[] stages;

    [Space(10)]

    [SerializeField]
    private Image blackBackground;
    [SerializeField]
    private Image background;

    private bool isMove = false;

    private int selectStage = 0;

    public int SelectStage {
        get => selectStage;
        set {
            selectStage = value;
            background.sprite = Resources.Load<Sprite>("UI/Round_0" + selectStage.ToString() + "/Background");        
        }
    }
    private void Start(){

        StartCoroutine(InitCoroutine());
        SelectStage = 0;
        for(int i = 0; i < stages.Length; i++){
            stages[i].transform.Translate(new Vector2(i * 80,0));
        }
    }
    private IEnumerator InitCoroutine(){
        yield return StartCoroutine(GameManager.instance.IFadeOut(blackBackground,0.25f));
        blackBackground.gameObject.SetActive(false);
    }
    private void Update(){
        if((Input.GetKeyDown(KeyCode.A) || GameManager.instance.touchManager.SwipeDirection.x > 0) && stages[0].transform.position.x < 0 && selectStage > 0 && !isMove){
            for(int i =0 ; i < stages.Length; i++){
                stages[i].transform.Translate(new Vector2(80f,0));
            }
            isMove = true;
            SelectStage--;
            StartCoroutine(CanvasWait());
        }
        else if ((Input.GetKeyDown(KeyCode.D) || GameManager.instance.touchManager.SwipeDirection.x < 0) && stages[stages.Length - 1].transform.position.x > 0 && selectStage < stages.Length && !isMove)
        {            
            for(int i =0 ; i < stages.Length; i++){
                stages[i].transform.Translate(new Vector3(-80f,0));
            }
            isMove = true;
            SelectStage++;
            StartCoroutine(CanvasWait());
        }
    }
    
    private IEnumerator CanvasWait(){
        yield return new WaitForSeconds(0.25f);
        isMove = false;
    }

    public IEnumerator FadeIn(){
        blackBackground.gameObject.SetActive(true);
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackBackground,0.25f));
    }
}
