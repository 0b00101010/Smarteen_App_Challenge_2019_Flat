using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using TMPro;
public class StageManager : MonoBehaviour
{

    #region FIELDS
    public static StageManager instance;
    private MapPhasing phasing;

    [BoxGroup("Cube")]
    [SerializeField]
    private CubeCtrl cube;

    [BoxGroup("Background")]
    [SerializeField]
    private Image backgroundImage;

    [BoxGroup("Background")]
    [SerializeField]
    private Image themeAdditionImage;

    [BoxGroup("Background")]
    [SerializeField]
    private Image themeColorImage;
    
    [BoxGroup("Background")]
    [SerializeField]
    private Image blackImage;

    [Space(10)]
    [BoxGroup("Result")]
    [SerializeField]
    private Canvas resultCanvas;


    [BoxGroup("Result")]
    [SerializeField]
    private Image clearText;

    [BoxGroup("Result")]
    [SerializeField]
    private Image resultBackgroundImage;

    [BoxGroup("Result")]
    [SerializeField]
    private Image[] buttonsImage;

    [Space(10)]
    [BoxGroup("Fields")]
    [SerializeField]
    private TextMeshProUGUI limitText;

    [BoxGroup("Fields")]
    [SerializeField]
    private TextMeshProUGUI missionText;

    [BoxGroup("Fields")]
    [SerializeField]
    private Image missionTextBox;

    [BoxGroup("Fields")]
    [SerializeField]
    private Sprite[] themeBackgrounds;

    [BoxGroup("Fields")]
    [SerializeField]
    private Color[] themeColor;

    [BoxGroup("Fields")]
    [SerializeField]
    private Sprite[] themeAddition;

    [BoxGroup("Fields")]
    [SerializeField]
    private Sprite[] clearTextImages;

    [BoxGroup("Fields")]
    [SerializeField]
    private Sprite[] resultBackgroundImages;

    [BoxGroup("Sound")]
    [SerializeField]
    private AudioClip clearSfx;

    [BoxGroup("Sound")]
    [SerializeField]
    private AudioClip failedSfx;

    [BoxGroup("Sound")]
    [SerializeField]
    private AudioClip backgroundMusic;

    #endregion FIELDS
    #region GAME_SETTING
    [Space(10)]
    [Header("Game Information Setting")]
    [Space(10)]

    [Dropdown("limitTypes")]
    [SerializeField]
    private string limitType;

    [Dropdown("missionTypes")]
    [SerializeField]
    private string missionType;
    private string stageType; // Original, custom
    private string[] missionTypes = {"Button_Excute", "Time", "MoveCount", "None"};
    private string[] limitTypes = {"Time", "MoveCount", "None"};
    
    private int theme;

    [Space(20)]
    [MinValue(0f),MaxValue(100f)]
    [SerializeField]
    private int limitValue;

    [MinValue(0f),MaxValue(100f)]
    [SerializeField]
    private int missionValue;
    private float floatTimer;
    private int moveCount = 0;
    private bool isLimitTypeMoveCount = false;
    private bool isMissionTypeMoveCount = false;
    private string languageTimeText;
    private string languageCountText;

    private ParticleSystem backgroundParticle;
    private GameObject backgroundObject;
    private IEnumerator timerCoroutine;

    [SerializeField]
    private bool missionClear;
    public int MoveCount {
        get => moveCount; 
        set {
                moveCount = value;
                if(isLimitTypeMoveCount)
                    limitText.text = moveCount.ToString() + languageCountText;
                if(isLimitTypeMoveCount && moveCount <= 0)
                    GameEnd();

                if(isMissionTypeMoveCount)
                    missionText.text = moveCount.ToString() + languageCountText;
                if(isMissionTypeMoveCount && moveCount <= 0)
                    missionClear = false;
            }
        }

    #endregion GAME_SETTING
    private void Awake(){
        if(instance == null)
            instance = this;
    }

    public void GameSetting(int stageNumber, string stageType, string limitType, int limitValue, string missionType, int missionValue, int theme){
        this.stageType = stageType;
        this.limitType = limitType;
        this.limitValue = limitValue;
        this.missionType = missionType;
        this.missionValue = missionValue;
        this.theme = theme;
    }

