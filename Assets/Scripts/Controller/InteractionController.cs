using System;
using UnityEngine;

public class InteractionController : FarmulatorElement
{
    public void PlantOnTile(ref PlantView tileToPlant, string plantKey)
    {
        //Retrieve the plant data of the species to plant based on the plantKey parameter
        PlantType typeToPlant = app.model.database.PlantType.plantTypeList.Find(p => p.plantName == plantKey);

        //If no such plant key is found, return an error
        if (typeToPlant == null)
        {
            Debug.Log("Plant Species doesn't exist");
            return;
        }

        //Instantiate the first tier of that plant on the current tile.
        Vector3 plantPosition = tileToPlant.transform.position + new Vector3(0f,0.05f,0f);
        var plantItem = Instantiate(typeToPlant.plantStages[3].stageModel, plantPosition, tileToPlant.transform.rotation);
        //Give the plant a tag and place is as a child of the current tile
        plantItem.tag = "Plant";
        plantItem.transform.parent = tileToPlant.transform;

        //add data to tile model
        string xPos, zPos;
        Vector3 tilePosition = tileToPlant.transform.position / 0.76f;
        //find the coordinates of the current tile
        xPos = Math.Round(tilePosition.x).ToString();
        zPos = Math.Round(tilePosition.z).ToString();
    }

    public void placeItem(GameObject placedItem)
    {
        if (app.model.player.buildPossible)
        {
            GameObject item = Instantiate(placedItem, app.view.player.getOutlinePosition());
            item.transform.parent = GameObject.Find("Objects").transform;

            Debug.Log(FCUtils.RoundToCoord(item.transform.position).x + "," + FCUtils.RoundToCoord(item.transform.position).z);

            app.model.tileData.Add(
                new Vector3Int(FCUtils.RoundToCoord(item.transform.position)),
                new FarmulatorModel.PlanterData(
                    Resources.Load<TileItem>("SO/TileItem/P_1b1A"),
                    app.model.database.PlantType.plantTypeMap["TestBerry"],
                    app.model.day
            ));
        }
        else
        {
            Debug.LogError("Cannot Build Here");
        }
    }
}