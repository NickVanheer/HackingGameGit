using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[CustomEditor(typeof(PatrolEnemy))]
public class PatrolEnemyEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate new patrol path", GUILayout.Height(30)))
        {
            PatrolEnemy p = ((PatrolEnemy)target);

            GameObject newG = new GameObject("Enemy patrol path");
            newG.transform.position = p.transform.position;
            LineTrack l = newG.AddComponent<LineTrack>();
            l.CalculateStartPoints();
            p.Track = l;
        }

    }
}
