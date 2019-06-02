using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Hitable
{
    public Sphere() { }

    public Sphere(Vector3 center, float radius)
    {
        this.circleCenter = center;
        this.radius = radius;
    }

    public Vector3 circleCenter
    {
        get;
        private set;
    }

    public float radius
    {
        get;
        private set;
    }

    public override bool Hit(Ray r, float t_min, float t_max, ref RaycastHit rh)
    {
        Vector3 oc = r.origin - circleCenter;
        float a = Vector3.Dot(r.direction, r.direction);
        float b = 2.0f * Vector3.Dot(r.direction, oc);
        float c = Vector3.Dot(oc, oc) - radius * radius;
        float delta = b * b - 4 * a * c;
        if (delta > 0)
        {
            float distance = (-b - Mathf.Sqrt(delta)) / 2 * a;
            //todo t_max和t_min的含义
            if (distance < t_max && distance > t_min)
            {
                rh.distance = distance;
                rh.point = r.GetPoint(rh.distance);
                rh.normal = (rh.point - circleCenter) / radius;
                return true;
            }

            distance = (-b + Mathf.Sqrt(delta)) / 2 * a;
            if (distance < t_max && distance > t_min)
            {
                rh.distance = distance;
                rh.point = r.GetPoint(rh.distance);
                rh.normal = (rh.point - circleCenter) / radius;
                return true;
            }
        }

        return false;
    }
}
