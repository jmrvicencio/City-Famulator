using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant Type", order = 51)]
[System.Serializable]
public class PlantType : ScriptableObject
{
    [SerializeField]
    public string plantName;
    //[SerializeField]
    //private PlantType harvestDrop;
    [SerializeField]
    private bool spring, summer, fall, winter;
    [SerializeField]
    [HideInInspector]
    public bool MultipleHarvest { get; set; } = false;
    [SerializeField]
    [HideInInspector]
    public int NumberOfHarvests { get; set; } = 0;
    [SerializeField]
    private int daysBetweenHarvest;
    [SerializeField]
    private List<PlantStage> plantStages;

    public bool Spring { get; set; }
    public bool Summer { get; set; }
    public bool Fall { get; set; }
    public bool Winter { get; set; }
    //public bool MultipleHarvest { get; set; }
    //public PlantType HarvestDrop { get; set; }
    //public int NumberOfHarvests
    //{
    //    get
    //    {
    //        return numberOfHarvests;
    //    }
    //    set
    //    {
    //        if (value >= 0)
    //        {
    //            numberOfHarvests = value;
    //        }
    //        else
    //        {
    //            numberOfHarvests = 0;
    //        }
    //    }
    //}
    public int DaysBetweenHarvest
    {
        get
        {
            return daysBetweenHarvest;
        }
        set
        {
            if (value > 0)
            {
                daysBetweenHarvest = value;
            }
            else
            {
                daysBetweenHarvest = 1;
            }
        }
    }
    public List<PlantStage> PlantStages { get; set; }

    public PlantType()
    {
        Spring = true;
        PlantStages = new List<PlantStage>() { new PlantStage(), new PlantStage()};
    }
}

[System.Serializable]
public class PlantStage
{
    [SerializeField]
    private GameObject stageModel;

    [SerializeField]
    private int daysToGrow;

    public GameObject StageModel { get; set; }
    public int DaysToGrow
    {
        get { return daysToGrow; }
        set
        {
            if(value > 0)
            {
                daysToGrow = value;
            }
            else
            {
                daysToGrow = 1;
            }
        }
    }
}