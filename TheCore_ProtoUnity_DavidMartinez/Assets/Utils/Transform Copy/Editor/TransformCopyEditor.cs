using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransformCopy))]
public class TransformCopyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Process"))
        {
            Process();
        }
        EditorGUILayout.EndHorizontal();
    }

    void Process()
    {
        TransformCopy t = (TransformCopy)target;
        t.localRelativePosition = t.transformToCopyFrom.InverseTransformPoint(t.transform.position);
        t.relativeRotation = Quaternion.Inverse(t.transform.rotation) * t.transformToCopyFrom.rotation;
        EditorUtility.SetDirty(t);
    }
}
