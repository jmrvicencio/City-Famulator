using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using EventNameHelper;
using System;

namespace SaveUtilsHelper {

    [ExecuteInEditMode]
    public class SaveHelper : FarmulatorElement
    {
        //Declaration of directory path strings & initialization of file extension type
        public static string saveDirectoryPath = "", currentSave = "development", projectDirectoryPath = "", fileExtension = ".bol";

        public static Dictionary<string, object> currentData = new Dictionary<string, object>();
        public static Dictionary<string, string> loadedData = new Dictionary<string, string>();
        private static List<string> usedKeys = new List<string>();

        public void OnEnable()
        {
            //Comment
            //assignment of strings for directory paths
            saveDirectoryPath = Application.persistentDataPath + "/player saves/";
            projectDirectoryPath = Application.dataPath + "/Assets/StreamingAssets/";
        }

        public static T GetData<T>(string dataKey, T defaultData)
        {
            //Throws an Exception if a DataKey is used more than once;
            if (usedKeys.Contains(dataKey))
            {
                Debug.LogError("A data key is being used more than once. Keys must be unique.");
                throw new SameDataKeyException(dataKey);
            }
            else
            {
                usedKeys.Add(dataKey);
            }
            
            if(loadedData.TryGetValue(dataKey, out string objectString) && currentData.ContainsKey(dataKey))
            {
                Debug.Log("Data was found");
                currentData[dataKey] = JsonMapper.ToObject<T>(objectString);
                return (T)currentData[dataKey];
            }
            else
            {
                Debug.Log("Data was not found");
                currentData.Add(dataKey, defaultData);
                return defaultData;
            }
        }

        public static void SaveData(string saveDirectory = "/player saves/")
        {
            //Triggers the OnSave Event before the data is saved
            EventManager.TriggerEvent(EventStrings.OnSave);
            
            saveDirectoryPath = Application.persistentDataPath + saveDirectory;
            string jsonString = JsonMapper.ToJson(currentData);

            CreateDirectory(saveDirectoryPath);

            File.WriteAllText(saveDirectoryPath + "/" + currentSave + fileExtension, jsonString);
        }

        public static void LoadData(string saveDirectory = "/player saves/")
        {
            //Clears all keys in the UsedKeys List to allow the data to be called again.
            usedKeys.Clear();
            
            saveDirectoryPath = Application.persistentDataPath + saveDirectory;
            string jsonString = File.ReadAllText(saveDirectoryPath + "/" + currentSave + fileExtension);
            JsonData jsonData = JsonMapper.ToObject<JsonData>(jsonString);
            //maps data to currentData so that keys can be looped through
            currentData = JsonMapper.ToObject<Dictionary<string, object>>(jsonString);

            //Clear previously loaded data.
            loadedData.Clear();

            //foraeachdkey in currentData, we will add the jsonString of the object
            //as the value to loadedData to be parsed later.
            foreach (KeyValuePair<string, object> dataKey in currentData)
            {
                string objectString = JsonMapper.ToJson(jsonData[dataKey.Key]);
                loadedData.Add(dataKey.Key, objectString);
            }

            EventManager.TriggerEvent(EventStrings.OnLoad);
        }

        private static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        }
    }

    public class SavePath
    {
        public string dbPath { get; set; }
        public string dbName { get; set; }
        public SavePath(string DBName, string DBPath)
        {
            dbPath = DBName;
            dbName = DBPath;
        }
    }
}

public class SameDataKeyException : Exception
{
    public SameDataKeyException()
    {

    }
    public SameDataKeyException(string dataKey)
       : base(String.Format("More than one usage of the Datakey: {0}", dataKey))
    {

    }
}