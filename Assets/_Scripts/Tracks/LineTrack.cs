using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class LineTrack : Track {

    public float pointOnLine = 0.5f;

    public override void CalculateStartPoints()
    {
        ClearPoints();

        AddPoint(transform.position - new Vector3(15, 0, 0));
        AddPoint(transform.position + new Vector3(15, 0, 0));

    }

    public void CalculateStartPoints(int xOffset, int zOffset)
    {
        ClearPoints();

        AddPoint(transform.position - new Vector3(xOffset, 0, zOffset));
        AddPoint(transform.position + new Vector3(xOffset, 0, zOffset));

    }

    public void CenterTransform()
    {
        transform.position = GetPointOnTrack(0.5f);
    }

    public override Vector3 GetPointOnTrack(float n)
    {
        Vector3 result = Vector3.Lerp(GetPointAtIndex(0), GetPointAtIndex(Count() - 1), n);  //ins't great when working with multiple points
        return result;
    }

    //Adds a new point on the collection based on the previous point
    public void AddNewLine()
    {
        Vector3 point = GetPointAtIndex(Count() - 1);

        point += new Vector3(3, 0, 3);
        AddPoint(point);
    }

    public void RemoveLast()
    {
        RemovePoint(Count() - 1);
    }
}
