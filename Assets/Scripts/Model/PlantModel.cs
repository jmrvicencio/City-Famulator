using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlantModel : FarmulatorElement
{
    public List<PlantType> plantTypeList = new List<PlantType>();
    public Dictionary<string, PlantType> plantTypes = new Dictionary<string, PlantType>();

    private void Start()
    {
        //foreach (PlantType p in Resources.LoadAll<PlantType>("PlantTypes"))
        //{
        //    plantTypes.Add(p.PlantName, p);
        //}
    }
}