using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(PlantModel))]
[CanEditMultipleObjects]
public class PlantModelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlantModel plant = (PlantModel)target;
        
        GUILayout.Space(30);

        Color tempColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f, 1f);
        GUILayout.Box("+ Add Plant Item Here", GUILayout.Height(50), GUILayout.Width(Screen.width-30));
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
                    if (!plant.plantTypeList.Contains(p)) {
                        plant.plantTypeList.Add(p);
                    }
                    else
                    {
                        Debug.LogError("That Plant type is already on the list");
                    }
                    
                }
                Event.current.Use();
            }

        }

        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Space(10);

        int currentIndex = 0;
        foreach (PlantType p in plant.plantTypeList.ToArray())
        {
            if(p == null)
            {
                plant.plantTypeList.RemoveAt(currentIndex);
                return;
            }
            GUILayout.BeginHorizontal(GUILayout.Width(Screen.width - 30));

            GUILayout.Label(p.plantName, new GUIStyle { fixedWidth = 80 });

            plant.plantTypeList[currentIndex] = (PlantType)EditorGUILayout.ObjectField(plant.plantTypeList[currentIndex], typeof(PlantType), false);

            if (GUILayout.Button("-"))
            {
                plant.plantTypeList.RemoveAt(currentIndex);
                currentIndex--;
            }
            if (GUILayout.Button("↑", GUILayout.Width(20)) && currentIndex != 0)
            {
                plant.plantTypeList[currentIndex] = plant.plantTypeList[currentIndex - 1];
                plant.plantTypeList[currentIndex - 1] = p;
            }
            if (GUILayout.Button("↓", GUILayout.Width(20)) && currentIndex + 1 != plant.plantTypeList.Count)
            {
                plant.plantTypeList[currentIndex] = plant.plantTypeList[currentIndex + 1];
                plant.plantTypeList[currentIndex + 1] = p;
            }

            GUILayout.EndHorizontal();

            currentIndex++;
        }
        
        //plant.plantTypeList = plant.plantTypeList.Distinct().ToList();

        GUILayout.Space(30);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(plant);
        }
    }
}
