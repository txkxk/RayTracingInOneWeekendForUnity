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
}
