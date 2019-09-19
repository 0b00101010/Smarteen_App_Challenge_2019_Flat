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
    
    [Space(20)]
    [SerializeField]
    [ProgressBar("Star",45,ProgressBarColor.Yellow)]
    private int star;    
    
    [SerializeField]
    private AudioClip mainBGM;

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
        PlayerPrefs.SetString("BGM","true");
        PlayerPrefs.SetString("SFX","true");
    }
    
    private void Update()
    {
        touchManager.ProcessMobileInput();
    }

      public IEnumerator FadeIn(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 30) {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator FadeOut(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 30) {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeIn(Image image, float spendTime, int repeatCount = 30)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeOut(Image image, float spendTime, int repeatCount = 30)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    private void OnApplicationQuit(){
        PlayerPrefs.Save();
    }
}
