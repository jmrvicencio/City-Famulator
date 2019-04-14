using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

namespace SaveUtilsHelper { 

    public class SaveHelper : FarmulatorElement
    {
        public static string saveDirectoryPath = "", playerDirectoryPath = "", projectDirectoryPath = "";
        static List<DatabaseInfo> databaseLocations = new List<DatabaseInfo>();

        public void Awake()
        {
            saveDirectoryPath = Application.persistentDataPath + "/farmulator data";
            playerDirectoryPath = saveDirectoryPath + "/player";
            projectDirectoryPath = Application.dataPath + "/Assets/StreamingAssets";
        }

        public static void SaveData(float f)
        {
            TestData forSave = new TestData();
            forSave.TestingData = f;
            string saveData = JsonMapper.ToJson(forSave);
            CreateSaveDirectory();
            File.WriteAllText(playerDirectoryPath + "/testing.fun", saveData);
        }

        public static void InitiateSave()
        {

        }

        public void SaveDatabaseLocations()
        {
            string directoryJSON = JsonMapper.ToJson(databaseLocations);
            File.WriteAllText(projectDirectoryPath, directoryJSON);
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

        public static void IncludeOnSave(List<object> itemsForSave, string databaseName, string databasePath)
        {
            DatabaseInfo database = new DatabaseInfo { DatabaseName = databaseName, DatabasePath = databasePath };
            ExistsInDatabaseLocations(database);
        }

        /// <summary>
        /// Checks if the save directory exists. Will create the directory
        /// if it's not already made.
        /// </summary>
        public static void CreateSaveDirectory()
        {
            if (!Directory.Exists(saveDirectoryPath))
            {
                Directory.CreateDirectory(saveDirectoryPath);
            }
            if (!Directory.Exists(playerDirectoryPath))
            {
                Directory.CreateDirectory(saveDirectoryPath);
            }
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

    public class TestData
    {
        public float TestingData { get; set; }
    }

}