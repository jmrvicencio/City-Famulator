using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;
using System.Text.RegularExpressions;

public class SavePlantType : EditorWindow
{
    private string prefsPath;
    private string plantsPath, invPath, dbPath;
    int currentTab = 0;
    string[] tabStrings = { "Plant Types", "WIP" };

    private enum MissingDataType {PlantData, InvItemData};

    Dictionary<string, List<MissingDataType>> missingData = new Dictionary<string, List<MissingDataType>>();
    List<DBPlantType> plantData = new List<DBPlantType>();

    [MenuItem("Window/Database Helper")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<SavePlantType>("Database Helper");
        window.minSize = new Vector2(500f, 500f);
    }

    private void OnEnable()
    {
        //Path for the prefs file
        prefsPath = Application.dataPath + "/Editor/Database Helper/DBHelper.prefs";

        //Load data from the prefs
        JsonData prefsJson = JsonMapper.ToObject(File.ReadAllText(prefsPath));
        plantsPath = (string)prefsJson["Plants Path"];
        invPath = (string)prefsJson["InventoryItem Path"];
        dbPath = (string)prefsJson["Database Path"];
    }

    private void OnGUI()
    {
        currentTab = GUILayout.Toolbar(currentTab, tabStrings);

        GUILayout.Space(20);

        DrawTabGUI();
    }

    private void DrawTabGUI()
    {
        switch (currentTab)
        {
            //Plant Types Tab
            case 0:
                
                //Clear the plantData dict for refreshing the data.
                plantData.Clear();
                missingData.Clear();
                string metaPattern = @".+\.meta";

                //Plant ScriptableObjects are taken and paired to InvItem ScriptableObjects and
                //added to the plantData dict.
                foreach (string file in Directory.GetFiles(Application.dataPath + "/" + plantsPath + "/"))
                {
                    //Use Regex.Match to exclude meta files
                    Match matchMeta = Regex.Match(file, metaPattern);
                    if (!matchMeta.Success)
                    {
                        //Take only the FileObject name from the file path.
                        string plantId = Regex.Replace(file, @".+/(.+)\..+", @"$1");

                        //Load All plant types fron the plantSO directory
                        PlantType plantType = AssetDatabase.LoadAssetAtPath<PlantType>($"Assets/{plantsPath}/{plantId}.asset");
                        if (plantType != null)
                        {
                            List<MissingDataType> missingDataTypes = new List<MissingDataType>();

                            //Convert the InstanceID to g.o. name and save the name onto the json.
                            string plantJson = JsonUtility.ToJson(plantType);
                            InstanceIdToObjName(ref plantJson);

                            //Create a DBPlantType item to add to the list.
                            DBPlantType dbPlantType = new DBPlantType();
                            dbPlantType.PlantId = plantId;
                            dbPlantType.PlantType = ConstEnums.PlantType.SingleHarvest;
                            dbPlantType.PlantData = JsonMapper.ToObject(plantJson);

                            //Search if the matching item is present in the item directory
                            Item invItem = AssetDatabase.LoadAssetAtPath<Item>($"Assets/{invPath}/{plantId}.asset");
                            //if the matching item isn't found, add it to the list of missing data
                            if (invItem == null)
                            {
                                missingDataTypes.Add(MissingDataType.InvItemData);
                                missingData.Add(plantId, missingDataTypes);
                            }

                            plantData.Add(dbPlantType);
                        }
                    }
                }
                
                //Begin GUI Rendering for the window
                GUILayout.Label("Data will be saved in: StreamingAssets" + dbPath);

                if (GUILayout.Button("Save Plant Types"))
                {
                    var ds = new DataService("FarmulatorData", true);
                    ds.CreateTable<PlantSQL>();

                    foreach (DBPlantType plant in plantData)
                    {
                        Debug.Log(ds.Update<PlantSQL>(new PlantSQL { PlantId = plant.PlantId, PlantType = plant.PlantType, PlantData=JsonMapper.ToJson(plant.PlantData) }));
                    }

                    //Debug.Log(ds.Update<DBItems>(new DBItems { DBName = "PlantType", JsonData = JsonMapper.ToJson(plantData) }));
                }

                GUILayout.Space(10);
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                GUILayout.Space(10);

                //Show a warning if there is any missing data
                if (missingData.Count > 0)
                {
                    string missingDataText = "";
                    foreach(KeyValuePair<string, List<MissingDataType>> entry in missingData)
                    {
                        missingDataText += $"{entry.Key}: ";
                        bool firstItem = true;
                        foreach(MissingDataType type in entry.Value)
                        {
                            if (firstItem == false) missingDataText += ", ";
                            missingDataText += type;
                            firstItem = false;
                        }
                        missingDataText += "\n";
                    }

                    EditorGUILayout.HelpBox($"\nThe following plants have incomplete data:\n\n{missingDataText}", MessageType.Warning);
                }

                GUILayout.Space(10);

                if (plantData.Count == 0) GUILayout.Label("There were no plant items found");
                else GUILayout.Label(" The following Plant Data will be exported: \n", new GUIStyle{fontSize = 15});

                foreach(DBPlantType entry in plantData)
                {
                    GUILayout.Label(entry.PlantId);
                }

                break;

            //Tree Tab
            case 1:
                break;
        }
    }

    /// <summary>
    /// Replace the {"instanceId":\d+} with the name of the gameobject.
    /// ideally will be used for calling items from assetbundles
    /// </summary>
    /// <param name="jsonString"></param>
    private void InstanceIdToObjName(ref string jsonString)
    {
        //find the instanceId inside the jsonString
        string instanceIdPattern = @"{""instanceID"":(.+?)}";
        MatchCollection instanceIdMatches = Regex.Matches(jsonString, instanceIdPattern);
        
        //Loop through all instanceId matches
        foreach (Match match in instanceIdMatches)
        {
            //get the object through the instanceId
            int instanceId = int.Parse(Regex.Replace(match.ToString(), @".+?(\d+).+", @"$1"));
            Object gameObjectModel = EditorUtility.InstanceIDToObject(instanceId);

            //replace instanceId with name of the object
            if (gameObjectModel != null)
            {
                jsonString = Regex.Replace(jsonString, match.ToString(), $"\"{gameObjectModel.name}\"");
            }
            else
            {
                jsonString = Regex.Replace(jsonString, match.ToString(), "null");
            }
        }
    }

    private class DBPlantType
    {
        public string PlantId;
        public ConstEnums.PlantType PlantType;
        public JsonData PlantData;
    }
}