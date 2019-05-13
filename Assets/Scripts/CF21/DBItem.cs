using SQLite4Unity3d;
using System.Collections.Generic;

public class DBItem : IOneKey
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={1},  Surname={2}, Age={3}]", Id, Name, Surname, Age);
    }
}

public class Address : IManyKey
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int DBForeignKey { get; set; }
    public string AddressName { get; set; }
}

public class DBItemComp : IDBManyToOne<DBItem, Address>
{
    public DBItem one { get; set; }
    public List<Address> many { get; set; }
}