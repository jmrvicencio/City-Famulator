using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;
using System.Text.RegularExpressions;

public class SavePlantType : EditorWindow
{
    string prefsPath;
    string plantsPath;
    string dbPath;
    List<string> plantTypes = new List<string>();

    [MenuItem("Window/Save Plant Types")]
    public static void ShowWindow()
    {
        GetWindow<SavePlantType>("Save Plant Types");
    }

    private void OnEnable()
    {
        //Path for the prefs file
        prefsPath = Application.dataPath + "/Editor/Plant Type Helper/SavePlantType.prefs";

        //Load data from the prefs
        JsonData prefsJson = JsonMapper.ToObject(File.ReadAllText(prefsPath));
        plantsPath = (string) prefsJson["Plants Path"];
        dbPath = (string)prefsJson["Database Path"];
        
        //Clear Plant types list on enable
        plantTypes.Clear();

        //Add all plant files to list
        string metaPattern = @".+\.meta";
        foreach (string file in Directory.GetFiles(Application.dataPath + plantsPath))
        {
            Match matchMeta = Regex.Match(file, metaPattern);
            if (!matchMeta.Success)
            {
                Debug.Log(file);
                plantTypes.Add(file);
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Data will be saved in: StreamingAssets" + dbPath);

        if (GUILayout.Button("Save Plant Types"))
        {
            
        }

        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Space(10);

        
    }
}
