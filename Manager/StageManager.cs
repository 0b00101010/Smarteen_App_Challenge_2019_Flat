using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public MaterialsCtrl materialsCtrl; 

    private void Awake(){
        if(instance == null)
            instance = this;
    }

    private void Update()
    {

    }

}
