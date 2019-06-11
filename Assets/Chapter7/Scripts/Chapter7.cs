using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter7 : MonoBehaviour {

    RaycastHit rh;
    Color c1 = Color.white;
    Color c2 = new Color(0.5f, 0.7f, 1.0f,1.0f);
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
    [Range(0f,1f)]
    public float atten = 0.5f;//衰减系数，每次反射都会进行衰减
    HitableList world = new HitableList();
    Sphere s1;
    Sphere s2;

    Camera cam;

    System.Random random = new System.Random(DateTime.Now.Millisecond);


    void Start()
    {
        //采样次数至少为1
        AntialiasLevel = System.Math.Max(1, AntialiasLevel);

        s1 = new Sphere(sphereCenter1, radius1);
        s2 = new Sphere(sphereCenter2, radius2);
        world.list.Add(s1);
        world.list.Add(s2);

        cam = new Camera(lower_left_corner, horizontal, vertical, origin);

        Draw();
    }

    Vector3 random_int_unit_sphere()
    {
        Vector3 p;
        do
        {
            p = 2.0f * new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()) - Vector3.one;
        } while (p.sqrMagnitude >= 1f);//拒绝法，不在单位球内的点不需要
        return p;
    }

    Color color(Ray r,Hitable world)
    {
        RaycastHit rh = new RaycastHit();
        if (world.Hit(r, t_min, t_max, ref rh))
        {
            //point射线与球面的交点,normal是球指向外的法线，point + normal表示与球面相切的单位球的圆心o
            //圆心o加上随机生成的单位球内的一个点s，可以模拟出随机反射方向
            //最终得到以球面交点为起点，s-p为方向的射线，继续寻找射线接触的点
            Vector3 target = rh.point + rh.normal + random_int_unit_sphere();
            Color c = atten * color(new Ray(rh.point, target - rh.point), world);
            c.a = 1;//atten的系数会影响aplha值
            return c;
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
                Color c = new Color();
                for (int s = 0; s < AntialiasLevel; s++)
                {
                    float u = (x + (float)random.NextDouble()) / Screen.width;
                    float v = (y + (float)random.NextDouble()) / Screen.height;
                    Ray r = cam.GetRay(u, v);
                    c += color(r,world);
                }

                c /= AntialiasLevel;
                //模拟gamma校正
                //c.r = Mathf.Sqrt(c.r);
                //c.g = Mathf.Sqrt(c.g);
                //c.b = Mathf.Sqrt(c.b);
                DrawScreen.instance.SetPixel(x, y, c);
            }
        }
        DrawScreen.instance.Draw();
    }
}
