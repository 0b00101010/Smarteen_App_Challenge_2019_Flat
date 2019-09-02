using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spectrum : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> Sticks;        
    [SerializeField]
    private Image themeAdditionImage;

    private void Start(){
        StartCoroutine(ImageFadeInOut());
    }
    private void Update()
    {
        float[] SpectrumData = AudioListener.GetSpectrumData(2048, 0, FFTWindow.BlackmanHarris);         
        for (int i = 0; i < Sticks.Count; i++)
        {
            Vector2 FirstScale = Sticks[i].transform.localScale;    
            if(FirstScale.y < 12)                              
                FirstScale.y = 3f + SpectrumData[i] * 250;                                         
            


            Sticks[i].transform.localScale = Vector2.MoveTowards(Sticks[i].transform.localScale, FirstScale, 0.1f);    
        }
    }
    
    private IEnumerator ImageFadeInOut(){
        
        WaitForSeconds waitingTime = new WaitForSeconds(0.1f);
        WaitForSeconds repeatDelay = new WaitForSeconds(2.5f);
        while(true){
            for(int i = 0; i < 10; i++){
                themeAdditionImage.color = new Color(themeAdditionImage.color.r,themeAdditionImage.color.g,themeAdditionImage.color.b,themeAdditionImage.color.a - 0.1f);
                yield return waitingTime;
            }   

            for(int i = 0; i < 10; i++){
                themeAdditionImage.color = new Color(themeAdditionImage.color.r,themeAdditionImage.color.g,themeAdditionImage.color.b,themeAdditionImage.color.a + 0.1f);
                yield return waitingTime;
            }     

            yield return repeatDelay;
        }
    }
}
