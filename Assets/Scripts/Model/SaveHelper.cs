using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

namespace SaveUtilsHelper {

    [ExecuteInEditMode]
    public class SaveHelper : FarmulatorElement
    {
        public static string saveDirectoryPath = "", projectDirectoryPath = "", fileExtension = ".bol";
        public static List<DatabaseInfo> databaseLocations = new List<DatabaseInfo>();
        public static Dictionary<string, string[]> databaseLocationsPath;

        private static Dictionary<string,Dictionary<string, object>> dataToSave = new Dictionary<string, Dictionary<string, object>>();

        public void Awake()
        {
            //Strings for the save directory paths
            saveDirectoryPath = Application.persistentDataPath + "/farmulator data/";
            projectDirectoryPath = Application.dataPath + "/Assets/StreamingAssets/";
        }

        public static void SaveData()
        {
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

        public static string LoadData()
        {
            //TODO:Load data is still hardcoded at the moment
            string jsonString = "";
            jsonString = File.ReadAllText(saveDirectoryPath + "testing.bol");
            return jsonString;
        }

        /// <summary>
        /// Checks whether or not the DatabaseInfo is part of the
        /// DatabaseLocations list.
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public static bool ExistsInDatabaseLocations(DatabaseInfo database)
        {
            bool existsInDatabase = false;

            foreach(DatabaseInfo d in databaseLocations)
            {
                if (databaseLocations.Contains(d))
                {
                    existsInDatabase = true;
                }
            }
            return existsInDatabase;    
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
                dataToSave.Add(databasePath, new Dictionary<string, object> { { databaseName, itemForSave } });
            }
            //if the path directory is already in use, add it to the path key.
            else
            {
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

    /// <summary>
    /// An object for storing database information
    /// </summary>
    public class DatabaseInfo
    {
        public string DatabaseName;
        public string DatabasePath;

        public DatabaseInfo(string databaseName = "data", string databasePath = "") 
        {
            DatabaseName = databaseName;
            DatabasePath = databasePath;
        }
    }
}