using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Represents a circle path in the editor
/// </summary>
public class CircleTrack : Track {

    public float Radius = 30.0f;
    public int InterpolationSteps = 20;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override Vector3 GetPointOnTrack(float n)
    {
        n = n * (float)Mathf.PI * 2; 
        float x, y;
        x = Mathf.Cos(n) * Radius + transform.position.x;
        y = Mathf.Sin(n) * Radius + transform.position.z;
        return new Vector3(x, 0, y);
    }

    /*
    public Vector3 CalculateCirclePoint(float angleInRadians)
    {
        float x, y;
        x = Mathf.Cos(angleInRadians) * Radius + transform.position.x;
        y = Mathf.Sin(angleInRadians) * Radius + transform.position.z;
        return new Vector3(x, 0, y);
    }
    */

    public override void CalculateStartPoints()
    {
        ClearPoints();

        float x, y;

        //
        float slice = 2 * Mathf.PI / InterpolationSteps;
        for (float i = 0; i <= InterpolationSteps; i++)
        {
            float angle = slice * i;

            x = Mathf.Cos(angle) * Radius + transform.position.x;
            y = Mathf.Sin(angle) * Radius + transform.position.z;

            AddPoint(new Vector3(x, 0, y));
        }
    }
}
