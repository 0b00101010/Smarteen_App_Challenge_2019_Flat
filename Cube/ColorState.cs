using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorState : MonoBehaviour
{
    public List<CubeColor> cubeColors = new List<CubeColor>();
    private CubeColor curColor;

    [SerializeField]
    private Image colorImage;

    [SerializeField]
    private ObservColor obs;

    private string colorString;

    public CubeColor CurColor {
        get => curColor;
        set {
            curColor = value;
            colorImage.color = curColor.GetColor();
            colorString = curColor.ToString();
        }

    }

    private void Awake()
    {
        cubeColors.Add (new Red());
        cubeColors.Add(new Magenda());
        cubeColors.Add(new Orange());
        cubeColors.Add(new Yellow());
        cubeColors.Add(new Blue());
        cubeColors.Add(new Lime());

        cubeColors[0].Init(cubeColors[1], cubeColors[4], cubeColors[3], cubeColors[5]);
        cubeColors[1].Init(cubeColors[3], cubeColors[5], cubeColors[0], cubeColors[2]);
        cubeColors[2].Init(cubeColors[1], cubeColors[4], cubeColors[5], cubeColors[3]);
        cubeColors[3].Init(cubeColors[4], cubeColors[1], cubeColors[2], cubeColors[0]);
        cubeColors[4].Init(cubeColors[5], cubeColors[3], cubeColors[0], cubeColors[2]);
        cubeColors[5].Init(cubeColors[1], cubeColors[4], cubeColors[0], cubeColors[2]);

        CurColor = cubeColors[5];
    }

    public void RotateRight()
    {
        obs.Left();
        for(int i = 0; i < cubeColors.Count; i++)
        {
            cubeColors[i].RotateRight();
        }
    }

    public void RotateLeft()
    {
        obs.Right();

        for (int i = 0; i < cubeColors.Count; i++)
        {
            cubeColors[i].RotateLeft();
        }
    }


}
