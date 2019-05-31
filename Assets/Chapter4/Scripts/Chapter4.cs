using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter4 : MonoBehaviour {

    Color c1 = Color.white;
    Color c2 = new Color(0.5f, 0.7f, 1.0f);
    Vector3 lower_left_corner = new Vector3(-2f, -1f, -1f);
    Vector3 horizontal = new Vector3(4f, 0f, 0f);
    Vector3 vertical = new Vector3(0f, 2f, 0f);
    Vector3 origin = Vector3.zero;

    public Vector3 sphereCenter = new Vector3(0, 0.2f, -1f);
    public float radius = 0.5f;
    public Color ballColor = Color.red;
    void Start()
    {
        Draw();
    }

    bool CheckHitSphere(Vector3 center,float radius,Ray r)
    {
        Vector3 oc = r.origin - center;
        float a = Vector3.Dot(r.direction, r.direction);
        float b = 2.0f * Vector3.Dot(r.direction , oc);
        float c = Vector3.Dot(oc, oc) - radius * radius;

        return (b * b - 4 * a * c) > 0;
    }

    Color color(Ray r)
    {
        if(CheckHitSphere(sphereCenter,radius,r))
        {
            return ballColor;
        }
        Vector3 unit_direction = r.direction.normalized;
        float t = 0.5f * (unit_direction.y + 1f);
        return Color.Lerp(c1, c2, t);
    }

    void Draw()
    {
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                float u = (float)x / (float)Screen.width;
                float v = (float)y / (float)Screen.height;

                Ray r = new Ray(origin, lower_left_corner + u * horizontal + v * vertical);
                DrawScreen.instance.SetPixel(x, y, color(r));
            }
        }
        DrawScreen.instance.Draw();
    }
}
