using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lambertian : Material
{
    public Lambertian(Color c)
    {
        albedo = c;
    }

    public override bool scatter(Ray in_ray, ref HitRecord hr, out Color attenuation, out Ray scattered)
    {
        Vector3 target = hr.raycastHit.point + hr.raycastHit.normal + Common.random_int_unit_sphere();
        scattered = new Ray(hr.raycastHit.point, target - hr.raycastHit.point);
        attenuation = albedo;
        return true;
    }

    protected Color albedo;
}
