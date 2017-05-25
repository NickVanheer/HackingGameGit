using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base class for tracks containing drawing methods, list of points, abstract methods that derived classes should implement,...
/// </summary>
public abstract class Track : MonoBehaviour {

    public List<Vector3> ShapePoints = new List<Vector3>(); //preferred to use methods below, but accessible if needed
    public float EditorVectorSize = 0.1f;

    public abstract Vector3 GetPointOnTrack(float n);
    public abstract void CalculateStartPoints();

    public bool IsHiddenInEditor = false;

    public void DrawVertexPoints()
    {
        if (IsHiddenInEditor)
            return;

        //point spheres
        Gizmos.color = Color.green;
        ShapePoints.ForEach(p => Gizmos.DrawWireSphere(p, EditorVectorSize));
    }

    public void DrawPointLines()
    {
        if (IsHiddenInEditor)
            return;

        for (int i = 0; i < Count() - 1; ++i)
        {
            Gizmos.DrawLine(GetPointAtIndex(i), GetPointAtIndex(i + 1));
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        DrawPointLines();
        DrawVertexPoints();
    }

    public void AddPoint(Vector3 point)
    {
        ShapePoints.Add(point);
    }

    public void RemovePoint(int index)
    {
        if (index < ShapePoints.Count)
            ShapePoints.RemoveAt(index);
    }

    public void ClearPoints()
    {
        ShapePoints.Clear();
    }

    public Vector3 GetPointAtIndex(int i)
    {
        return ShapePoints[i];
    }

    public int Count()
    {
        return ShapePoints.Count;
    }

    public void SetAllY(float value)
    {
        for (int i = 0; i < ShapePoints.Count; i++)
        {
            ShapePoints[i] = new Vector3(ShapePoints[i].x, value, ShapePoints[i].z);
        }
    }



}
