using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneObject : MonoBehaviour
{
    [SerializeField]
    private int methodNumber;

    private void OnTriggerEnter(Collider other){
        if(methodNumber == 0)
            StartSceneManager.instance.StageSelectScene();
        else if (methodNumber == 1)
            StartSceneManager.instance.SettingButton();
        else if (methodNumber == 2)
            return;
    }

}