    private void Start() {
        GameObject[] sides = GameObject.FindGameObjectsWithTag("Side");
        phasing = gameObject.GetComponent<MapPhasing>() ?? null;
        phasing?.Phasing();
        timerCoroutine = GameTimer();

        for(int i = 0; i < 6; i++){
            sides[i].GetComponent<MeshRenderer>().material = GetResoueceMaterials(sides[i].GetComponent<CubeColor>().SideColor);
        }
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        for(int i = 0; i < buttons.Length; i++)
            buttons[i].GetComponent<ColorButton>().GetWall();
        if(!stageType.Equals("Custom")){
            backgroundImage.sprite = Resources.Load<Sprite>("StageObject/" + GameManager.instance.nextRound + "/Background");
            themeAdditionImage.sprite = Resources.Load<Sprite>("StageObject/" + GameManager.instance.nextRound + "/Addition");
            themeColorImage.color = themeColor[theme];
            backgroundParticle = Resources.Load<ParticleSystem>("StageObject/" + GameManager.instance.nextRound + "/BackgroundParticle");
            backgroundObject = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/BackgroundObject");
            backgroundMusic = Resources.Load<AudioClip>("StageObject/" + GameManager.instance.nextRound + "/BackgroundMusic");
        }else{
            backgroundImage.sprite = themeBackgrounds[theme];
            themeAdditionImage.sprite = themeAddition[theme];
            themeColorImage.color = themeColor[theme];
            backgroundParticle = Resources.Load<ParticleSystem>("StageObject/" + GameManager.instance.nextRound + "/BackgroundParticle");
            backgroundObject = Resources.Load<GameObject>("StageObject/" + GameManager.instance.nextRound + "/BackgroundObject");
            backgroundMusic = Resources.Load<AudioClip>("StageObject/" + GameManager.instance.nextRound + "/BackgroundMusic");
        }

        if(GameManager.instance.LanguageCord.Equals(0)){
            languageTimeText = "초";
            languageCountText = "회";
        }
        else if(GameManager.instance.LanguageCord.Equals(1)){
            languageTimeText = " Sec";
            languageCountText = " Moves";
        }
            
        GameStart();
    }


    private void GameStart(){
        StartCoroutine(BlackFadeOut());

        if(limitType.Equals("Time")){
            floatTimer = (float)limitValue;
            StartCoroutine(timerCoroutine);
        }    
        else if(limitType.Equals("MoveCount")){
            isLimitTypeMoveCount = true;
            MoveCount = limitValue;
        }
        else if (limitType.Equals("None")){
            limitText.text = "";
        }

        if(missionType.Equals("Time")){
            missionClear = true;
            floatTimer = missionValue;
            StartCoroutine(timerCoroutine);                
        }
        else if(missionType.Equals("MoveCount")){
            isMissionTypeMoveCount = true;
            missionClear = true;
            MoveCount = missionValue;
        }
        else if(missionType.Equals("Button_Excute")){
        }
        else if(missionType.Equals("None")){
            missionClear = true;
            missionText.text = "";
            missionTextBox.enabled = false;
        }

        if(backgroundParticle != null)
            Instantiate(backgroundParticle, new Vector3(0,0,0), backgroundParticle.transform.rotation);

        if(backgroundObject != null)
            Instantiate(backgroundObject, backgroundObject.transform.position, backgroundObject.transform.rotation);

        if(backgroundMusic != null ){
            GameManager.instance.soundManager.ChangeBGM(backgroundMusic,theme);
        }
        
        StartCoroutine(ImageFadeInOut());
    }
    
