using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveUtilsHelper;

public class PersistentDataModel : FarmulatorModel
{
    private List<object> databasesForSave = new List<object>();

    public void IncludedOnSave(object ItemsForSave, DatabaseInfo databaseInfo)
    {
        foreach(object database in databasesForSave)
        {
            //if (
        }
    }   
}

public class ItemDatabaseToSave
{
    List<object> itemsForSave;
    string databaseName;
    string databasePath;

    ItemDatabaseToSave(List<object> ItemsForSave, DatabaseInfo databaseInfo)
    {
        itemsForSave = ItemsForSave;
        databaseName = databaseInfo.DatabaseName;
        databasePath = databaseInfo.DatabasePath;
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  