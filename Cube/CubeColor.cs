using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeColor : MonoBehaviour
{
    public IColor up, down, left, right;
    public IColor Up()
    {
        return up;
    }

    public IColor Down()
    {
        return down;
    }

    public IColor Left()
    {
        return left;
    }

    public IColor Right()
    {
        return right;
    }

    public void RotateLeft()
    {
        IColor temp = up;
        up = left;
        left = down;
        down = right;
        right = temp;
    }
        
    public void RotateRight()
    {
        IColor temp = up;
        up = right;
        right = down;
        down = left;
        left = temp;
    }

    public void LeftRightMirror()
    {
        IColor temp = left;
        left = right;
        right = temp;
    }

    public void UpDownMirror()
    {
        IColor temp = up;
        up = down;
        down = temp;
    }



    public void ColorDebug()
    {
        //Debug.Log("up    : " + up.);
        //Debug.Log("down  : " +  down.GetColor().ToString());
        //Debug.Log("left  : " +  left.GetColor().ToString());
        //Debug.Log("right : " +  right.GetColor().ToString());
    }
}


public class Red : CubeColor,IColor
{
    private string color = "Red";
    public string Colors { get => color; }

    public void Init(IColor up, IColor down, IColor left, IColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public Color GetColor()
    {
        return Color.red;
    }

}


public class Magenda : CubeColor, IColor
{
    private string color = "Magenda";
    public string Colors { get => color; }

    public void Init(IColor up, IColor down, IColor left, IColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public Color GetColor()
    {
        return Color.magenta;
    }


}


public class Orange : CubeColor, IColor
{
    private string color = "Orange";
    public string Colors { get => color; }

    public void Init(IColor up, IColor down, IColor left, IColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }


    public Color GetColor()
    {
        return Color.black;
    }


}


public class Yellow : CubeColor, IColor
{
    private string color = "Yellow";
    public string Colors { get => color; }

    public void Init(IColor up, IColor down, IColor left, IColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public Color GetColor()
    {
        return Color.yellow;
    }

}


public class Blue : CubeColor, IColor
{
    private string color = "Blue";
    public string Colors { get => color; }
 
    public void Init(IColor up, IColor down, IColor left, IColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }


    public Color GetColor()
    {
        return Color.blue;
    }

}


public class Lime : CubeColor, IColor
{
    private string color = "Lime";
    public string Colors { get => color; }
    
    public void Init(IColor up, IColor down, IColor left, IColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public Color GetColor()
    {
        return Color.green;
    }

}

