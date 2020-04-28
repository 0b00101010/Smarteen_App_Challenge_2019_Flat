using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Boost : InteractionObject
{
    private int otherObjectColor;
    private bool isCollision = false;
    
    [Dropdown("boostDirections")]
    [SerializeField]
    private Vector3 boostDirection;

    private Vector3[] boostDirections = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};

    private void OnTriggerEnter(Collider other)
    {

        otherObjectColor = GetColorIndex();
        // otherObjectColor = other.GetComponent<CubeColor>().SideColor;
        if (otherObjectColor.Equals(colorNumber) && !isCollision)
        {
            Debug.Log("Button::TriggerEnter Side Number : " + otherObjectColor);
            Interaction();
            isCollision = true;
            
            StartCoroutine(CollisionWait());
        }
    }

    private IEnumerator CollisionWait()
    {
        yield return new WaitForSeconds(0.35f);
        isCollision = false;
    }


    public void SetDirection(int vectorNumber){
        switch(vectorNumber){
            case 0:
                boostDirection = Vector3.up;
            break;
            
            case 1:
                boostDirection = Vector3.down;
            break;

            case 2:
                boostDirection = Vector3.left;
            break;
            
            case 3:
                boostDirection = Vector3.right;
            break;
        }
    }

    protected override void Interaction(){
        
        // TODO : 부스트가 바라보는 방향으로 큐브를 옮김
        base.Interaction();
    }

}
