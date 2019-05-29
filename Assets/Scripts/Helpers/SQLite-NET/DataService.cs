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
            if (!Directory.Exists(Application.streamingAssetsPath + "/Database"))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath + "/Database");
            }
            dbPath = string.Format(Application.streamingAssetsPath + "/Database/{0}", DatabaseName);
        }
        else
        {
            if(!Directory.Exists(Application.persistentDataPath + "/Saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
            }
            dbPath = string.Format(Application.persistentDataPath + "/Database/Saves/{0}", DatabaseName);
        }

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        //Debug.Log("Final Path: " + dbPath);
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
