using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmulatorModel : FarmulatorElement
{

    //Nested Models
    public WorldModel world;
    public PlayerModel player;
    public PlantTypeModel plantTypeDB;
    public PersistentDataModel persistentData;

    //data
    public float cameraAngle;
    public int day;
}
