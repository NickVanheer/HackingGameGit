using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackerEnemy : MonoBehaviour {

    public float RotateSpeed = 80.0f;

    public GameObject TrackTarget;
    private static List<TrackerEnemy> TrackerEnemies = new List<TrackerEnemy>();

    public Vector3 Velocity;

    // Use this for initialization
    void Start () {
        TrackerEnemies.Add(this);
    }
	
	// Update is called once per frame
	void Update () {

        if (TrackTarget == null)
            return; 

        float x = 0f;
        float y = 0f;

        //direction
        var thisPos = Camera.main.WorldToViewportPoint(this.transform.position); //0-1
        var playerScreen = Camera.main.WorldToViewportPoint(TrackTarget.transform.position); //0-1

        //point from player to target
        Vector3 dir = playerScreen - thisPos;
        x = dir.x;
        y = dir.y;

        RotateTowards(new Vector3(x, 0, y));
    }


    void RotateTowards(Vector3 direction)
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, RotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
