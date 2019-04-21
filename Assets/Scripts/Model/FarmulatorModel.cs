using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmulatorModel : FarmulatorElement
{

    //Nested Models
    public PlayerModel player;
    public PlantModel plant;
    public PersistentDataModel persistentData;

    //data
    public float cameraAngle;
    public int day;
}
