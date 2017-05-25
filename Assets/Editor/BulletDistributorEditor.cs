using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[CustomEditor(typeof(BulletDistributor))]
public class BulletDistributorEditor : Editor{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate new bullet path",GUILayout.Height(30)))
        {
            BulletDistributor b = ((BulletDistributor)target);

            GameObject newG = new GameObject("Bullet spawn path");
            newG.transform.position = b.transform.position;
            LineTrack l = newG.AddComponent<LineTrack>();
            l.CalculateStartPoints(0, -15);
            b.MoveMode = TrackMoveMode.Circle;
            b.SpawnMode = BulletSpawnMode.OnTrack;
            b.TrackToSpawnOn = l;
        }

    }
}
