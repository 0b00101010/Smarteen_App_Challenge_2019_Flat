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
    
    private int selectStage = 0;
    
    private void Start(){

        StartCoroutine(InitCoroutine());

        for(int i = 0; i < stages.Length; i++){
            stages[i].transform.Translate(new Vector2(i * 80,0));
        }
    }
    private IEnumerator InitCoroutine(){
        yield return StartCoroutine(GameManager.instance.IFadeOut(blackBackground,0.25f));
        blackBackground.gameObject.SetActive(false);
    }
    private void Update(){
        if((Input.GetKeyDown(KeyCode.A) || GameManager.instance.touchManager.SwipeDirection.x < 0) && stages[0].transform.position.x < 0){
            for(int i =0 ; i < stages.Length; i++){
                stages[i].transform.Translate(new Vector2(80f,0));
            }
        }
        else if ((Input.GetKeyDown(KeyCode.D) || GameManager.instance.touchManager.SwipeDirection.x > 0) && stages[stages.Length - 1].transform.position.x > 0)
        {            
            for(int i =0 ; i < stages.Length; i++){
                stages[i].transform.Translate(new Vector3(-80f,0));
            }
        }
    }
    


    public IEnumerator FadeIn(){
        blackBackground.gameObject.SetActive(true);
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackBackground,0.25f));
    }
}
