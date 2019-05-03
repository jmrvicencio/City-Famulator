using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(PlantType))]
[CanEditMultipleObjects]
public class PlantTypeEditor : Editor
{
    //[SerializeField]
    //public bool ShowSeasons = true;

    PlantType t;
    SerializedObject GetTarget;
    SerializedProperty PlantStages;
    SerializedProperty PlantName;
    SerializedProperty Summer;
    SerializedProperty Spring;
    SerializedProperty Fall;
    SerializedProperty Winter;
    SerializedProperty MultipleHarvest;
    SerializedProperty MaxHarvests;
    SerializedProperty DaysBetweenHarvest;
    int StagesSize;
    
    [SerializeField]
    public bool showSeasons = true;

    private void OnEnable()
    {
        t = (PlantType)target;
        GetTarget = new SerializedObject(t);

        //Initialize the data
        PlantName = GetTarget.FindProperty("plantName");
        Summer = GetTarget.FindProperty("summer");
        Spring = GetTarget.FindProperty("spring");
        Fall = GetTarget.FindProperty("fall");
        Winter = GetTarget.FindProperty("winter");
        MultipleHarvest = GetTarget.FindProperty("multipleHarvest");
        MaxHarvests = GetTarget.FindProperty("maxHarvests");
        DaysBetweenHarvest = GetTarget.FindProperty("daysBetweenHarvest");
        PlantStages = GetTarget.FindProperty("plantStages");
    }

