using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Tutorials : MonoBehaviour
{
    [SerializeField]
    private Sprite[] tutorialKor;

    [SerializeField]
    private Sprite[] tutorialEng;

    [SerializeField]
    private Image tutorialImage;

    [SerializeField]
    private Image blackBackgroundImage;

    [SerializeField]
    private Transform[] fingerPositions;
    private IEnumerator tutorialCoroutine;
    private bool isWait = false;
    private int index;

    
    [SerializeField]
    private ParticleSystem particle;

    [SerializeField]
    private Image fingerImage;

    private void Start(){
        if(GameManager.instance.LaguageCord.Equals(0))
            TutorialKor();    
        else if (GameManager.instance.LaguageCord.Equals(1))
            TutorialEng();
    }

    private void Update(){
        if((GameManager.instance.touchManager.IsTouch || Input.GetMouseButtonDown(0)) && !isWait){
            if(GameManager.instance.LaguageCord.Equals(0))
                TutorialKor();    
            else if (GameManager.instance.LaguageCord.Equals(1))
                TutorialEng();

            isWait = true;
            StartCoroutine(WaitingTime());
        }
    }

    private IEnumerator FingerMove(){
        
        if(index.Equals(0)){
            for(int i = 0; i < 30; i++){
                fingerImage.transform.position = Vector2.Lerp(fingerPositions[0].position, fingerPositions[1].position, (float)i / 30.0f);
                Debug.Log(((float)i/30.0f));
                yield return new WaitForSeconds(0.02f);
            }
            
            particle.gameObject.transform.position = fingerImage.transform.position;
            particle.Play();
        }

        if(index.Equals(3)){
            for(int i = 0; i < 30; i++){
                fingerImage.transform.position = Vector2.Lerp(fingerPositions[1].position, fingerPositions[2].position, (float)i / 30.0f);
                Debug.Log(((float)i/30.0f));
                yield return new WaitForSeconds(0.02f);
            }
            particle.gameObject.transform.position = fingerImage.transform.position;
            particle.Play();
        }
    }

    private void TutorialKor(){
        if(index < tutorialKor.Length){
            StartCoroutine(FingerMove());
            tutorialImage.sprite = tutorialKor[index++];
        }
        else
            StartCoroutine(NextScene());
    }

    private void TutorialEng(){
        if(index < tutorialEng.Length){
            StartCoroutine(FingerMove());
            tutorialImage.sprite = tutorialEng[index++];
        }
        else
            StartCoroutine(NextScene());
    }

    private IEnumerator WaitingTime(){
        yield return new WaitForSeconds(0.5f);
        isWait = false;
    }

    private IEnumerator NextScene(){

        PlayerPrefs.SetInt("FirstPlay",0);
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackBackgroundImage,0.25f));
        SceneManager.LoadScene("01.StageSelectScene");
    }

}
