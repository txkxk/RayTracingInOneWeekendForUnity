using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter5_1 : MonoBehaviour {

    RaycastHit rh;
    Color c1 = Color.white;
    Color c2 = new Color(0.5f, 0.7f, 1.0f);
    Vector3 lower_left_corner = new Vector3(-2f, -1f, -1f);
    Vector3 horizontal = new Vector3(4f, 0f, 0f);
    Vector3 vertical = new Vector3(0f, 2f, 0f);
    Vector3 origin = Vector3.zero;

    public Vector3 sphereCenter1 = new Vector3(0, 0, -1f);
    public float radius1 = 0.5f;
    public Vector3 sphereCenter2 = new Vector3(0, -100.5f, -1f);
    public float radius2 = 100f;
    float t_min = 0;
    float t_max = float.MaxValue;
    HitableList world = new HitableList();
    Sphere s1;
    Sphere s2;
    public Color ballColor = Color.red;


    void Start()
    {
        s1 = new Sphere(sphereCenter1, radius1);
        s2 = new Sphere(sphereCenter2, radius2);
        world.list.Add(s1);
        world.list.Add(s2);

        Draw();
    }

    //bool CheckHitSphere(Ray r, ref RaycastHit rh)
    //{
        
    //}

    Color color(Ray r)
    {
        RaycastHit rh = new RaycastHit();
        if (world.Hit(r,t_min,t_max,ref rh))
        {
            return new Color((rh.normal.x + 1f) * 0.5f, (rh.normal.y + 1f) * 0.5f, (rh.normal.z + 1f) * 0.5f) ;//把范围映射到0-1
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
