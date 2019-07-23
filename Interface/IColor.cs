using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IColor
{
    void Init(IColor up, IColor down, IColor left, IColor right);
    IColor Up();
    IColor Down();
    IColor Left();
    IColor Right();
    Color GetColor();
    void RotateLeft();
    void RotateRight();
    void ColorDebug();
}
