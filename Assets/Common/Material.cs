using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Material {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="in_ray">射入射线</param>
    /// <param name="hr">射线碰撞结果</param>
    /// <param name="attenuation">当散射时，反射率R应使光线衰减多少（即材质本身吸收了多少）</param>
    /// <param name="scattered">新的散射射线</param>
    /// <returns>函数有无计算散射射线</returns>
    public abstract bool scatter(Ray in_ray, ref HitRecord hr, out Color attenuation, out Ray scattered);
}
