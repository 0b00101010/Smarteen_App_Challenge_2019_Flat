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
    private Image backgroundWhite;

    [SerializeField]
    private Canvas settingCanvas;
    private Ray ray = new Ray();
    [SerializeField]
    private Button bgmButton;
    [SerializeField]
    private Button sfxButton;
    
    private int frontMoveCount;

    private int FrontMoveCount { 
        get => frontMoveCount; 
        set{       
            Debug.Log(value);

            if(!(value > -2 && value < 2))
                return;
            frontMoveCount = value;
            
            if(frontMoveCount == -1)
                StageSelectScene();
        }
    } 

    private int rightMoveCount;

    private int RightMoveCount{
        get => rightMoveCount;
        set{
            Debug.Log(value);
            if(!(value > -2 && value < 2))
                return;
            rightMoveCount = value;
            
            if(rightMoveCount == 1)
                SettingButton();
        }
    }
    
    private void Start(){
        StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));
        StartCoroutine(WhiteFadeInOut());
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
        if(blackImage.color.a < 0.005f)
            blackImage.gameObject.SetActive(false);

        if(settingCanvas.gameObject.activeInHierarchy && (GameManager.instance.touchManager.IsTouch || Input.GetMouseButtonDown(0))){
            CastRay();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            FrontMoveCount++;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            FrontMoveCount--;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            RightMoveCount--;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            RightMoveCount++;
        }

        if(GameManager.instance.touchManager.SwipeDirection.x > 0 && GameManager.instance.touchManager.SwipeDirection.y > 0 ){
            FrontMoveCount++;
        }
        else if(GameManager.instance.touchManager.SwipeDirection.x < 0 && GameManager.instance.touchManager.SwipeDirection.y < 0 ){
            FrontMoveCount--;

        }
        else if(GameManager.instance.touchManager.SwipeDirection.x < 0 && GameManager.instance.touchManager.SwipeDirection.y > 0 ){
            RightMoveCount--;
        }
        else if(GameManager.instance.touchManager.SwipeDirection.x > 0 && GameManager.instance.touchManager.SwipeDirection.y < 0 ){
            RightMoveCount++;
            
        }
        // #endif

        // #if UNITY_ANDROID
       
        
    }

    private IEnumerator WhiteFadeInOut(){
        WaitForSeconds waitingTime = new WaitForSeconds(0.025f);
        WaitForSeconds waitingTime2 = new WaitForSeconds(1.5f);
        while(true){
            for(int i = 0; i < 30; i++){
                backgroundWhite.color = new Color(backgroundWhite.color.r,backgroundWhite.color.g,backgroundWhite.color.b ,backgroundWhite.color.a + 0.01f);
                yield return waitingTime;
            }

            yield return waitingTime2;

            for(int i = 0; i < 30; i++){
                backgroundWhite.color = new Color(backgroundWhite.color.r,backgroundWhite.color.g,backgroundWhite.color.b,backgroundWhite.color.a - 0.01f);
                yield return waitingTime;
            }
            yield return waitingTime2;

        }

    }

    private void CastRay(){
        
        // ray.origin = GameManager.instance.touchManager.GetPosition();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // ray.origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ray.direction = Vector3.forward;
        RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction,Mathf.Infinity,LayerMask.GetMask("UI"));
        
        if(hit.collider == null){
            settingCanvas.gameObject.SetActive(false);
        }
        
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
