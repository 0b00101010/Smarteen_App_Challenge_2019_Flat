using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Red : IColor
{
    private string color = "Red";
    public string Colors { get => color; }
    public IColor up, down, left, right;
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


public class Magenda : IColor
{
    private string color = "Magenda";
    public string Colors { get => color; }
    public IColor up, down, left, right;

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


public class Oragne : IColor
{
    private string color = "Red";
    public string Colors { get => color; }
    public IColor up, down, left, right;
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


public class Yellow : IColor
{
    private string color = "Red";
    public string Colors { get => color; }
    public IColor up, down, left, right;
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


public class Blue : IColor
{
    private string color = "Red";
    public string Colors { get => color; }
    public IColor up, down, left, right;
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


public class Lime : IColor
{
    private string color = "Red";
    public string Colors { get => color; }
    public IColor up, down, left, right;
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

