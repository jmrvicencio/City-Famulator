using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : FarmulatorElement
{
    public void PlantOnTile(ref PlantView tileToPlant, string plantKey)
    {
        PlantType typeToPlant = app.model.plantTypeDB.plantTypeList.Find(p => p.plantName == plantKey);

        if (typeToPlant == null)
        {
            Debug.Log("Plant Species doesn't exist");
            return;
        }

        Vector3 plantPosition = tileToPlant.transform.position;
        var plantItem = Instantiate(typeToPlant.plantStages[3].stageModel, plantPosition, tileToPlant.transform.rotation);
        plantItem.tag = "Plant";
        plantItem.transform.parent = tileToPlant.transform;
    }
}