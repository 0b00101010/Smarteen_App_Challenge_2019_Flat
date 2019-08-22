using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    private Image blackImage;

    [SerializeField]
    private Canvas settingCanvas;
    private Ray ray = new Ray();
    [SerializeField]
    private Button bgmButton;
    [SerializeField]
    private Button sfxButton;

    private void Start(){
        StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));
                
        if(GameManager.instance.soundManager.BGMOnOff)
            bgmButton.image.color = new Color(1f,1f,1f,1f);
            
        else 
            bgmButton.image.color = new Color(0.5f,0.5f,0.5f,1f);
        
        if(GameManager.instance.soundManager.SFXOnOff)
            sfxButton.image.color = new Color(1f,1f,1f,1f);
            
        else 
            sfxButton.image.color = new Color(0.5f,0.5f,0.5f,1f);
    }
    private void Update(){
        if(blackImage.color.a < 0.05f)
            blackImage.gameObject.SetActive(false);

        if(settingCanvas.gameObject.activeSelf && (GameManager.instance.touchManager.IsTouch || Input.GetMouseButtonDown(0))){
            CastRay();
        }
        
    }

    private void CastRay(){

//ss
        
        // ray.origin = GameManager.instance.touchManager.GetPosition();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray.origin);
        // ray.origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ray.direction = Vector3.forward;
        RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction,Mathf.Infinity,LayerMask.GetMask("UI"));
        
        Debug.Log(hit.collider?.transform.name);
        
        // RaycastHit hit; 
        // if(Physics.Raycast(ray,out hit,Mathf.Infinity,LayerMask.GetMask("UI"))){
        //    Debug.Log(hit.collider?.transform.name);
        // }

        
    }

    public void StageSelectScene(){
        blackImage.gameObject.SetActive(true);
        StartCoroutine(StageSelectSceneLoad());

    }

    private IEnumerator StageSelectSceneLoad(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.5f));
        SceneManager.LoadScene("01.StageSelectScene");
    }

    public void CustomScene(){
        blackImage.gameObject.SetActive(true);
        StartCoroutine(CustomSceneLoad());
    }

    private IEnumerator CustomSceneLoad(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.5f));
        SceneManager.LoadScene("03.CustomeScene");
    }

    public void SettingButton(){
        settingCanvas.gameObject.SetActive(true);
    }

    public void BgmOnOff(){
        
        GameManager.instance.soundManager.BGMOnOff = !GameManager.instance.soundManager.BGMOnOff;
        
        if(GameManager.instance.soundManager.BGMOnOff)
            bgmButton.image.color = new Color(1f,1f,1f,1f);
            
        else 
            bgmButton.image.color = new Color(0.5f,0.5f,0.5f,1f);
    
    }

    public void SfxOnOff(){
        
        GameManager.instance.soundManager.SFXOnOff = !GameManager.instance.soundManager.SFXOnOff;
        
        if(GameManager.instance.soundManager.SFXOnOff)
            sfxButton.image.color = new Color(1f,1f,1f,1f);
            
        else 
            sfxButton.image.color = new Color(0.5f,0.5f,0.5f,1f);
    
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
}
