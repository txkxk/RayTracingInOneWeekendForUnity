using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter9 : MonoBehaviour {
    Color c1 = Color.white;
    Color c2 = new Color(0.5f, 0.7f, 1.0f, 1.0f);
    Vector3 lower_left_corner = new Vector3(-2f, -1f, -1f);
    Vector3 horizontal = new Vector3(4f, 0f, 0f);
    Vector3 vertical = new Vector3(0f, 2f, 0f);
    Vector3 origin = Vector3.zero;

    public Vector3 sphereCenter1 = new Vector3(0, 0, -1f);
    public float radius1 = 0.5f;
    public Vector3 sphereCenter2 = new Vector3(0, -100.5f, -1f);
    public float radius2 = 100f;
    public Vector3 sphereCenter3 = new Vector3(1f, 0, -1f);
    public float radius3 = 0.5f;
    public Vector3 sphereCenter4 = new Vector3(-1f, 0, -1f);
    public float radius4 = 0.5f;
    public Vector3 sphereCenter5 = new Vector3(-1f, 0, -1f);
    public float radius5 = -0.45f;
    float t_min = 0.001f;
    float t_max = float.MaxValue;
    public int AntialiasLevel = 50;
    [Range(0f, 1f)]
    public float atten = 0.5f;//衰减系数，每次反射都会进行衰减
    HitableList world = new HitableList();

    Camera cam;

    void Start()
    {
        //采样次数至少为1
        AntialiasLevel = Math.Max(1, AntialiasLevel);

        Sphere s1 = new Sphere(sphereCenter1, radius1, new Lambertian(new Color(0.1f, 0.2f, 0.5f)));
        Sphere s2 = new Sphere(sphereCenter2, radius2, new Lambertian(new Color(0.8f, 0.8f, 0.0f)));
        Sphere s3 = new Sphere(sphereCenter3, radius3, new Metal(new Color(0.8f, 0.6f, 0.2f)));
        Sphere s4 = new Sphere(sphereCenter4, radius4, new Dielectric(1.5f));
        Sphere s5 = new Sphere(sphereCenter5, radius5, new Dielectric(1.5f));
        world.AddHitable(s1);
        world.AddHitable(s2);
        world.AddHitable(s3);
        world.AddHitable(s4);
        world.AddHitable(s5);

        cam = new Camera(lower_left_corner, horizontal, vertical, origin);

        Draw();

    }

    Color color(Ray r, Hitable world, int depth)
    {
        //RaycastHit rh = new RaycastHit();
        HitRecord hr = new HitRecord();
        if (world.Hit(r, t_min, t_max, ref hr))
        {
            Ray scattered;
            Color attenuation;
            if (depth < 50 && hr.material.scatter(r, ref hr, out attenuation, out scattered))
            {
                return attenuation * color(scattered, world, depth + 1);
            }
            else
            {
                return Color.black;
            }
        }
        else
        {
            Vector3 unit_direction = r.direction.normalized;
            float t = 0.5f * (unit_direction.y + 1f);
            return Color.Lerp(c1, c2, t);
        }
    }


    void Draw()
    {
        for (int x = 0; x < Screen.width; x++)
        {
            for (int y = 0; y < Screen.height; y++)
            {
                Color c = new Color();
                for (int s = 0; s < AntialiasLevel; s++)
                {
                    float u = (x + (float)Common.random.NextDouble()) / Screen.width;
                    float v = (y + (float)Common.random.NextDouble()) / Screen.height;
                    Ray r = cam.GetRay(u, v);
                    c += color(r, world, 0);
                }

                c /= AntialiasLevel;

                //gamma校正
                c.r = Mathf.LinearToGammaSpace(c.r);
                c.g = Mathf.LinearToGammaSpace(c.g);
                c.b = Mathf.LinearToGammaSpace(c.b);
                DrawScreen.instance.SetPixel(x, y, c);
            }
        }
        DrawScreen.instance.Draw();
    }
}
