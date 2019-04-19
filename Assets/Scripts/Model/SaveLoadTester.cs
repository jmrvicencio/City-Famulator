using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SaveUtilsHelper;
using EventNameHelper;

public class SaveLoadTester : EditorWindow
{
    public int sliderValue;
    public TestingData slider = new TestingData
    {
        SliderValue = 0
    };

    private void OnEnable()
    {
        SaveHelper.LoadData();
        EventManager.StartListening(EventStrings.OnLoad, OnLoad);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.OnLoad, OnLoad);
    }

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
            SaveHelper.SaveData();
        }
        if (GUILayout.Button("Load"))
        {
            SaveHelper.LoadData(); 
        }
    }

    private void OnLoad()
    {
        slider = SaveHelper.GetData<TestingData>("TestData", new TestingData {
            SliderValue = 0
        });
        Debug.Log("Slider value is: " + slider.SliderValue);
    }
}

public class TestingData
{
    public int SliderValue {get;set;}
}