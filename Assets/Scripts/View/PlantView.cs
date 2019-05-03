using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantView : FarmulatorElement, IPlayerInteractable
{
    public void PlayerAction(Item item)
    {
        PlantView thisPlant = this.GetComponent<PlantView>();
        app.controller.plant.PlantOnTile(ref thisPlant, "TestBerry");

        //PlantType plantType = app.model.plantTypeDB.plantTypeList.Find(p => p.plantName == "TestBerrys");
        //if (plantType == null)
        //{
        //    Debug.Log("No Plant Type found");
        //    return;
        //}
    }
}
