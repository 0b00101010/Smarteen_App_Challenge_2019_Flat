using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageButton : MonoBehaviour
{   
    private bool isStar;
    private Image blackBackground;

    public bool IsStar{get => isStar;}
    
    private void Start(){
        blackBackground = GameObject.FindWithTag("BlackBackground").GetComponent<Image>();
    }

    public void LoadScene(){

    }

    private IEnumerator LoadSceneCoroutine(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackBackground,0.25f));
        SceneManager.LoadScene("LodingScene");
    }
}
