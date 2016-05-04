using UnityEngine;
//using System.Collections;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Serialization;

public class FlameInventory_ItemDatabase : MonoBehaviour {

	private List<OldItemType> database = new List<OldItemType>();
	private JsonData itemData;

	// Inside of StreamingAssets. also has a default value for convinience.
	[SerializeField] string ItemDatabaseFilePath = "Items.json";

	void Start () 
	{
		
		// For convinience we pre generate the file path.
		string fileString = File.ReadAllText (Application.dataPath + "/StreamingAssets/"+ItemDatabaseFilePath);

		// If the Item.json is not formated correctly then the error message is wierd. So we replace the error message and stop the game.
        try {
           
			// Read the file.
			itemData = JsonMapper.ToObject (fileString);
        }
        catch (JsonException) {
			
			// Print the error message.
			Debug.LogAssertion(ItemDatabaseFilePath+"File not formated correctly.");

			// Stop the time
			Time.timeScale = 0;
			
			// Return so that no other messages get displayed.
			 return;
        }
		
		// We have read the file, now we need to parse it using our dictionary.
		ConstructItemDatabase ();
	}
	
	// Get the item in database by ID, if item not found return null
	public OldItemType FetchItemByID(int id) {
		
		// Make sure tha database has been inited.
		if (itemData == null)
		{
			// If not then init it!
			Start();
		}
		
		// Loop through the database.
		for (int i = 0; i < database.Count; i++)
		{
			
			// Check if the ID's match
			if ((int)database[i].ID == id)
			{
				
				// If they do then return a referacne.
				return database[i];
			}
		}
		// If the loop exists then we didn't find anything.
		Debug.Log("Unable to find item by id, id is: " + id);
		
		// Return null referacne.
		return null;
	}

	void ConstructItemDatabase() // Dictionary, key and value.
	{
		
		// For each item in the read file.
		for (int i = 0; i < itemData.Count; i++) 
		{

			// Add the item that is pointed.
			database.Add (new OldItemType (
				(int)itemData[i]["id"], 
				itemData[i]["title"].ToString(), 
				(int)itemData[i]["value"],
				itemData[i]["description"].ToString(),				
				(bool) itemData[i]["stackable"],
				(int)itemData[i]["rarity"],
				itemData[i]["slug"].ToString(),
				(bool) itemData[i]["Destroy if not posetiv"],
				(JsonData)itemData[i]["stats"]
			
			));
		}
	}
}
	
public class OldItemType {
	
	// The folder inside a Resources folder that contains the sprites. Has to end with "/".
	public string spriteFolderPath = "Sprites/Items/";
	
	
	// Properties go with an Capital letter!
	public int ID {get; set;}
	public string Title { get; set;}
	public int Value { get; set;}
	
	public bool DestoryOnNonPosetiv {get; set;}
	public string Description {get; set;}
	public bool Stackable {get; set;}
	public int Rarity {get; set;}
	public string Slug {get; set;}
	private Sprite Sprite {get; set;}
	public int Amount {get; set;}
	
	private IDictionary<string, object> Stats = new Dictionary<string, object>();
	
	// The main item class.
	public OldItemType(int id, string title, int value, string description, bool stackable, int rarity, string slug, bool destoryOnNonPosetiv, JsonData stats)
	{
		
		// ID
		this.ID = id;
		
		// Title
		this.Title = title;
		
		// Value of the items, in shops for example.
		this.Value = value;
		
		// List through all stats and add them. 
		foreach(string s in stats.Keys)
		
			// When we found something we add it.
  			Stats.Add ( s, (int) stats[s]);
		
		// Description
		this.Description = description;
		
		// If the item is Stackable or not.
		this.Stackable = stackable;
		
		// The rarity of the item.
		this.Rarity = rarity;
		
		// The slug.
		this.Slug = slug;
		
		// If we are going to destroy the object if it's Amount is not posetiv.
		this.DestoryOnNonPosetiv = destoryOnNonPosetiv;
		
		// Load the sprite based on the slug!
		ReloadItemSprite();
		
		
		
	}
	
	// Get the sprite	
	public Sprite GetItemSprite()
	{
		
		// Return the referance.
		return Sprite;
	}
	
	// Refreshes / Reloads the sprite referance of the item.
	public void ReloadItemSprite()
	{
		
		// Load.
		this.Sprite = Resources.Load<Sprite>(spriteFolderPath + Slug);
		
		// Check for failures.
		if (this.Sprite == null)
		{
			
			// Error, we failed!
			Debug.LogError("Could not find sprite for: "+Slug);
			
			// Set the referance to a garantued existing sprite. "missing_sprite".
			this.Sprite = Resources.Load<Sprite>(spriteFolderPath+"missing_sprite");
		}
	}
	
	// Returns all the Stat names.
	public string [] GetStatNames ()
	{
		// Create a array.
		string [] stats = new string [Stats.Count];
		
		// Create a counter.
		int i = 0;
		
		// Then cycle through all vars.
		foreach(string s in Stats.Keys)
		{
			
			// Set the vars.
			stats[i] = s;
			
			// Increment the counter.
			i++;
		}
		return stats;
		
	}
	
	// Returns an T.
	public T GetStat<T>(string statName)
	{
		
		// Check if the request is the type of "object".
		if (Stats[statName] is T)
		{
			return (T)Stats[statName];
		}
		
		// If not we need to convert it to T.
		else
		{
			
			// This may fail so we try.
			try
			{
				
				// Return the converted type.
				return (T)Convert.ChangeType(Stats[statName], typeof(T));
    		}
			
			// We failed
			catch (InvalidCastException)
			{
				
				// Return default type.
				return default(T);
			}
		}
	}

		
		
	// For empty items
	public OldItemType() 
	{
		
		// ID -1 means an empty object.
		this.ID = -1;
	}
	
}
