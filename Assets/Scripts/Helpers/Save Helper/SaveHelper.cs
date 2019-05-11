using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

namespace SaveUtilsHelper {

    [ExecuteInEditMode]
    public class SaveHelper : FarmulatorElement
    {
        //Declaration of directory path strings & initialization of file extension type
        public static string saveDirectoryPath = "", currentSave = "development", projectDirectoryPath = "", fileExtension = ".bol";

        //Initialization of Dictionaries to hold Data. currentData will hold Data to save
        //and loadedData will hold strings loaded from json until a proper Object Type can
        //be found for the dataKey to map the strings using JsonMapper.
        public static Dictionary<string, object> currentData = new Dictionary<string, object>();
        public static Dictionary<string, string> loadedData = new Dictionary<string, string>();
        private static List<string> usedKeys = new List<string>();

        public void OnEnable()
        {
            //assignment of strings for directory paths
            saveDirectoryPath = Application.persistentDataPath + "/player saves/";
            projectDirectoryPath = Application.dataPath + "/Assets/StreamingAssets/";
        }

        /// <summary>
        /// A Public Method that allows other classes to retrieve data from database
        /// and set default data to the database if there isnt any data found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataKey"></param>
        /// <param name="defaultData"></param>
        /// <returns></returns>
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
            
            //Checks if dataKey is the loadedData & currentData dictionaries
            if(loadedData.TryGetValue(dataKey, out string objectString) && currentData.ContainsKey(dataKey))
            {
                currentData[dataKey] = JsonMapper.ToObject<T>(objectString);

                //once the data has been mapped from laodedData to currentData
                //we count the number of properties insode the new currentData[key] vs
                //how many properties are supposed to be present.
                int currentDataProps = 0, actualDataProps = 0;
                foreach (var props in typeof(T).GetProperties())
                {
                    actualDataProps++;
                }
                if (currentData[dataKey] != null)
                {
                    foreach (var props in currentData[dataKey].GetType().GetProperties())
                    {
                        currentDataProps++;
                    }
                }

                //If the no. of properties is the same, return the data
                if(currentDataProps == actualDataProps)
                {
                    return (T)currentData[dataKey];
                }
                //if it is not, return default data and LogError
                else
                {
                    Debug.LogError(String.Format("The data for the key [{0}] was damaged or corrpted. Reverting its data to default data.", dataKey));
                    currentData[dataKey] = defaultData;

                    return defaultData;
                }
            }
            //If the data cannot be fount in the data Dictionaries, the default data will
            //be populated into the currentData Dictionary & will also be returned.
            else
            {
                currentData.Add(dataKey, defaultData);
                return defaultData;
            }
        }

        /// <summary>
        /// Saves the Data fom the currentData dictionary into the
        /// JSON file
        /// </summary>
        /// <param name="saveDirectory"></param>
        public static void SaveData(string saveDirectory = "/player saves/")
        {
            //Triggers the OnSave Event before the data is saved
            EventManager.TriggerEvent(EventStrings.OnSave);
            
            //Assignment of strings for directory paths
            saveDirectoryPath = Application.persistentDataPath + saveDirectory;

            //Converts currentData to a Json string
            string jsonString = JsonMapper.ToJson(currentData);

            try
            {
                CreateDirectory(saveDirectoryPath);
                File.WriteAllText(saveDirectoryPath + "/" + currentSave + fileExtension, jsonString);
            }
            catch(IOException e)
            {
                Debug.LogError("There was a problem trying to save the file. Please try again");
                throw e;
            }
        }

        /// <summary>
        /// Loads the Data from the JSON file and places it into the
        /// dictionaries
        /// </summary>
        /// <param name="saveDirectory"></param>
        public static void LoadData(string saveDirectory = "/player saves/")
        {
            //Clears all keys in the UsedKeys List to allow the data to be called again.
            usedKeys.Clear();
            
            saveDirectoryPath = Application.persistentDataPath + saveDirectory;
            string jsonString;
            JsonData jsonData;

            try
            {
                jsonString = File.ReadAllText(saveDirectoryPath + "/" + currentSave + fileExtension);
                jsonData = JsonMapper.ToObject<JsonData>(jsonString);
                
                //maps data to currentData so that keys can be looped through
                currentData = JsonMapper.ToObject<Dictionary<string, object>>(jsonString);

                //Clear previously loaded data.
                loadedData.Clear();

                //for each key in currentData, we will add the jsonString of the object
                //as the value to loadedData to be parsed later.
                foreach (KeyValuePair<string, object> dataKey in currentData)
                {
                    string objectString = JsonMapper.ToJson(jsonData[dataKey.Key]);
                    loadedData.Add(dataKey.Key, objectString);
                }
            }
            catch(Exception e)
            {
                Debug.LogError("Please try saving to create a new save file");
                throw e;
            }
            finally
            {
                EventManager.TriggerEvent(EventStrings.OnLoad);
            }
        }

        /// <summary>
        /// Creates the save directory for the JSON file
        /// </summary>
        /// <param name="directoryPath"></param>
        private static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
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