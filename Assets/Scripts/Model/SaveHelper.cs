using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

namespace SaveUtilsHelper {

    [ExecuteInEditMode]
    public class SaveHelper : FarmulatorElement
    {
        //Declaration of directory path strings & initialization of file extension type
        public static string saveDirectoryPath = "", projectDirectoryPath = "", fileExtension = ".bol";

        //Dictionary of all data to be saved is declared. Data will be stored as objects so that the active data and the
        //data to be saved will be sharing the same instance. A Dictionary for all current files will also be declared to
        //give us a map to loop through.
        private static Dictionary<string,Dictionary<string, object>> dataToSave = new Dictionary<string, Dictionary<string, object>>();
        public static Dictionary<string, List<string>> databasePaths = new Dictionary<string, List<string>>();

        public void Awake()
        {
            //assignment of strings for directory paths
            saveDirectoryPath = Application.persistentDataPath + "/farmulator data/";
            projectDirectoryPath = Application.dataPath + "/Assets/StreamingAssets/";
        }

        /// <summary>
        /// Saves all data stored in the dataToSave dictionary.
        /// </summary>
        public static void SaveData()
        {
            //the loop will go through all items and save each object as their own database using databaseName as
            //the name of the database
            foreach(KeyValuePair<string, Dictionary<string, object>> path in dataToSave)
            {
                CreateSaveDirectory(path.Key);
                foreach(KeyValuePair<string, object> database in path.Value)
                {
                    string jsonString = JsonMapper.ToJson(database.Value);
                    File.WriteAllText(saveDirectoryPath + path.Key + "/" + database.Key + fileExtension, jsonString);
                }
            }
        }

        /// <summary>
        /// Loads data from databases from all currently initialized databases in the dataToSave Dictionary
        /// </summary>
        public static void LoadData()
        {
            //repeating the loop but now we are reading from the databases and putting the read data back into
            //the dataToSave object.
            foreach (KeyValuePair<string, List<string>> path in databasePaths)
            {
                foreach (string databaseName in path.Value)
                {
                    string jsonString = File.ReadAllText(saveDirectoryPath + path.Key + "/" + databaseName + fileExtension);
                    dataToSave[path.Key][databaseName] = JsonMapper.ToObject<object>(jsonString);
                }
            }
        }

        public static object GetSavedData(string databaseName, string databasePath = "")
        {
            string jsonString = File.ReadAllText(saveDirectoryPath + databasePath + "/" + databaseName + fileExtension);
            object savedData = JsonMapper.ToObject<object>(jsonString);
            return savedData;
        }

        /// <summary>
        /// Include Objects for when data is saved.
        /// </summary>
        /// <param name="itemForSave"></param>
        /// <param name="databaseName"></param>
        /// <param name="databasePath"></param>
        public static void IncludeOnSave(object itemForSave, string databaseName, string databasePath="")
        {
            //If the path directory isn't being used, add it.
            if (!dataToSave.ContainsKey(databasePath))
            {
                databasePaths.Add(databasePath, new List<string> {databaseName });
                dataToSave.Add(databasePath, new Dictionary<string, object> { { databaseName, itemForSave } });
            }
            //if the path directory is already in use, add it to the path key.
            else
            {
                databasePaths[databasePath].Add(databaseName);
                dataToSave[databasePath].Add(databaseName, itemForSave);
            }
        }

        /// <summary>
        /// Checks if the save directory exists. Will create the directory
        /// if it's not already made.
        /// </summary>
        public static void CreateSaveDirectory(string path)
        {
            if (!Directory.Exists(saveDirectoryPath + path))
            {
                Directory.CreateDirectory(saveDirectoryPath + path);
            }
        }

        /// <summary>
        /// Checks if the save directory exists in StreamingAssets.
        ///  Will create the directory if it's not already made.
        /// </summary>
        public static void CreateStreamingAssetsDirectory()
        {
            if (!Directory.Exists(projectDirectoryPath))
            {
                Directory.CreateDirectory(projectDirectoryPath);
            }
        }
    }
}