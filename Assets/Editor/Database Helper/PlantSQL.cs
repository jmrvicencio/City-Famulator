using SQLite4Unity3d;

public class PlantSQL
{
    [PrimaryKey]
    public string PlantId { get; set; }
    public ConstEnums.PlantType PlantType { get; set; }
    public string PlantData { get; set; }
}
