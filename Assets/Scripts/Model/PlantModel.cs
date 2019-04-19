using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class PlantStage
//{
//    public Object plantStageModel;
//    public int daysToGrow = 1;
//}

public class PlantModel : FarmulatorElement
{
    public string plantName;
    public bool summer = true, spring = true, winter = true, fall = true;
    public List<PlantStage> plantStages = new List<PlantStage>(new PlantStage[] { new PlantStage() });
}