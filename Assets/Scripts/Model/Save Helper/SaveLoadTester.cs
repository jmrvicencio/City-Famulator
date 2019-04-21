using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SaveUtilsHelper;

public class SaveLoadTester : EditorWindow
{
    public int sliderValue;
    public TestingData slider = new TestingData
    {
        TestSliderValue = 0
    };

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.OnLoad, OnLoad);
        SaveHelper.LoadData();
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

        slider.TestSliderValue = EditorGUILayout.IntSlider(slider.TestSliderValue, 0, 100);

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
            TestSliderValue = 0
        });
        //Debug.Log("LoadData has been called from SaveLoad Tester",this);
    }
}

public class TestingData
{
    public int TestSliderValue {get;set;}
}