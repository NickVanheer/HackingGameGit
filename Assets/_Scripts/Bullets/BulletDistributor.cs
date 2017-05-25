using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public enum BulletSpawnMode
{
    TowardsPlayer, OnTrack
}

public enum TrackMoveMode
{
    NoMove, Range, Circle
}

/// <summary>
/// Fires bullets either in the direction of the player or on a specified LineTrack path
/// </summary>
public class BulletDistributor : MonoBehaviour {

    // Use this for initialization
    public Transform Player;
    public BulletSpawnMode SpawnMode;
    
    public float ActivationRadius = 60f;
    public float StartDelay = 0f;
    public bool IsIgnoreActivationRadius = false;

    [Header("Bullet properties")]
    public GameObject OrangeBulletPrefab;
    public GameObject PurpleBulletPrefab;

    [Range(0.02f, 1.0f)]
    public float FireCooldown = 0.5f;

    [Range(10f, 70f)]
    public float BulletSpeed = 2f;

    public bool IsBulletsIgnoreWalls = false;

    [Header("Bullet follow track properties")]
    public LineTrack TrackToSpawnOn;
    public bool IsTrackHiddenInEditor = true;
    public TrackMoveMode MoveMode = TrackMoveMode.NoMove;
    [Range(0.0f, 30.0f)]
    public float TrackMoveRangeHorizontal = 1.0f; //movemode range
    [Range(0.0f, 30.0f)]
    public float TrackMoveRangeVertical = 1.0f; //movemode range
    [Range(5.0f, 60.0f)]
    public float TrackCircleRadius = 30.0f; //movemode circle
    [Range(0.0f, 3.0f)]
    public float TrackMoveSpeed = 3.0f;

    public float TrackStartT = 0.0f;

    [Header("1 wave bullet properties")]
    public float TotalCount;
    public float NumberOfOrangeBullets;

    //privates
    private float coolDownTimer = 0;
    private float index = 0;
    private Vector3 sineTrackOriginalPos;
    private float circleTrackT = 0.0f;
    private float onTrackBulletSpeedIncrement = 3f;
    private float trackSine = 0.0f;

    void Start () {

        if (SpawnMode == BulletSpawnMode.OnTrack)
            Assert.IsNotNull<LineTrack>(TrackToSpawnOn, "BulletDistributor > Bullets trying to spawn on a track but no track is assigned.");
            
        //cache of the original second point, which will be modified later on
        if (SpawnMode == BulletSpawnMode.OnTrack)
        {
            sineTrackOriginalPos = TrackToSpawnOn.ShapePoints[1];
            circleTrackT = TrackStartT;
        }
    }


    void OnDrawGizmos()
    {
        //hack really, but it works in the editor
        if (this.transform.hasChanged && TrackToSpawnOn != null)
        {
            TrackToSpawnOn.ShapePoints[0] = transform.position;
            TrackToSpawnOn.IsHiddenInEditor = IsTrackHiddenInEditor;
        }
    }

    // Update is called once per frame
    void Update () {

        if (Player == null)
            return;

        if (StartDelay > 0)
        {
            StartDelay -= Time.deltaTime;
            return;
        }

        //return and do nothing when there's too much distance
        bool isTooFar = Vector3.Distance(Player.transform.position, this.transform.position) >= ActivationRadius;
        if (isTooFar && !IsIgnoreActivationRadius)
            return;

        coolDownTimer -= Time.deltaTime;
        if(coolDownTimer <= 0)
        {
            ShootBullet();
            coolDownTimer = 1 - FireCooldown;

            index++;

            if (index >= TotalCount)
                index = 0;
        }

        if (SpawnMode == BulletSpawnMode.OnTrack)
        {
            TrackToSpawnOn.ShapePoints[0] = transform.position;

            if (MoveMode == TrackMoveMode.Range)
            {
                float valSine = Mathf.Sin(trackSine);

                TrackToSpawnOn.ShapePoints[1] = new Vector3(sineTrackOriginalPos.x + valSine * TrackMoveRangeHorizontal, sineTrackOriginalPos.y, sineTrackOriginalPos.z + valSine * TrackMoveRangeVertical);
                trackSine += Time.deltaTime * TrackMoveSpeed;
            }

            if (MoveMode == TrackMoveMode.Circle)
            {
                Vector3 point = CalculateCirclePoint(circleTrackT, TrackCircleRadius, transform.position.x, transform.position.z);
                TrackToSpawnOn.ShapePoints[1] = point;

                circleTrackT += Time.deltaTime * TrackMoveSpeed;
            }
            //end position
        }
    }

    public void ShootBullet()
    {
        GameObject gO;
        if(index < NumberOfOrangeBullets)
             gO = Utils.InstantiateSafe(OrangeBulletPrefab, transform.position);
        else
            gO = Utils.InstantiateSafe(PurpleBulletPrefab, transform.position);

        if (SpawnMode == BulletSpawnMode.OnTrack)
        {
            //on bullet object - Patroller
            PatrolEnemy e = gO.AddComponent<PatrolEnemy>();
            e.Track = TrackToSpawnOn;
            e.SetTrajectory(0.0f);
            e.MoveSpeed = BulletSpeed * onTrackBulletSpeedIncrement;
            e.IsNoReverse = true;
            e.IsDestroyAtEnd = true;

            Bullet b = gO.GetComponent<Bullet>();
            //b.Speed = BulletSpeed; //doesn't do anything because bullets aren't moving on their own.
            b.IsImmortal = true;
        }

        if(SpawnMode == BulletSpawnMode.TowardsPlayer)
        {
            Vector3 playerDirection = Player.position - this.transform.position;
            Bullet b = gO.GetComponent<Bullet>();
            b.Speed = BulletSpeed;

            if (IsBulletsIgnoreWalls)
                b.IsIgnoreWall = true;
            //move towards player
            b.Move(playerDirection.normalized);
         }

        //parent, can be optimized
        string name = this.gameObject.name + " bullet list";
        GameObject groupObject = GameObject.Find(name);

        if (groupObject == null)
            groupObject = new GameObject(name);

        gO.transform.parent = groupObject.transform;

    }

    public Vector3 CalculateCirclePoint(float angleInRadians, float rad, float xCenter, float yCenter)
    {
        float x, y;
        x = Mathf.Cos(angleInRadians) * rad + xCenter;
        y = Mathf.Sin(angleInRadians) * rad + yCenter;
        return new Vector3(x, 0, y);
    }
}
