using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObservColor : MonoBehaviour
{
    [SerializeField]
    private Image[] colorsImage;

    public void Left()
    {
        Color temp = colorsImage[0].color;
        colorsImage[0].color = colorsImage[1].color;
        colorsImage[1].color = colorsImage[2].color;
        colorsImage[2].color = colorsImage[3].color;
        colorsImage[3].color = temp;

    }

    public void Right()
    {
        Color temp = colorsImage[0].color;
        colorsImage[0].color = colorsImage[3].color;
        colorsImage[3].color = colorsImage[2].color;
        colorsImage[2].color = colorsImage[1].color;
        colorsImage[1].color = temp;
    }
}
