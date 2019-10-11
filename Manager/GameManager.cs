using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TouchManager touchManager;
    
    public SoundManager soundManager;
    [Space(20)]
    public string nextRound;
    public int nextStageNumber;
    
    private int languageCord = 0;

    public int LanguageCord {
        get => languageCord; 
        set {
             languageCord = value;
             PlayerPrefs.SetInt("LanguageCord",languageCord); 
            }
        }

    [Space(20)]
    [SerializeField]
    [ProgressBar("Star",45,ProgressBarColor.Yellow)]
    private int star;    
    
    [SerializeField]
    private AudioClip mainBGM;

    public AudioClip MainBgm {get=> mainBGM;}

    public int Star{
        get => star;
        set{
            star = value;
            PlayerPrefs.SetInt("Star",star);
        }    
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;

        if(!PlayerPrefs.HasKey("Star"))
            PlayerPrefs.SetInt("Star",0);
        else
            star = PlayerPrefs.GetInt("Star");    
                    
        if(!PlayerPrefs.HasKey("LanguageCord")){
            LanguageCordSetting();
        }else{
            LanguageCord = PlayerPrefs.GetInt("LanguageCord");
        }

        if(!PlayerPrefs.HasKey("FirstPlay"))
            PlayerPrefs.SetInt("FirstPlay",1);


        touchManager = gameObject.GetComponent<TouchManager>();
        soundManager = gameObject.GetComponent<SoundManager>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start(){
        soundManager.ChangeBGM(mainBGM,-1);
    }

    [Button("PlayerPrefs Key all Clear")]
    public void ClearAllKey(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("FirstPlay",1);
        PlayerPrefs.SetString("BGM","true");
        PlayerPrefs.SetString("SFX","true");

        
    }

    private void LanguageCordSetting(){
        switch(Application.systemLanguage){
            case SystemLanguage.Korean:
                LanguageCord = 0; 
            break;
            default:
                LanguageCord = 1;
            break;
        }
    }

    private void Update()
    {
        touchManager.ProcessMobileInput();
    }

      public IEnumerator FadeIn(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 30) {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + (1.0f / repeatCount));
            yield return CoroutineManager.WaitSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator FadeOut(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 30) {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (1.0f / repeatCount));
            yield return CoroutineManager.WaitSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeIn(Image image, float spendTime, int repeatCount = 30)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (1.0f / repeatCount));
            yield return CoroutineManager.WaitSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeOut(Image image, float spendTime, int repeatCount = 30)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (1.0f / repeatCount));
            yield return CoroutineManager.WaitSeconds(spendTime / repeatCount);
        }
    }

    private void OnApplicationQuit(){
        PlayerPrefs.Save();
    }
}
