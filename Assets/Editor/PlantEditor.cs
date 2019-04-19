using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlantType))]
[CanEditMultipleObjects]
public class PlantTypeEditor : Editor
{
    [SerializeField]
    public bool ShowSeasons = true;

    public override void OnInspectorGUI()
    {
        PlantType plantData = (PlantType)target;

        plantData.PlantName = EditorGUILayout.TextField("Plant Name",plantData.PlantName);

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

        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Space(10);

        plantData.MultipleHarvest = EditorGUILayout.ToggleLeft("Multiple Harvests", plantData.MultipleHarvest);
        if (plantData.MultipleHarvest)
        {
            GUILayout.Space(5);
            float originalLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 200f;
            plantData.NumberOfHarvests = EditorGUILayout.IntField("Number of harvests", plantData.NumberOfHarvests);
            plantData.DaysBetweenHarvest = EditorGUILayout.IntField("Days Between Harvest", plantData.DaysBetweenHarvest);
            EditorGUIUtility.labelWidth = originalLabelWidth;
        }

        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Space(10);

        if (GUILayout.Button("Add Plant Stage"))
        {
            plantData.PlantStages.Add(new PlantStage());
        }

        GUILayout.Space(10);

        int listIndex = 0;
        foreach(PlantStage p in plantData.PlantStages.ToArray())
        {
            string suffix = "";
            if(listIndex == 0) { suffix = ": Seed Stage"; }
            GUILayout.Label("Stage" + (listIndex + 1) + suffix, new GUIStyle() { fontSize = 13 });

            GUILayout.Space(5);

            p.StageModel = (GameObject)EditorGUILayout.ObjectField("Stage Game Object", p.StageModel, typeof(GameObject), false);
            p.DaysToGrow = EditorGUILayout.IntField("Days to Grow", p.DaysToGrow);
            
            GUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Remove Stage", GUILayout.MinWidth(100), GUILayout.MaxWidth(200)))
            {
                if (plantData.PlantStages.Count > 1)
                {
                    plantData.PlantStages.RemoveAt(listIndex);
                }
            }
            if (plantData.PlantStages.Count > 1) {
                if (GUILayout.Button("↑", GUILayout.Width(40)) && listIndex != 0)
                {
                    plantData.PlantStages[listIndex] = plantData.PlantStages[listIndex - 1];
                    plantData.PlantStages[listIndex - 1] = p;
                }
                if (GUILayout.Button("↓", GUILayout.Width(40)) && listIndex + 1 != plantData.PlantStages.Count)
                {
                    plantData.PlantStages[listIndex] = plantData.PlantStages[listIndex + 1];
                    plantData.PlantStages[listIndex + 1] = p;
                }
            }
            
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            listIndex++;
        }
    }
}

//[CustomEditor(typeof(PlantModel))]
//[CanEditMultipleObjects]
//public class PlantEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        PlantModel plantData = (PlantModel)target;

//        plantData.plantName = EditorGUILayout.TextField("Plant Name", plantData.plantName);
//        EditorGUILayout.LabelField("Growth Stages", plantData.plantStages.Count.ToString());
//        GUILayout.Space(10);
//        if (GUILayout.Button("Add Growth Stage"))
//        {
//            plantData.plantStages.Add(new PlantStage());
//        }

//        GUIStyle headings = new GUIStyle();
//        headings.fontSize = 13;
//        int listIndex = 0;
//        foreach (PlantStage p in plantData.plantStages)
//        {
//            listIndex++;
//            GUILayout.Label("Stage " + listIndex, headings);
//            p.plantStageModel = EditorGUILayout.ObjectField("Stage Game Object", p.plantStageModel, typeof(GameObject), false);
//            p.daysToGrow = EditorGUILayout.IntField("Days to Grow", p.daysToGrow);
//            GUILayout.Space(20);
//        }
//    }
//}
