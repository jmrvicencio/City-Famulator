using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    List<TestObject> testList = new List<TestObject>();

    private void Start()
    {
        TestObject inArray = new TestObject{ TestData = 1 };
        testList.Add(inArray);
        if(testList.Contains(inArray))
        {
            Debug.Log("TestObject was found");
        }
        else
        {
            Debug.Log("No Test Data found");
        }
    }
}

public class TestObject
{
    public int testData;
    public int TestData { get; set; }
}