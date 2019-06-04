using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera
{
    Ray r = new Ray();

    Vector3 lower_left_corner;
    Vector3 horizontal;
    Vector3 vertical;
    Vector3 origin;
    public Camera()
    {
        lower_left_corner = new Vector3(-2f, -1f, -1f);
        horizontal = new Vector3(4f, 0f, 0f);
        vertical = new Vector3(0f, 2f, 0f);
        origin = Vector3.zero;

        r.origin = this.origin;
    }

    public Camera(Vector3 lower_left_corner, Vector3 horizontal, Vector3 vertical, Vector3 origin)
    {
        this.lower_left_corner = lower_left_corner;
        this.horizontal = horizontal;
        this.vertical = vertical;
        this.origin = origin;

        r.origin = this.origin;
    }

    public Ray GetRay(float u,float v)
    {
        r.direction = lower_left_corner + u * horizontal + v * vertical - r.origin;
        return r;
    }
}
