using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

[CustomEditor(typeof(DebuggingController))]
public class DebuggingControllerEditor : Editor
{
    SerializedProperty meter;
    SerializedProperty meterNum;
    SerializedProperty light;
    // Start is called before the first frame update
    void OnEnable()
    {
        meter = serializedObject.FindProperty("meter");
        meterNum = serializedObject.FindProperty("meterNum");
        light = serializedObject.FindProperty("uiLight");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Change meter 1") && meter != null)
        {
            ((MeterControl)meter.objectReferenceValue).Value = meterNum.intValue;
            Debug.Log("changed meter to " + meterNum.intValue.ToString());
        }
        if (GUILayout.Button("Toggle Light") && light != null)
        {
            LightControl lightRef = ((LightControl)light.objectReferenceValue);
            lightRef.Lit = !lightRef.Lit;
        }
    }
}
