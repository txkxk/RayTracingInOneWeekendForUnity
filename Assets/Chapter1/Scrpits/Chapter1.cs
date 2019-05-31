using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter1
{
    public class Chapter1 : MonoBehaviour
    {
        void Start()
        {
            drawScreen();
        }

        void drawScreen()
        {
            for(int x=0;x<Screen.width;x++)
            {
                for(int y = 0;y<Screen.height;y++)
                {
                    DrawScreen.instance.SetPixel(x, y, Color.red);
                }
            }
            DrawScreen.instance.Draw();
        }
    }
}
