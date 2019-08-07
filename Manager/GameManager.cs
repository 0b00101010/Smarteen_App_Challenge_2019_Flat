using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TouchManager touchManager;
    
    public string nextRound;
    public int nextStageNumber;

    [SerializeField]
    [ProgressBar("Star",100,ProgressBarColor.Yellow)]
    private int star;    

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

        if(PlayerPrefs.HasKey("Star"))
            PlayerPrefs.SetInt("Star",0);
        else
            star = PlayerPrefs.GetInt("Star");    
                    
        touchManager = gameObject.GetComponent<TouchManager>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        touchManager.ProcessMobileInput();
    }
}
