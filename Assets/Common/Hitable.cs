using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hitable {
    public abstract bool Hit(Ray r, float t_min, float t_max, ref RaycastHit rh);
}
