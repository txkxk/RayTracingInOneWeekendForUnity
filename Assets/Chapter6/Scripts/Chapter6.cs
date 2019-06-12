using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter6 : MonoBehaviour {

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
    public int AntialiasLevel = 50;
    HitableList world = new HitableList();
    Sphere s1;
    Sphere s2;

    Camera cam;


    void Start()
    {
        //采样次数至少为1
        AntialiasLevel = System.Math.Max(1, AntialiasLevel);

        s1 = new Sphere(sphereCenter1, radius1);
        s2 = new Sphere(sphereCenter2, radius2);
        world.AddHitable(s1);
        world.AddHitable(s2);

        cam = new Camera(lower_left_corner, horizontal, vertical, origin);

        Draw();
    }

    Color color(Ray r)
    {
        RaycastHit rh = new RaycastHit();
        if (world.Hit(r, t_min, t_max, ref rh))
        {
            return new Color((rh.normal.x + 1f) * 0.5f, (rh.normal.y + 1f) * 0.5f, (rh.normal.z + 1f) * 0.5f);//把范围映射到0-1
        }
        Vector3 unit_direction = r.direction.normalized;
        float t = 0.5f * (unit_direction.y + 1f);
        return Color.Lerp(c1, c2, t);
    }

    System.Random random = new System.Random();

    void Draw()
    {
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                Color c = new Color();
                for(int s = 0;s<AntialiasLevel;s++)
                {
                    float u = (x + (float)random.NextDouble()) / (float)Screen.width;
                    float v = (y + Random.Range(0f, 1f - float.Epsilon)) / (float)Screen.height;
                    Ray r = cam.GetRay(u, v);
                    c += color(r);
                }

                c /= AntialiasLevel;

                DrawScreen.instance.SetPixel(x, y, c);
            }
        }
        DrawScreen.instance.Draw();
    }
}
