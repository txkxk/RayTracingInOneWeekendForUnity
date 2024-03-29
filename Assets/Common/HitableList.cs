﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableList : Hitable
{
    List<Hitable> list = new List<Hitable>();

    public override bool Hit(Ray r, float t_min, float t_max, ref RaycastHit rh)
    {
        bool hit_anything = false;
        float closest_so_far = t_max;
        for(int i=0;i<list.Count;i++)
        {
            if(list[i].Hit(r,t_min, closest_so_far, ref rh))
            {
                hit_anything = true;
                closest_so_far = rh.distance;
            }
        }

        return hit_anything;
    }

    public void AddHitable(Hitable hitable_object)
    {
        list.Add(hitable_object);
    }

    public override bool Hit(Ray r, float t_min, float t_max, ref HitRecord hr)
    {
        bool hit_anything = false;
        float closest_so_far = t_max;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Hit(r, t_min, closest_so_far, ref hr))
            {
                hit_anything = true;
                closest_so_far = hr.raycastHit.distance;
            }
        }

        return hit_anything;
    }
}
