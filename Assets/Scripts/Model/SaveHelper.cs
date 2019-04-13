using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

namespace SaveUtilsHelper { 

    public class SaveHelper : FarmulatorElement
    {
        public static void SaveData(float f)
        {
            TestData forSave = new TestData();
            forSave.TestingData = f;
            string json_string = JsonMapper.ToJson(forSave);
            Debug.Log(json_string);
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
        }
    }

    public class TestData
    {
        public float TestingData { get; set; }
    }

}