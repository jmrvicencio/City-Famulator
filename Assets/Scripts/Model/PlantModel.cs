using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantModel : FarmulatorElement
{
    [SerializeField]
    public List<PlantType> plantTypeList = new List<PlantType>(1);
    public Dictionary<string, PlantType> plantTypeMap = new Dictionary<string, PlantType>(1);

    private void OnEnable()
    {
        foreach(PlantType p in plantTypeList)
        {
            plantTypeMap.Add(p.plantName, p);
        }
        Debug.Log("The dictionary has " + plantTypeMap.Count + " items");
    }
}