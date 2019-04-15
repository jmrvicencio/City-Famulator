using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SaveUtilsHelper;

public class SaveLoadTester : EditorWindow
{
    public int sliderValue;
    public TestingData slider = new TestingData { SliderValue = 0 };
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

        slider.SliderValue = EditorGUILayout.IntSlider(slider.SliderValue, 0, 100);

        if (GUILayout.Button("Save"))
        {
            SaveHelper.IncludeOnSave(slider, "SaveLoadTest", "DataTest");
            SaveHelper.SaveData();
        }
        if (GUILayout.Button("Load"))
        {
            SaveHelper.LoadData();
            Debug.Log(slider.SliderValue);
        }
    }
}

public class TestingData
{
    public int SliderValue {get;set;}
}