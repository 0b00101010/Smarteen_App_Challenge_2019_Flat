using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeColor : MonoBehaviour
{
    public CubeColor up, down, left, right;

    public CubeColor Up()
    {
        return up;
    }

    public CubeColor Down()
    {
        return down;
    }

    public CubeColor Left()
    {
        return left;
    }

    public CubeColor Right()
    {
        return right;
    }

    public void RotateLeft()
    {
        CubeColor temp = up;
        up = left;
        left = down;
        down = right;
        right = temp;
    }

    public void RotateRight()
    {
        CubeColor temp = up;
        up = right;
        right = down;
        down = left;
        left = temp;
    }

    public void LeftRightMirror()
    {
        CubeColor temp = left;
		left = right;
		right = temp;

		Debug.Log("Mirror_LR");
    }

    public void UpDownMirror()
    {
        CubeColor temp = up;

		up = down;
		down = temp;

		Debug.Log("Mirror_UD");

	}

	public virtual void Init(CubeColor up, CubeColor down, CubeColor left, CubeColor right) { }
    public virtual Color GetColor() { return Color.white; }
  
}


public class Red : CubeColor
{
    private string color = "Red";
    public string Colors { get => color; }

    public override void Init(CubeColor up, CubeColor down, CubeColor left, CubeColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public override Color GetColor()
    {
        return Color.red;
    }

}


public class Magenda : CubeColor
{
    private string color = "Magenda";
    public string Colors { get => color; }

    public override void Init(CubeColor up, CubeColor down, CubeColor left, CubeColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public override Color GetColor()
    {
        return Color.magenta;
    }


}


public class Orange : CubeColor
{
    private string color = "Orange";
    public string Colors { get => color; }

    public override void Init(CubeColor up, CubeColor down, CubeColor left, CubeColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }


    public override Color GetColor()
    {
        return Color.black;
    }


}


public class Yellow : CubeColor
{
    private string color = "Yellow";
    public string Colors { get => color; }

    public override void Init(CubeColor up, CubeColor down, CubeColor left, CubeColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public override Color GetColor()
    {
        return Color.yellow;
    }

}


public class Blue : CubeColor
{
    private string color = "Blue";
    public string Colors { get => color; }
 
    public override void Init(CubeColor up, CubeColor down, CubeColor left, CubeColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }


    public override Color GetColor()
    {
        return Color.blue;
    }

}


public class Lime : CubeColor
{
    private string color = "Lime";
    public string Colors { get => color; }
    
    public override void Init(CubeColor up, CubeColor down, CubeColor left, CubeColor right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public override Color GetColor()
    {
        return Color.green;
    }

}

