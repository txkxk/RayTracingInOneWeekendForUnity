using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dielectric : Material
{
    private float ref_idx;
    public Dielectric(float ri)
    {
        ref_idx = ri;
    }
    public override bool scatter(Ray in_ray, ref HitRecord hr, out Color attenuation, out Ray scattered)
    {
        Vector3 outward_normal;
        Vector3 reflected = Vector3.Reflect(in_ray.direction, hr.raycastHit.normal);
        float in_over_nt;
        attenuation = new Color(1f, 1f, 1f);
        Vector3 refracted;
        float reflect_prob;
        float cosine;
        if(Vector3.Dot(in_ray.direction,hr.raycastHit.normal)>0)
        {
            outward_normal = -hr.raycastHit.normal;
            in_over_nt = ref_idx;
            cosine = ref_idx * Vector3.Dot(in_ray.direction, hr.raycastHit.normal) / in_ray.direction.magnitude;
        }
        else
        {
            outward_normal = hr.raycastHit.normal;
            in_over_nt = 1f / ref_idx;
            cosine = - Vector3.Dot(in_ray.direction, hr.raycastHit.normal) / in_ray.direction.magnitude;
        }

        if (Common.refract(in_ray.direction, outward_normal, in_over_nt, out refracted))
        {
            //scattered = new Ray(hr.raycastHit.point, refracted);
            reflect_prob = Common.schlick(cosine, ref_idx);
        }
        else
        {
            reflect_prob = 1.0f;
        }

        if(Common.random.NextDouble() < reflect_prob)
        {
            scattered = new Ray(hr.raycastHit.point, reflected);
        }
        else
        {
            scattered = new Ray(hr.raycastHit.point, refracted);
        }

        return true;
    }
}
