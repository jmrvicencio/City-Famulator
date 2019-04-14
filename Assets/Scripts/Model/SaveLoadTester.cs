using UnityEngine;
using UnityEditor;
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

        if (GUILayout.Button("Save"))
        {
            SaveHelper.SaveData(testValue);
        }
        if (GUILayout.Button("Load"))
        {

        }
    }
}