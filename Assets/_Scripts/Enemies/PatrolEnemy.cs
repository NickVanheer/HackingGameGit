using UnityEngine;
using System.Collections;

public class PatrolEnemy : MonoBehaviour {

    public Track Track;

    [Range(10f, 180.0f)]
    public float MoveSpeed = 20;
    public bool IsNoReverse;
    private float trajectory = 0.0f;
    public bool IsDestroyAtEnd = false;
    public bool IsIgnoreY = false;
    private float moveDirection = 1;


	void FixedUpdate () {
        MoveEnemy();
	}

    void MoveEnemy()
    {

        //feeds in a value from 0 -> 1 and back to GetPointOnTrack
        if (!IsNoReverse)
       { 
            if (trajectory <= 0.01f || trajectory >= 0.99f)
            {
                trajectory = Mathf.Clamp(trajectory, 0.01f, 0.99f);
                moveDirection *= -1;
            }
        }

        trajectory += Time.deltaTime * (MoveSpeed / 100) * moveDirection;

        Vector3 position = Track.GetPointOnTrack(trajectory);

        if(IsIgnoreY)
            position.y = this.transform.position.y;

        this.transform.position = position;

        if(trajectory > 1.0f && IsDestroyAtEnd)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTrajectory(float t)
    {
        trajectory = t;
    }
}
