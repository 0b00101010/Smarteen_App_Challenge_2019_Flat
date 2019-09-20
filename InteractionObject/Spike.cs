using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : InteractionObject
{
    private bool isCollision = false;

    private int otherObjectColor;

    private void OnTriggerEnter(Collider other)
    {
        otherObjectColor = GetColorIndex();
        if (otherObjectColor.Equals(colorNumber) && !isCollision)
        {
            Interaction();
            isCollision = true;
            StartCoroutine(CollisionWait());
        }
    }

    protected override void Interaction(){
        Handheld.Vibrate();
        StageManager.instance.GameEnd();
    }


    private IEnumerator CollisionWait()
    {
        yield return new WaitForSeconds(0.1f);
        isCollision = false;
    }

}
