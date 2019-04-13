using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantStage2
{
    public string name;
    public int amount = 1;
}

public class PlantModel2 : FarmulatorElement
{
    public int experience = 1;
    public List<PlantStage> plantStages = new List<PlantStage>(new PlantStage[] {new PlantStage()});

    public int level
    {
        get { return experience / 750; }
    }
}