    public override void OnInspectorGUI()
    {
        //Update all the data
        GetTarget.Update();

        //General Data of the plant
        PlantName.stringValue = EditorGUILayout.TextField("Plant Name", PlantName.stringValue);

        GUILayout.Space(10);

        //Define the seasons the plant can survive
        showSeasons = EditorGUILayout.Foldout(showSeasons, "Seasons Viable");
        if (showSeasons)
        {
            GUILayout.BeginHorizontal();
            Summer.boolValue = EditorGUILayout.ToggleLeft("Summer", Summer.boolValue);
            Spring.boolValue = EditorGUILayout.ToggleLeft("Spring", Spring.boolValue);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Fall.boolValue = EditorGUILayout.ToggleLeft("Fall", Fall.boolValue);
            Winter.boolValue = EditorGUILayout.ToggleLeft("Winter", Winter.boolValue);
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Space(10);

        //Allow the plant to have multiple Harvest
        MultipleHarvest.boolValue = EditorGUILayout.ToggleLeft("Can Have Multiple Harvests", MultipleHarvest.boolValue);
        if (MultipleHarvest.boolValue)
        {
            EditorGUI.indentLevel++;
            float tempLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 180;

            GUILayout.Space(10);
            MaxHarvests.intValue = EditorGUILayout.IntField("Max Harvests", MaxHarvests.intValue);
            if(MaxHarvests.intValue < 2) { MaxHarvests.intValue = 2; }
            DaysBetweenHarvest.intValue = EditorGUILayout.IntField("Days between harvest", DaysBetweenHarvest.intValue);
            if (DaysBetweenHarvest.intValue < 1) { DaysBetweenHarvest.intValue = 1; }

            EditorGUIUtility.labelWidth = tempLabelWidth;
            EditorGUI.indentLevel--;
        }

        GUILayout.Space(10);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        GUILayout.Space(10);

        //Add a stage to the plant
        StagesSize = PlantStages.arraySize;
        if (StagesSize < 1) StagesSize = 1;
        //StagesSize = EditorGUILayout.IntField("Number of Stages", StagesSize);
        
        if (GUILayout.Button("Add Plant Stage"))
        {
            StagesSize++;
        }

        //Resizes the list if it's size is reduced.
        if (StagesSize != PlantStages.arraySize)
        {
            while (StagesSize > PlantStages.arraySize)
            {
                PlantStages.InsertArrayElementAtIndex(PlantStages.arraySize);

                //Make the newly added property have default data
                SerializedProperty Stage = PlantStages.GetArrayElementAtIndex(PlantStages.arraySize-1);
                SerializedProperty StageModel = Stage.FindPropertyRelative("stageModel");
                SerializedProperty DaysToGrow = Stage.FindPropertyRelative("daysToGrow");
                StageModel.objectReferenceValue = null;
                DaysToGrow.intValue = 1;
            }
            while (StagesSize < PlantStages.arraySize)
            {
                PlantStages.DeleteArrayElementAtIndex(PlantStages.arraySize - 1);
            }
        }

        int removeStageAt = -1;
        for (int i = 0; i < PlantStages.arraySize; i++)
        {
            SerializedProperty Stage = PlantStages.GetArrayElementAtIndex(i);
            SerializedProperty StageModel = Stage.FindPropertyRelative("stageModel");
            SerializedProperty DaysToGrow = Stage.FindPropertyRelative("daysToGrow");

            GUILayout.Space(20);
            
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(170));

            float tempLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 90;

            DaysToGrow.intValue = EditorGUILayout.IntField("Days To Grow",DaysToGrow.intValue);
            if(DaysToGrow.intValue < 1) { DaysToGrow.intValue = 1; }
            StageModel.objectReferenceValue = EditorGUILayout.ObjectField("3d Model", StageModel.objectReferenceValue, typeof(GameObject), false);
            
            //Adds the buttons for the plant stages
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove Stage " + (i + 1), GUILayout.Width(120)))
            {
                removeStageAt = i;
            }
            if (GUILayout.Button("▲", GUILayout.Width(30)) && i != 0)
            {
                SwapStages(i, i - 1);
            }
            if (GUILayout.Button("▼", GUILayout.Width(30)) && i != PlantStages.arraySize - 1)
            {
                SwapStages(i, i + 1);
            }
            GUILayout.EndHorizontal();

            EditorGUIUtility.labelWidth = tempLabelWidth;

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();

            //Display a label preview of the GameObject for the stage
            Texture stagePreview = AssetPreview.GetAssetPreview(StageModel.objectReferenceValue);
            GUILayout.Label(stagePreview, GUILayout.Width(55), GUILayout.Height(55));

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        //Call Actions after the loop
        if (removeStageAt >= 0) RemoveStage(removeStageAt);

        //Apply modified properties
        GetTarget.ApplyModifiedProperties();
    }

    private void RemoveStage(int index)
    {
        int currentArraySize = PlantStages.arraySize;
        while (PlantStages.arraySize == currentArraySize)
        {
            PlantStages.DeleteArrayElementAtIndex(index);
        }
    }

    private void SwapStages(int firstIndex, int secondIndex)
    {
        //Serialize Property Changes
        SerializedProperty FirstIndex = PlantStages.GetArrayElementAtIndex(firstIndex);
        SerializedProperty FirstStageModel = FirstIndex.FindPropertyRelative("stageModel");
        SerializedProperty FirstDaysToGrow = FirstIndex.FindPropertyRelative("daysToGrow");
        SerializedProperty SecondIndex = PlantStages.GetArrayElementAtIndex(secondIndex);
        SerializedProperty SecondStageModel = SecondIndex.FindPropertyRelative("stageModel");
        SerializedProperty SecondDaysToGrow = SecondIndex.FindPropertyRelative("daysToGrow");

        //Temporarily store the values of the first item
        GameObject tempStageModel = (GameObject) FirstStageModel.objectReferenceValue;
        int tempDaysToGrow = FirstDaysToGrow.intValue;

        //Swap the values of the first and second index items;
        FirstStageModel.objectReferenceValue = SecondStageModel.objectReferenceValue;
        FirstDaysToGrow.intValue = SecondDaysToGrow.intValue;
        SecondStageModel.objectReferenceValue = tempStageModel;
        SecondDaysToGrow.intValue = tempDaysToGrow;
    }
}

//[CustomEditor(typeof(PlantType))]
//[CanEditMultipleObjects]
//public class PlantTypeEditor : Editor
//{
//    [SerializeField]
//    public bool ShowSeasons = true;

//    SerializedProperty plantName;

//    private void OnEnable()
//    {
//        plantName = serializedObject.FindProperty("plantName");
//        SerializedProperty plantStage = serializedObject.FindProperty("plantStages");
//    }

//    public override void OnInspectorGUI()
//    {
//        PlantType plantData = (PlantType)target;

//        serializedObject.Update();

//        plantName.stringValue = EditorGUILayout.TextField("Plant Name", plantData.plantName);

//        //Add this line once HarvestDrops are integrated with Jovi's inventory system
//        //plantData.HarvestDrop = (PlantType)EditorGUILayout.ObjectField("Harvest Drop",plantData.HarvestDrop, typeof(PlantType), false);

//        ShowSeasons = EditorGUILayout.Foldout(ShowSeasons, "Seasons Available");
//        if (ShowSeasons)
//        {
//            EditorGUILayout.BeginHorizontal();

//            plantData.Spring = EditorGUILayout.ToggleLeft("Spring", plantData.Spring);
//            plantData.Summer = EditorGUILayout.ToggleLeft("Summer", plantData.Summer);

//            EditorGUILayout.EndHorizontal();

//            EditorGUILayout.BeginHorizontal();

//            plantData.Fall = EditorGUILayout.ToggleLeft("Fall", plantData.Fall);
//            plantData.Winter = EditorGUILayout.ToggleLeft("Winter", plantData.Winter);

//            EditorGUILayout.EndHorizontal();
//        }

//        GUILayout.Space(15);

//        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

//        GUILayout.Space(15);

//        plantData.MultipleHarvest = EditorGUILayout.ToggleLeft("Multiple Harest", plantData.MultipleHarvest);
//        if (plantData.MultipleHarvest)
//        {
//            float tempLabelWidth = EditorGUIUtility.labelWidth;
//            EditorGUIUtility.labelWidth = 150;

//            plantData.NumberOfHarvests = EditorGUILayout.IntField("Number of Harvests", plantData.NumberOfHarvests);
//            plantData.DaysBetweenHarvest = EditorGUILayout.IntField("Days Between Harvest", plantData.DaysBetweenHarvest);

//            EditorGUIUtility.labelWidth = tempLabelWidth;
//        }

//        GUILayout.Space(15);

//        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

//        GUILayout.Space(15);

//        if (GUILayout.Button("Add New Stage"))
//        {
//            plantData.PlantStages.Add(new PlantStage());
//        }

//        GUILayout.Space(10);

//        int plantStageIndex = 0;
//        foreach (PlantStage p in plantData.PlantStages.ToArray())
//        {
//            string extraLabelString = "";
//            if (plantStageIndex == 0) { extraLabelString = ": Seed Stage"; }
//            else if (plantStageIndex + 1 == plantData.PlantStages.Count) { extraLabelString = ": Harvest Stage"; }
//            GUILayout.Label("Stage" + (plantStageIndex + 1 + extraLabelString), new GUIStyle { fontSize = 15 });

//            GUILayout.Space(5);

//            GUILayout.BeginHorizontal();
//            GUILayout.BeginVertical(new GUIStyle { fixedWidth = 90 });

//            GUILayout.Label("Days to Grow");
//            GUILayout.Label("Stage Model");

//            GUILayout.EndVertical();
//            GUILayout.BeginVertical(new GUIStyle { fixedWidth = 80 });

//            p.DaysToGrow = EditorGUILayout.IntField(p.DaysToGrow);
//            p.StageModel = (GameObject)EditorGUILayout.ObjectField(p.StageModel, typeof(GameObject), false);

//            GUILayout.EndVertical();

//            GUILayout.FlexibleSpace();

//            Texture stageModelPreview = AssetPreview.GetAssetPreview(p.StageModel);
//            GUILayout.Label(stageModelPreview, GUILayout.Height(50), GUILayout.Width(50));

//            GUILayout.EndHorizontal();

//            GUILayout.BeginHorizontal();

//            if (GUILayout.Button("Remove Stage", GUILayout.Width(150)) && plantData.PlantStages.Count > 2)
//            {
//                plantData.PlantStages.RemoveAt(plantStageIndex);
//            }
//            if (GUILayout.Button("↑", GUILayout.Width(20)) && plantStageIndex != 0)
//            {
//                plantData.PlantStages[plantStageIndex] = plantData.PlantStages[plantStageIndex - 1];
//                plantData.PlantStages[plantStageIndex - 1] = p;
//            }
//            if (GUILayout.Button("↓", GUILayout.Width(20)) && plantStageIndex + 1 != plantData.PlantStages.Count)
//            {
//                plantData.PlantStages[plantStageIndex] = plantData.PlantStages[plantStageIndex + 1];
//                plantData.PlantStages[plantStageIndex + 1] = p;
//            }

//            GUILayout.EndHorizontal();

//            GUILayout.Space(20);


//            plantStageIndex++;
//        }

//        serializedObject.ApplyModifiedProperties();
//        Undo.RecordObject(target, "Record Scriptable Object");
//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(plantData);
//        }
//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();
//    }
//}
