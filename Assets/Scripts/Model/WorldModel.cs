using System.Collections.Generic;
using UnityEngine;

public class WorldModel : FarmulatorElement
{
    public int day = 1;
    public Dictionary<string, TileData> tileData = new Dictionary<string, TileData>();

    //public enum TileType { Planter, Decor, Obstacle };

    #region Custom Classes
    public class TileData
    {
        //TileType Type { get; set; }
        public TileItem TileItem { get; set; }
    }

    public class PlanterData : TileData
    {
        public PlantType PlantedType { get; set; }
        public int DayPlanted { get; set; }
    }
    #endregion

    public void AddDay()
    {
        day++;
    }
}