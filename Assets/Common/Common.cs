using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common {
    public static System.Random random = new System.Random(DateTime.Now.Millisecond);

    public static Vector3 random_int_unit_sphere()
    {
        Vector3 p;
        do
        {
            p = 2.0f * new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()) - Vector3.one;
        } while (p.sqrMagnitude >= 1f);//拒绝法，不在单位球内的点不需要
        return p;
    }

    public static bool refract(Vector3 v,Vector3 n,float ni_over_nt,out Vector3 refracted)
    {
        Vector3 uv = v.normalized;
        float dt = Vector3.Dot(uv, n);
        float discriminant = 1.0f - ni_over_nt * ni_over_nt * (1 - dt * dt);
        if(discriminant > 0)
        {
            refracted = ni_over_nt * (uv - n * dt) - n * Mathf.Sqrt(discriminant);
            return true;
        }
        else
        {
            refracted = Vector3.zero;
            return false;
        }
    }

    public static float schlick(float cosine,float ref_idx)
    {
        float r0 = (1f - ref_idx) / (1f + ref_idx);
        r0 = r0 * r0;
        return r0 + (1 - r0) * Mathf.Pow((1 - cosine), 5);
    }
}
