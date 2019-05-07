using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmulatorModel : FarmulatorElement
{

    //Nested Models
    public WorldModel world;
    public PlayerModel player;
    public DatabaseModel database;
    public PersistentDataModel persistentData;

    //data
    public float cameraAngle;
    public int day;
    public float gridSize = 0.76f;
}
