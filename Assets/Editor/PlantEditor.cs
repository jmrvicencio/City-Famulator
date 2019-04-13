using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlantModel))]
[CanEditMultipleObjects]
public class PlantEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlantModel plantData = (PlantModel)target;

        plantData.plantName = EditorGUILayout.TextField("Plant Name", plantData.plantName);
        EditorGUILayout.LabelField("Growth Stages", plantData.plantStages.Count.ToString());
        GUILayout.Space(10);
        if (GUILayout.Button("Add Growth Stage"))
        {
            plantData.plantStages.Add(new PlantStage());
        }

        GUIStyle headings = new GUIStyle();
        headings.fontSize = 13;
        int listIndex = 0;
        foreach (PlantStage p in plantData.plantStages)
        {
            listIndex++;
            GUILayout.Label("Stage " + listIndex, headings);
            p.plantStageModel = EditorGUILayout.ObjectField("Stage Game Object", p.plantStageModel, typeof(GameObject), false);
            p.daysToGrow = EditorGUILayout.IntField("Days to Grow", p.daysToGrow);
            GUILayout.Space(20);
        }
    }
}
