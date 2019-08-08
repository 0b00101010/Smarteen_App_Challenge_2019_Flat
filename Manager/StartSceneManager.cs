using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    private Image blackImage;

    private void Start(){
        StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));
    }

    public void StageSelectScene(){
        StartCoroutine(StageSelectSceneLoad());

    }

    private IEnumerator StageSelectSceneLoad(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.5f));
        SceneManager.LoadScene("01.StageSelectScene");
    }

    public void CustomScene(){
        StartCoroutine(CustomSceneLoad());
    }

    private IEnumerator CustomSceneLoad(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.5f));
        SceneManager.LoadScene("03.CustomeScene");
    }
}
