using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SaveUtilsHelper;

public class SaveLoadTester : EditorWindow
{
    public int testValue;

    [MenuItem("Window/Save Load Tester")]
    public static void ShowWindow()
    {
        GetWindow<SaveLoadTester>("Save Load Tester");
    }

    private void OnGUI()
    {
        GUIStyle headings = new GUIStyle();
        headings.fontSize = 13;
        GUILayout.Label("Test Value", headings);

        testValue = EditorGUILayout.IntSlider(testValue, 0, 100);

        int[] intArray = new int[]{ 1,2,3,4};
        List<object> testData = new List<object>
        {
            testValue,
            intArray
        };

        if (GUILayout.Button("Save"))
        {
            SaveHelper.SaveData();
        }
        if (GUILayout.Button("Load"))
        {
            SaveHelper.LoadData();
        }
    }
}