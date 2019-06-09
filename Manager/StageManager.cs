using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : MonoBehaviour
{

    private GameObject stageCube;
    private float rotateSensitivity = 0.5f;

    private void Start()
    {
        stageCube = GameObject.FindWithTag("StageCube");
    }

    private void Update()
    {
        if (GameManager.instance.touchManager.IsSwiped)
            RotateCube();
    }

    public void SetRotateSensitivity(Slider slider)
    {
        rotateSensitivity = slider.value;
    }

    private void RotateCube()
    {
        if (GameManager.instance.touchManager.SwipeDirection.x < 0)
            stageCube.transform.Rotate(Vector3.up * Time.deltaTime * rotateSensitivity * 30);

        else if (GameManager.instance.touchManager.SwipeDirection.x > 0)
            stageCube.transform.Rotate(Vector3.up * -1 * Time.deltaTime * rotateSensitivity * 30);
          
    }
}
