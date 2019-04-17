using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveUtilsHelper;
using DBStrings;
using LitJson;
using System.IO;

public class FarmulatorElement : MonoBehaviour
{
   //Gives access of the app to all instances running it
   public FarmulatorApplication app
    {
        get { return GameObject.FindObjectOfType<FarmulatorApplication>(); }
    }
}

public class FarmulatorApplication : MonoBehaviour
{
    //Reference to the root instances.
    public FarmulatorModel model;
    public FarmulatorController controller;
    public FarmulatorView view;

    private void Start()
    {
        string jsonString = JsonMapper.ToJson(new SecondObject {TestData = "newString" });
        SecondObject jsonData = JsonMapper.ToObject<SecondObject>(jsonString);
        SaveHelper.LoadData();
        //Debug.Log(jsonData.TestData);
    }
}

public class SecondObject
{
    public string TestData { get; set; }
}