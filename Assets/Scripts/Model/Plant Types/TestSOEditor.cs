using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(TestSO))]
[CanEditMultipleObjects]
public class TestSOEditor : Editor
{

    SerializedProperty stringField;
    SerializedProperty stringProperty;
    SerializedProperty intField;
    SerializedProperty objectList;

    private void OnEnable()
    {
        stringField = serializedObject.FindProperty("stringField");
        stringProperty = serializedObject.FindProperty("stringProperty");
        intField = serializedObject.FindProperty("intField");
        objectList = serializedObject.FindProperty("things");
    }

    public override void OnInspectorGUI()
    {
        this.serializedObject.Update();

        TestSO testTarget = (TestSO)target;

        stringField.stringValue = EditorGUILayout.TextField("String Field", stringField.stringValue);
        stringProperty.stringValue = EditorGUILayout.TextField("String Property", stringProperty.stringValue);
        testTarget.IntField = EditorGUILayout.IntField("Int Field", testTarget.IntField);
        intField.intValue = testTarget.IntField;

        SerializedProperty objectListCopy = objectList.Copy();

        Debug.Log(objectListCopy.isArray);

        GUILayout.Space(100);

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            // writing changes of the testScriptable into Undo
            Undo.RecordObject(testTarget, "Test Scriptable Editor Modify");
            // mark the testScriptable object as "dirty" and save it
            EditorUtility.SetDirty(testTarget);
        }
    }
}
