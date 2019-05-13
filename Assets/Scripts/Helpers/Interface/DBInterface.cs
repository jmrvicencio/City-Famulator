using System.Collections.Generic;

public interface IOneKey
{
    int Id { get; set; }
}
 public interface IManyKey
{
    int DBForeignKey { get; set; }
}

public interface IDBManyToOne<One, Many>
{
    One one { get; set; }
    List<Many> many { get; set; }
}