using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class Beziers
{
    
    public static Vector2 BeziersInterpolation(Vector2[] points, float t)
    {
       
        if (points.Length == 1)
        {
            return points[0];
        }
        else
        {
            return (1 - t) * BeziersInterpolation(points.Take((points.Length + 1) / 2).ToArray(), t) + t * BeziersInterpolation(points.Skip(points.Length / 2).ToArray(), t);
        }
    }
    public static List<Vector2> BeziersCurve(Vector2[] points, float step)
    {
        List<Vector2> curve = new List<Vector2>();
        for (float t = step; t < 1 + step; t+=step)
        {
            curve.Add(BeziersInterpolation(points, t));
        }
        return curve;
    }



}
