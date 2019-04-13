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

    private void Start()
    {
        
    }
}