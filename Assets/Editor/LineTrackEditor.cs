using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[CustomEditor(typeof(LineTrack))]
public class LineTrackEditor : Editor {

    private bool IsSetYTo0 = true;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset", GUILayout.Height(30)))
        {
            var lineTrack = (LineTrack)target;
            lineTrack.CalculateStartPoints();
        }

        GUILayout.Space(20);
        GUILayout.Label("Utilities");

        IsSetYTo0 = EditorGUILayout.Toggle("Y is 0 when moving", IsSetYTo0);
        if (GUILayout.Button("Reset Y"))
        {
            var lineTrack = (LineTrack)target;
            lineTrack.SetAllY(0);
            lineTrack.transform.SetY(0);
        }

        if (GUILayout.Button("Center transform"))
        {
            var lineTrack = (LineTrack)target;
            lineTrack.CenterTransform();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Add line"))
        {
            var lineTrack = (LineTrack)target;
            lineTrack.AddNewLine();
        }

        if (GUILayout.Button("Remove last line"))
        {
            var lineTrack = (LineTrack)target;
            lineTrack.RemoveLast();
        }
    }

    void OnSceneGUI()
    {
        var lineTrack = (LineTrack)target;

        for (int i = 0; i < lineTrack.ShapePoints.Count; i++)
        {
            lineTrack.ShapePoints[i] = Handles.FreeMoveHandle(lineTrack.ShapePoints[i], Quaternion.identity, 0.4f, Vector2.zero, Handles.CircleCap);
            Handles.Label(lineTrack.ShapePoints[i], string.Format("[{0}]", i.ToString()));
        }

        EditorUtility.SetDirty(target);
    }
}
