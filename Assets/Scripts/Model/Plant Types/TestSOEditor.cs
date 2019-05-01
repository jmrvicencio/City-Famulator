using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(TestSO))]
[CanEditMultipleObjects]
public class TestSOEditor : Editor
{

    TestSO t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    SerializedProperty ThisDict;
    int ListSize;

    private void OnEnable()
    {
        t = (TestSO)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("myList");
        ThisDict = GetTarget.FindProperty("myDictionary");
    }

    public override void OnInspectorGUI()
    {
        GetTarget.Update();

        ListSize = ThisList.arraySize;
        ListSize = EditorGUILayout.IntField("List Size", ListSize);

        if (ListSize != ThisList.arraySize)
        {
            while (ListSize > ThisList.arraySize)
            {
                ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
            }
            while (ListSize < ThisList.arraySize)
            {
                ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
            }
        }

        //PlantTypeDictionary test = (PlantTypeDictionary) ThisDict.objectReferenceValue;

        if (GUILayout.Button("Add New"))
        {
            ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
        }

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);
            SerializedProperty MyString = MyListRef.FindPropertyRelative("ObjectString");
            SerializedProperty MyInt = MyListRef.FindPropertyRelative("ObjectInt");

            EditorGUILayout.PropertyField(MyString);
            EditorGUILayout.PropertyField(MyInt);
        }

        GetTarget.ApplyModifiedProperties();
    }
}
