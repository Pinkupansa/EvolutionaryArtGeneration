using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonalLine : MonoBehaviour
{
    public PolygonalLine(List<Vector2> _points, Color _color)
    {
        points = _points;
        color = _color;
    }

    public static PolygonalLine Random()
    {
        int numberOfPoints = UnityEngine.Random.Range(2,10);
        List<Vector2> _points = new List<Vector2>();
        for(int i = 0; i < numberOfPoints; i++)
        {
            _points.Add(new Vector2(UnityEngine.Random.Range(0f,1f), UnityEngine.Random.Range(0f,1f)));
        }
        Color _color = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
        return new PolygonalLine(_points, _color);
    }
    public static PolygonalLine Merge(PolygonalLine pline1, PolygonalLine pline2)
    {
        List<Vector2> _points = pline1.points;
        _points.AddRange(pline2.points);
        return new PolygonalLine(_points, pline1.color);
    }
    public List<Vector2> points; 
    
    public Color color;

}
