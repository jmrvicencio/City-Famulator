using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DatabaseHelper : EditorWindow
{
    [SerializeField] private int testInt;
    [SerializeField] private List<PlantType> plantList = new List<PlantType>();
    [SerializeField] private List<string> plantNames = new List<string>();

    [MenuItem("Window/Old DB Helper")]
    public static void ShowWindow()
    {
        GetWindow<DatabaseHelper>("Old DB Helper");
    }

    protected void OnEnable()
    {
        //Retrieve the data from the EditorPrefs if it exists
        var data = EditorPrefs.GetString("PlantTypeDBConvert", JsonUtility.ToJson(this, false));
        //Then we apply them to this window
        JsonUtility.FromJsonOverwrite(data, this);

        //map plantnames to plantNames List
    }

    protected void OnDisable()
    {
        //We get the JSON data
        var data = JsonUtility.ToJson(this, false);
        //And save it
        EditorPrefs.SetString("PlantTypeDBConvert", data);
    }

    private void OnGUI()
    {
        testInt = EditorGUILayout.IntField("TestInt", testInt);
        GUILayout.Label(plantList.Count.ToString());

        Color tempColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f, 1f);
        GUILayout.Box("+ Add Plant Item Here", GUILayout.Height(50), GUILayout.Width(Screen.width - 10f));
        GUI.backgroundColor = tempColor;

        if (GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
        {
            if (Event.current.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                Event.current.Use();
            }
            else if (Event.current.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();
                foreach (PlantType p in DragAndDrop.objectReferences)
                {
                    AddToList(p);
                }
                Event.current.Use();
            }
        }
    }

    private void AddToList(PlantType plant)
    {
        if (!plantNames.Contains(plant.plantName))
        {
            plantList.Add(plant);
            plantNames.Add(plant.plantName);
        }
        else
        {
            Debug.LogError(string.Format("The Plant Name {0} is already in use", plant.plantName));
        }
    }
}
