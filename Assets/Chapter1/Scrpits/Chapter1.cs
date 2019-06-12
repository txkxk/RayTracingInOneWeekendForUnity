using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1 : MonoBehaviour
{
    public Color c;

    void Start()
    {
        drawScreen();
    }

    void drawScreen()
    {
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                DrawScreen.instance.SetPixel(x, y, c);
            }
        }
        DrawScreen.instance.Draw();
    }
}