    private IEnumerator BlackFadeOut(){
        yield return StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));
        blackImage.gameObject.SetActive(false);
    }

    private IEnumerator BlackFadeIn(){
        blackImage.gameObject.SetActive(true);
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackImage, 0.5f));
    }

    private IEnumerator ImageFadeInOut(){
        
        WaitForSeconds waitingTime = new WaitForSeconds(0.1f);
        WaitForSeconds repeatDelay = new WaitForSeconds(2.5f);
        while(true){
            for(int i = 0; i < 10; i++){
                themeColorImage.color = new Color(themeColorImage.color.r, themeColorImage.color.g, themeColorImage.color.b, themeColorImage.color.a - 0.025f);
                themeAdditionImage.color = new Color(themeAdditionImage.color.r,themeAdditionImage.color.g,themeAdditionImage.color.b,themeAdditionImage.color.a - 0.1f);
                yield return CoroutineManager.WaitSeconds(0.1f);
            }   

            for(int i = 0; i < 10; i++){
                themeColorImage.color = new Color(themeColorImage.color.r, themeColorImage.color.g, themeColorImage.color.b, themeColorImage.color.a + 0.025f);
                themeAdditionImage.color = new Color(themeAdditionImage.color.r,themeAdditionImage.color.g,themeAdditionImage.color.b,themeAdditionImage.color.a + 0.1f);
                yield return CoroutineManager.WaitSeconds(0.1f);
            }     

            yield return CoroutineManager.WaitSeconds(2.5f);
        }
    }

    private IEnumerator GameTimer(){
        
        while(true){
            floatTimer -= Time.deltaTime;
            
            if(limitType.Equals("Time"))
                limitText.text = floatTimer.ToString("N0") + languageTimeText;

            else if(missionType.Equals("Time"))
                missionText.text = floatTimer.ToString("N0") + languageTimeText;

            if(floatTimer < 0){
                
                if(limitType.Equals("Time"))
                    GameEnd();
                else if(missionType.Equals("Time"))
                    missionClear = false;

                break;
            }
            
            yield return CoroutineManager.WaitSeconds(0.02f);
        }
    }

    private Material GetResoueceMaterials(int index){
        switch(index){
            case 0:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Lime"); 
  
            case 1:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Blue"); 
   
            case 2:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Red"); 
                
            case 3:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Magenta"); 

            case 4:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Orange"); 

            case 5:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Yellow"); 

        }
        return null;
    }



    [Button("Retry")]
    public void Retry(){
        StartCoroutine(RetryCouroutine());
    }

    public void ReturnToMenu(){
        StartCoroutine(ReturnToMenuCoroutine());
    }

    private IEnumerator ReturnToMenuCoroutine(){
        yield return StartCoroutine(BlackFadeIn());
        SceneManager.LoadScene("01.StageSelectScene");
    }

    private IEnumerator RetryCouroutine(){  
        yield return StartCoroutine(BlackFadeIn());
        SceneManager.LoadScene("02.InGame");

    }

    public void NextGame(){
        StartCoroutine(NextGameCoroutine());
    }

    private IEnumerator NextGameCoroutine(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.25f));
        GameManager.instance.nextStageNumber++;
        SceneManager.LoadScene("02.InGame");
    }

    [Button("GameClear")]    
    public void GameClear(){
        GameManager.instance.soundManager.SFXOneShot(clearSfx);
        resultBackgroundImage.sprite = resultBackgroundImages[0];
        clearText.sprite = clearTextImages[0];
        if(GameManager.instance.nextStageNumber.Equals(14))
            buttonsImage[0].GetComponent<Button>().interactable = false;
        StartCoroutine(ResultCoroutine());        
        if(!bool.Parse(PlayerPrefs.GetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString() + "_Star")) && missionClear){
            GameManager.instance.Star += GetMapStar();
            PlayerPrefs.SetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString() + "_Star","true");   
        }

        PlayerPrefs.SetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString(),"true");
    
    }

    [Button("GameEnd")]
    public void GameEnd(){
        GameManager.instance.soundManager.SFXOneShot(failedSfx);
        resultBackgroundImage.sprite = resultBackgroundImages[1];
        clearText.sprite = clearTextImages[1];
        StartCoroutine(ResultCoroutine());
        buttonsImage[0].GetComponent<Button>().interactable = false;
        if(!bool.Parse(PlayerPrefs.GetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString()))){
            PlayerPrefs.SetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString(), "false");
        }

        if(!bool.Parse(PlayerPrefs.GetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString() + "_Star"))){
            PlayerPrefs.SetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString() + "_Star","false");   
        } 

        
    }

    private IEnumerator ResultCoroutine(){
        cube.enabled = false;
        resultCanvas.gameObject.SetActive(true);
        StopCoroutine(timerCoroutine);
        yield return StartCoroutine(GameManager.instance.IFadeIn(resultBackgroundImage,0.5f));
        
        StartCoroutine(GameManager.instance.IFadeIn(clearText,0.5f));
        for(int i = 0; i < buttonsImage.Length;i ++)
            StartCoroutine(GameManager.instance.IFadeIn(buttonsImage[i],0.5f));

        
    }


    private int GetMapStar(){
        
        return 1;
    }
}
