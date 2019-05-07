using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SaveUtilsHelper;

public class TestingHelper : EditorWindow
{
    FarmulatorApplication app;
    Vector2 scrollPosition = Vector2.zero;
    public int sliderValue;
    public TestingData slider = new TestingData
    {
        TestSliderValue = 0
    };

    private void OnEnable()
    {
        EventManager.StartListening(EventStrings.OnLoad, OnLoad);
        SaveHelper.LoadData();
        app = GameObject.FindObjectOfType<FarmulatorApplication>();
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventStrings.OnLoad, OnLoad);
    }

    [MenuItem("Window/Testing Helper")]
    public static void ShowWindow()
    {
        GetWindow<TestingHelper>("Save Load Tester");
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height - 20));

        if (GUILayout.Button("Add Day"))
        {
            app.model.world.AddDay();
        }

        GUIStyle headings = new GUIStyle();
        headings.fontSize = 13;
        
        GUILayout.Label("Test Value for Save", headings);

        slider.TestSliderValue = EditorGUILayout.IntSlider(slider.TestSliderValue, 0, 100);

        if (GUILayout.Button("Save"))
        {
            SaveHelper.SaveData();
        }
        if (GUILayout.Button("Load"))
        {
            SaveHelper.LoadData(); 
        }

        GUILayout.EndScrollView();
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