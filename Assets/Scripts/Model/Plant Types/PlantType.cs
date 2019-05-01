using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant Type", order = 51)]
[System.Serializable]
public class PlantType : ScriptableObject
{
    [SerializeField]
    public string plantName;
    [SerializeField]
    public bool spring, summer = true, fall, winter;
    [SerializeField]
    public bool multipleHarvest = false;
    [SerializeField]
    public int maxHarvests = 2;
    [SerializeField]
    private int daysBetweenHarvest;
    [SerializeField]
    public List<PlantStage> plantStages = new List<PlantStage>(1);

    [System.Serializable]
    public class PlantStage
    {
        [SerializeField]
        private GameObject stageModel;
        [SerializeField]
        private int daysToGrow;
    }
}