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


public class Oragne : CubeColor, IColor
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
        return Color.black;
    }


}


public class Yellow : CubeColor, IColor
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
        return Color.yellow;
    }

}


public class Blue : CubeColor, IColor
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
        return Color.blue;
    }

}


public class Lime : CubeColor, IColor
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
        return Color.green;
    }

}

