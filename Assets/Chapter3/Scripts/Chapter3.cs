using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter3
{
    public class Chapter3 : MonoBehaviour
    {
        Color c1 = Color.white;
        Color c2 = new Color(0.5f, 0.7f, 1.0f);
        Vector3 lower_left_corner = new Vector3(-2f, -1f, -1f);
        Vector3 horizontal = new Vector3(4f, 0f, 0f);
        Vector3 vertical = new Vector3(0f,2f,0f);
        Vector3 origin = Vector3.zero;
        void Start()
        {
            Draw();
        }

        Color color(Ray r)
        {
            Vector3 unit_direction = r.direction.normalized;
            float t = 0.5f * (unit_direction.y + 1f);
            return Color.Lerp(c1, c2, t);
        }

        void Draw()
        {
            Ray r = new Ray();
            r.origin = origin;
            for (int x = 0; x < Screen.width; x++)
            {
                for (int y = 0; y < Screen.height; y++)
                {
                    float u = (float)x / (float)Screen.width;
                    float v = (float)y / (float)Screen.height;

                    r.direction = lower_left_corner + u * horizontal + v * vertical;
                    DrawScreen.instance.SetPixel(x, y, color(r));
                }
            }
            DrawScreen.instance.Draw();
        }
    }
}

