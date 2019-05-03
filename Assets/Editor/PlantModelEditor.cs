using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(PlantTypeModel))]
[CanEditMultipleObjects]
public class PlantModelEditor : Editor
{
    PlantTypeModel t;
    SerializedObject GetTarget;
    SerializedProperty PlantTypeList;
    int listSize;
    bool showWarning;

    private void OnEnable()
    {
        t = (PlantTypeModel)target;
        GetTarget = new SerializedObject(t);
        PlantTypeList = GetTarget.FindProperty("plantTypeList");

        UpdateDictionary();
    }

    public override void OnInspectorGUI()
    {
        GetTarget.Update();

        if(t.plantTypeMap.Count() != t.plantTypeList.Count())
        {
            showWarning = true;
        }
        else
        {
            showWarning = false;
        }

        listSize = PlantTypeList.arraySize;
        //listSize = EditorGUILayout.IntField("List Size", listSize);
        GUILayout.Label("There are " + listSize + " items in the list");

        Color tempColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f, 1f);
        GUILayout.Box("+ Add Plant Item Here", GUILayout.Height(50), GUILayout.Width(Screen.width - 30));
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

        if(listSize != PlantTypeList.arraySize)
        {
            while(listSize > PlantTypeList.arraySize)
            {
                PlantTypeList.InsertArrayElementAtIndex(PlantTypeList.arraySize);
            }
            while(listSize < PlantTypeList.arraySize)
            {
                PlantTypeList.DeleteArrayElementAtIndex(PlantTypeList.arraySize - 1);
            }
        }

        //Update the value of the dictionary

        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Space(10);

        if (showWarning)
        {
            EditorGUILayout.HelpBox("There are 2 PlantTypes with the same PlantName", MessageType.Error);
            GUILayout.Space(10);
        }

        int RemoveItemAt = -1;
        for (int i = 0; i < PlantTypeList.arraySize; i++)
        {
            GUILayout.BeginHorizontal();
            float tempLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 110;

            SerializedProperty PlantType = PlantTypeList.GetArrayElementAtIndex(i);
            PlantType currentPlantType = (PlantType)PlantType.objectReferenceValue;

            PlantType.objectReferenceValue = EditorGUILayout.ObjectField((i + 1) + ": " + currentPlantType.plantName, PlantType.objectReferenceValue, typeof(PlantType), false);

            var duplicateItem = t.plantTypeList.GroupBy(p => p);

            foreach (var grp in duplicateItem)
            {
                if (grp.Count() > 1)
                {
                    int duplicateIndex = t.plantTypeList.IndexOf(grp.Key);
                    if(duplicateIndex == i)
                    {
                        duplicateIndex = t.plantTypeList.IndexOf(grp.Key, i + 1);
                    }

                    RemoveItemAt = duplicateIndex;
                }
            }

            if (PlantType.objectReferenceValue == null) RemoveItemAt = i;

            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                RemoveItemAt = i;
            }
            if (GUILayout.Button("▲", GUILayout.Width(20)) && i != 0)
            {
                SwapStages(i, i - 1);
            }
            if (GUILayout.Button("▼", GUILayout.Width(20)) && i != PlantTypeList.arraySize - 1)
            {
                SwapStages(i, i + 1);
            }

            EditorGUIUtility.labelWidth = tempLabelWidth;
            GUILayout.EndHorizontal();
        }

        if (RemoveItemAt >= 0) RemoveListItem(RemoveItemAt);

        GetTarget.ApplyModifiedProperties();
    }

    private void UpdateDictionary()
    {
        t.plantTypeMap.Clear();

        foreach (PlantType p in t.plantTypeList)
        {
            if (!t.plantTypeMap.ContainsKey(p.plantName))
            {
                t.plantTypeMap.Add(p.plantName, p);
            }
            else
            {
                Debug.LogError("There are more than 1 items with a Plant name of \"" + p.plantName + "\".");
            }
        }
    }

    private void SwapStages(int firstIndex, int secondIndex)
    {
        SerializedProperty firstItem = PlantTypeList.GetArrayElementAtIndex(firstIndex);
        SerializedProperty secondItem = PlantTypeList.GetArrayElementAtIndex(secondIndex);
        PlantType tempItem = (PlantType) firstItem.objectReferenceValue;

        firstItem.objectReferenceValue = secondItem.objectReferenceValue;
        secondItem.objectReferenceValue = tempItem;
    }

    private void RemoveListItem(int index)
    {
        t.plantTypeMap.Remove(t.plantTypeList[index].plantName);
        listSize--;
        while(listSize < PlantTypeList.arraySize)
        {
            PlantTypeList.DeleteArrayElementAtIndex(index);
        }
        t.plantTypeList.RemoveAt(index);

        UpdateDictionary();
    }

    private void AddToList(PlantType p) {
        //if (!t.plantTypeList.Contains(p))
        if (!t.plantTypeMap.ContainsKey(p.plantName))
            {
            listSize++;
            t.plantTypeMap.Add(p.plantName, p);
            while (listSize > PlantTypeList.arraySize)
            {
                PlantTypeList.InsertArrayElementAtIndex(PlantTypeList.arraySize);

                PlantTypeList.GetArrayElementAtIndex(PlantTypeList.arraySize - 1).objectReferenceValue = p;
            }
        }
        else if (!t.plantTypeList.Contains(p))
        {
            Debug.LogError("Plant Name("+ p.plantName +") is already being used by another PlantType");
        }
        else
        {
            Debug.LogError("The Chosen Plant Object is already part of the list");
        }
    }
}
