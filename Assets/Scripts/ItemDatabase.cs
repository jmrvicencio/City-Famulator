using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    // creates a list from the Item class. Tbh I'm not sure why there's = new List,Item>();
    private List<Item> database = new List<Item>();
    // creating a Json object
    private JsonData itemData;

    public void Start()
    {
        //converts the json object to c#

        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json "));

        ConstructItemDatabase();
        Debug.Log(FetchItemByID(1).Description);

    }
        public Item FetchItemByID(int id)
        {
           for (int i = 0; i < database.Count; i++)
           
             if (database[i].Id == id)
            {

                   return database[i];
            }


            
               return null;
           

        }


    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            //add a new Item based on the info from the json file
            //you need to adt (int) or .tostring() to convert them
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"], itemData[i]["description"].ToString()));
        }
    }

}

public class Item

{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }

    //constructor for the Item class
    public Item(int id, string title, int value, string description)
    {
        this.Id = id;
        this.Title = title;
        this.Value = value;
        this.Description = description;


    }
    // a constructor for removing items
    public Item()
    {
        this.Id = -1;
    }

}


