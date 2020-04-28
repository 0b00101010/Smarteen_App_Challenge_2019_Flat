using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowFrame : MonoBehaviour
{
    [SerializeField]
    private Text frameText;

    private float deltaTime = 0.0f;


    private void Update(){
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        frameText.text = (1.0f / deltaTime).ToString("F1");
    }
}
