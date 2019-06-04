using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter5 : MonoBehaviour
{
    RaycastHit rh;
    Color c1 = Color.white;
    Color c2 = new Color(0.5f, 0.7f, 1.0f);
    Vector3 lower_left_corner = new Vector3(-2f, -1f, -1f);
    Vector3 horizontal = new Vector3(4f, 0f, 0f);
    Vector3 vertical = new Vector3(0f, 2f, 0f);
    Vector3 origin = Vector3.zero;

    public Vector3 sphereCenter = new Vector3(0, 0.2f, -1f);
    public float radius = 0.5f;
    void Start()
    {
        Draw();
    }

    bool CheckHitSphere(Vector3 center, float radius, Ray r, out float distance)
    {
        distance = 0;
        Vector3 oc = r.origin - center;
        float a = Vector3.Dot(r.direction, r.direction);
        float b = 2.0f * Vector3.Dot(r.direction, oc);
        float c = Vector3.Dot(oc, oc) - radius * radius;
        float delta = b * b - 4 * a * c;
        if (delta > 0)
        {
            //求出来的解表示长度，由于需要找到视线第一个接触的点，所以需要找长度最短的点，因此只取一个解
            distance = (-b - Mathf.Sqrt(delta)) / 2 * a;
        }

        return delta > 0;
    }

    Color color(Ray r)
    {
        float distance;
        if (CheckHitSphere(sphereCenter, radius, r, out distance))
        {
            Vector3 p = r.GetPoint(distance);
            Vector3 normal = (p - sphereCenter).normalized;//单位法线的分量范围为-1——1
            return new Color(0.5f * (normal.x + 1f), 0.5f * (normal.y + 1f), 0.5f * (normal.z + 1f));//把范围映射到0-1
        }
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
