using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[CustomEditor(typeof(CircleTrack))]
public class CircleTrackEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);
        if (GUILayout.Button("Refresh"))
        {
            var circleTrack = (CircleTrack)target;
            circleTrack.CalculateStartPoints();
        }
    }
}
