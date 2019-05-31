using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawScreen
{
    private Texture2D tex2D;
    public static DrawScreen instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new DrawScreen();
            }
            return _instance;
        }
    }

    private static DrawScreen _instance;

    public void SetTexture(Texture2D tex2D)
    {
        this.tex2D = tex2D;
    }

    public void SetPixel(int x,int y,Color color)
    {
        tex2D.SetPixel(x, y, color);
    }

    public void Draw()
    {
        tex2D.Apply();
    }
}
