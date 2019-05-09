using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmulatorModel : FarmulatorElement
{
    //Nested Models
    public PlayerModel player;
    public DatabaseModel database;
    public PersistentDataModel persistentData;

    //data
    public float cameraAngle;
    public int day = 1;
    public float gridSize = 0.76f;

    public Dictionary<Vector3Int, TileData> tileData = new Dictionary<Vector3Int, TileData>();

    #region Custom Classes
    public class TileData
    {
        public TileItem TileItem { get; set; }

        public TileData(TileItem item)
        {
            TileItem = item;
        }
    }

    public class PlanterData : TileData
    {
        public PlantType PlantedType { get; set; }
        public int DayPlanted { get; set; }

        public PlanterData(TileItem item, PlantType type, int DayPlanted) : base (item)
        {
            TileItem = item;
            PlantedType = type;
            this.DayPlanted = DayPlanted;
        }
    }
    #endregion

    public void AddDay()
    {
        day++;
    }
}
