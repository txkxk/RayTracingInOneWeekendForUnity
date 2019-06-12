using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : Material {
    public Metal(Color c)
    {
        albedo = c;
    }
    public override bool scatter(Ray in_ray, ref HitRecord hr, out Color attenuation, out Ray scattered)
    {
        Vector3 reflect = Vector3.Reflect(in_ray.direction.normalized, hr.raycastHit.normal);
        scattered = new Ray(hr.raycastHit.point, reflect);
        attenuation = albedo;
        //入射角度与法线夹角小于90度则射线有效
        return Vector3.Dot(scattered.direction,hr.raycastHit.normal) > 0;
    }

    protected Color albedo;
}
