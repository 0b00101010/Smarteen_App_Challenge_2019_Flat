using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObserver : MonoBehaviour, IObserver
{
    [SerializeField]
    private CubeCtrl cubeCtrl;

    private void Start(){
        cubeCtrl.SubscribeObserver(this);
    }

    public void Notify(){
       StageManager.instance.MoveCount--; 
    }
}
