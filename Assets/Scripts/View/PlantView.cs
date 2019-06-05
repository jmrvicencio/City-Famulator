using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantView : FarmulatorElement, IPlayerInteractable
{
    public void PlayerAction(Item item)
    {
        PlantView thisPlant = this.GetComponent<PlantView>();
        app.controller.interaction.PlantOnTile(ref thisPlant, item.name);
    }
}
