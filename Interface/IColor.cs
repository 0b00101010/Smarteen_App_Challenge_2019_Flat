using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IColor
{
    // void Init(IColor up, IColor down, IColor left, IColor right);
    CubeColor Up();
    CubeColor Down();
    CubeColor Left();
    CubeColor Right();
    // Color GetColor();
    void UpDownMirror();
    void LeftRightMirror(  );
    void RotateLeft();
    void RotateRight();
    void ColorDebug();
}
