using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(PlantType))]
[CanEditMultipleObjects]
public class PlantTypeEditor : Editor
{
    [SerializeField]
    public bool ShowSeasons = true;

    SerializedProperty plantName;

    private void OnEnable()
    {
        plantName = serializedObject.FindProperty("plantName");
        SerializedProperty plantStage = serializedObject.FindProperty("plantStages");
    }

    public override void OnInspectorGUI()
    {
        PlantType plantData = (PlantType)target;

        serializedObject.Update();

        plantName.stringValue = EditorGUILayout.TextField("Plant Name", plantData.plantName);

        //Add this line once HarvestDrops are integrated with Jovi's inventory system
        //plantData.HarvestDrop = (PlantType)EditorGUILayout.ObjectField("Harvest Drop",plantData.HarvestDrop, typeof(PlantType), false);

        ShowSeasons = EditorGUILayout.Foldout(ShowSeasons, "Seasons Available");
        if (ShowSeasons)
        {
            EditorGUILayout.BeginHorizontal();

            plantData.Spring = EditorGUILayout.ToggleLeft("Spring", plantData.Spring);
            plantData.Summer = EditorGUILayout.ToggleLeft("Summer", plantData.Summer);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            plantData.Fall = EditorGUILayout.ToggleLeft("Fall", plantData.Fall);
            plantData.Winter = EditorGUILayout.ToggleLeft("Winter", plantData.Winter);

            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(15);

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        GUILayout.Space(15);

        plantData.MultipleHarvest = EditorGUILayout.ToggleLeft("Multiple Harest", plantData.MultipleHarvest);
        if (plantData.MultipleHarvest)
        {
            float tempLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 150;

            plantData.NumberOfHarvests = EditorGUILayout.IntField("Number of Harvests", plantData.NumberOfHarvests);
            plantData.DaysBetweenHarvest = EditorGUILayout.IntField("Days Between Harvest", plantData.DaysBetweenHarvest);

            EditorGUIUtility.labelWidth = tempLabelWidth;
        }

        GUILayout.Space(15);

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        GUILayout.Space(15);

        if (GUILayout.Button("Add New Stage"))
        {
            plantData.PlantStages.Add(new PlantStage());
        }

        GUILayout.Space(10);

        int plantStageIndex = 0;
        foreach (PlantStage p in plantData.PlantStages.ToArray())
        {
            string extraLabelString = "";
            if(plantStageIndex == 0) { extraLabelString = ": Seed Stage"; }
            else if(plantStageIndex + 1 == plantData.PlantStages.Count){ extraLabelString = ": Harvest Stage"; }
            GUILayout.Label("Stage" + (plantStageIndex + 1 + extraLabelString), new GUIStyle {fontSize = 15 });

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(new GUIStyle {fixedWidth = 90 });

            GUILayout.Label("Days to Grow");
            GUILayout.Label("Stage Model");

            GUILayout.EndVertical();
            GUILayout.BeginVertical(new GUIStyle { fixedWidth = 80 });

            p.DaysToGrow = EditorGUILayout.IntField(p.DaysToGrow);
            p.StageModel = (GameObject)EditorGUILayout.ObjectField(p.StageModel, typeof(GameObject), false);

            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();

            Texture stageModelPreview = AssetPreview.GetAssetPreview(p.StageModel);
            GUILayout.Label(stageModelPreview, GUILayout.Height(50), GUILayout.Width(50));

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Remove Stage", GUILayout.Width(150)) && plantData.PlantStages.Count > 2)
            {
                plantData.PlantStages.RemoveAt(plantStageIndex);
            }
            if (GUILayout.Button("↑", GUILayout.Width(20)) && plantStageIndex != 0)
            {
                plantData.PlantStages[plantStageIndex] = plantData.PlantStages[plantStageIndex - 1];
                plantData.PlantStages[plantStageIndex - 1] = p;
            }
            if (GUILayout.Button("↓", GUILayout.Width(20)) && plantStageIndex + 1 != plantData.PlantStages.Count)
            {
                plantData.PlantStages[plantStageIndex] = plantData.PlantStages[plantStageIndex + 1];
                plantData.PlantStages[plantStageIndex + 1] = p;
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(20);
            

            plantStageIndex++;
        }

        serializedObject.ApplyModifiedProperties();
        Undo.RecordObject(target, "Record Scriptable Object");
        if (GUI.changed) {
            EditorUtility.SetDirty(plantData);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
