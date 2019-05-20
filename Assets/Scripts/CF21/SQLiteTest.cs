using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections.Generic;

public class SQLiteTest : MonoBehaviour
{
    void Start()
    {
        StartSync();
    }

    private void StartSync()
    {
        var ds = new DataService("TempDB.db");
        //ds.CreateTable<DBItem>();
        //ds.Insert<DBItem>(new DBItem { Name = "Kyle", Surname = "Mark" });
        //ds.CreateTable<Address>();
        //ds.Insert<Address>(new Address { DBForeignKey = 1, AddressName = "Sydney" });
        //ds.Insert<Address>(new Address { DBForeignKey = 1, AddressName = "London" });

        var items = ds.Query<DBItem>(x => x.Name == "Kyle");
        List<DBItemComp> mappedItems = new List<DBItemComp>();

        foreach(var item in items)
        {
            DBItemComp mappedItem = new DBItemComp();
            mappedItem.one = item;
            mappedItem.many = new List<Address>();
            var many = ds.Query<Address>(x => x.DBForeignKey == item.Id);

            foreach(var manyItems in many)
            {
                mappedItem.many.Add(manyItems);
                Debug.Log("One matching item");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
