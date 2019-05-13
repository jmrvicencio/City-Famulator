using SQLite4Unity3d;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

public class DataService
{
    private SQLiteConnection _connection;

    public DataService(string DatabaseName, bool createLocalDB = false)
    {
        string dbPath;
        if (createLocalDB)
        {
            if (!Directory.Exists(@"Assets/StreamingAssets/Database/"))
            {
                Directory.CreateDirectory(@"Assets/StreamingAssets/Database/");
            }
            dbPath = string.Format(@"Assets/StreamingAssets/Database/{0}", DatabaseName);
        }
        else
        {
#if UNITY_EDITOR
            if (!Directory.Exists(@"Assets/StreamingAssets/Saves/"))
            {
                Directory.CreateDirectory(@"Assets/StreamingAssets/Saves/");
            }
            dbPath = string.Format(@"Assets/StreamingAssets/Saves/{0}", DatabaseName);
#else
        //check if file exists in Application.persistentDataPath
        var filePath = string.Format("{0}/Saves/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filePath))
        {
            Debug.Log("Database not in Persistent path");
            var loadDb = Application.dataPath + "/StreamingAssets/Saves/" + DatabaseName;
            File.Copy(loadDb, filePath);
        }

        var dbPath = filePath;
#endif
        }

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final Path: " + dbPath);
    }

    public void CreateTable<T>()
    {
        _connection.CreateTable<T>();
    }

    public void Insert<T>(T item)
    {
        _connection.Insert(item);
    }

    public IEnumerable<T> Query<T>(Expression<Func<T, bool>> filter) where T : new()
    {
        return _connection.Table<T>().Where(filter);
    }

    public IEnumerable<T> Query<T>() where T : new()
    {
        return _connection.Table<T>();
    }
}
