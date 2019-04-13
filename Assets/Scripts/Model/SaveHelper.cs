using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

namespace SaveUtilsHelper { 

    public class SaveHelper : FarmulatorElement
    {
        public static string saveDirectoryPath = "", playerDirectoryPath = "";

        public void Awake()
        {
            saveDirectoryPath = Application.persistentDataPath + "/farmulator_saves";
            playerDirectoryPath = saveDirectoryPath + "/player";
        }

        public static void SaveData(float f)
        {
            TestData forSave = new TestData();
            forSave.TestingData = f;
            string saveData = JsonMapper.ToJson(forSave);
            Debug.Log(saveData);
            SaveGame();
            File.WriteAllText(playerDirectoryPath + "/testing.fun", saveData);
        }

        public static bool IsSaveFile()
        {
            return Directory.Exists(Application.persistentDataPath + "/farmulator_saves");
        }

        public static void SaveGame()
        {
            if (!IsSaveFile())
            {
                Debug.Log("Now Creting Save Directory");
                Directory.CreateDirectory(Application.persistentDataPath + "/farmulator_saves");
            }
            else
            {
                Debug.Log("Directory Found");
            }

            if (!Directory.Exists(playerDirectoryPath))
            {
                Directory.CreateDirectory(playerDirectoryPath);
            }
        }
    }

    public class TestData
    {
        public float TestingData { get; set; }
    }

}