using SQLite4Unity3d;

public class DBItems
{
    [PrimaryKey]
    public string DBName { get; set; }
    public string JsonData { get; set; }

    public override string ToString()
    {
        return $"[DBItem: DBName={DBName}, JsonData={JsonData}]";
    }
}
