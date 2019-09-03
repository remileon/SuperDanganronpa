using UnityEngine;

public class BezierUtils
{
    public static Vector3 Cubic(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        var u = 1 - t;
        var tt = t * t;
        var uu = u * u;
        var uuu = uu * u;
        var ttt = tt * t;

        var p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    public static float CubicLength(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        var chord = (p3 - p0).magnitude;
        var contNet = (p0 - p1).magnitude + (p2 - p1).magnitude + (p3 - p2).magnitude;

        var appArcLength = (contNet + chord) / 2;

        return appArcLength;
    }
}