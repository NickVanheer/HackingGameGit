using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[CustomEditor(typeof(CurveTrack))]
public class CurveTrackEditor : Editor {

    public bool IsSetYTo0 = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);
        if (GUILayout.Button("Refresh", GUILayout.Height(30)))
        {
            var lineTrack = (CurveTrack)target;
            lineTrack.CalculateStartPoints();

            EditorUtility.SetDirty(((CurveTrack)target));
        }


        if (GUILayout.Button("Reset on Transform"))
        {
            var lineTrack = (CurveTrack)target;
            lineTrack.ResetOnTransform();

            EditorUtility.SetDirty(((CurveTrack)target));
        }


        GUILayout.Space(20);
       
        EditorGUILayout.LabelField("Utilities", EditorStyles.boldLabel);

        IsSetYTo0 = GUILayout.Toggle(IsSetYTo0, "Y is 0 when moving");

        if (GUILayout.Button("Reset Y"))
        {
            var lineTrack = (CurveTrack)target;
            lineTrack.SetAllY(0);
            lineTrack.transform.SetY(0);
            lineTrack.StartPoint.y = 0;
            lineTrack.StartControlPoint.y = 0;
            lineTrack.EndPoint.y = 0;
            lineTrack.EndControlPoint.y = 0;
        }
    }

    void OnSceneGUI()
    {
        var simpleSpline = (CurveTrack)target;

        simpleSpline.StartControlPoint = Handles.FreeMoveHandle(simpleSpline.StartControlPoint, Quaternion.identity, 0.2f, Vector3.zero, Handles.CircleCap);
        simpleSpline.EndControlPoint = Handles.FreeMoveHandle(simpleSpline.EndControlPoint, Quaternion.identity, 0.2f, Vector3.zero, Handles.CircleCap);

        Vector3 originalStart = simpleSpline.StartPoint;
        Vector3 originalEnd = simpleSpline.EndPoint;

        Vector3 moveStart = Handles.FreeMoveHandle(simpleSpline.StartPoint, Quaternion.identity, 0.2f, Vector3.zero, Handles.RectangleCap);
        Vector3 moveEnd = Handles.FreeMoveHandle(simpleSpline.EndPoint, Quaternion.identity, 0.2f, Vector3.zero, Handles.RectangleCap);

        Vector3 startDelta = moveStart - originalStart;
        Vector3 endDelta = moveEnd - originalEnd;

        if(IsSetYTo0)
        {
            simpleSpline.SetAllY(0);
            simpleSpline.transform.SetY(0);
            simpleSpline.StartPoint.y = 0;
            simpleSpline.StartControlPoint.y = 0;
            simpleSpline.EndPoint.y = 0;
            simpleSpline.EndControlPoint.y = 0;
        }

        simpleSpline.StartPoint += startDelta;
        simpleSpline.StartControlPoint += startDelta;

        simpleSpline.EndPoint += endDelta;
        simpleSpline.EndControlPoint += endDelta;

        if (GUI.changed)
        {
            simpleSpline.CalculateStartPoints();
            EditorUtility.SetDirty(simpleSpline);
        }
    }
}
