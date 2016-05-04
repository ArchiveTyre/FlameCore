using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class FlameInventory_Container : MonoBehaviour {

	public List<Flame_Item> items = new List<Flame_Item>();

	public bool AddItem (Flame_Item o)
	{
		items.Insert(items.Count, o);
		return false;
	}

	public bool AddItem(Flame_Item item, int quantity)
	{
		item.amount = quantity;
		items.Insert(items.Count, item);
		return false;
	}

	public void SaveContainer ()
	{
		string s = JsonMapper.ToJson(items);
		Debug.Log("Saving container as: \n"+s);
	}

	public void LoadContainer()
	{
		string json = "";
		items = JsonMapper.ToObject<List<Flame_Item>>(json);
	}

	public static void SwapItem(ref Flame_Item a, ref Flame_Item b)
	{
		Flame_Item c = a;
		a = b;
		b = c; // C is old a.
	}

	public Flame_Item GetBySlug(String slug)
	{
		foreach (Flame_Item f in items)
		{
			if (f.slug == slug)
			{
				return f;
			}
		}
		return null;
	}
	
}

[Serializable]
public class Flame_Item
{

	public string slug;
	public int id;
	public string title = "Undefined";
	public string description = "No information";
	public int amount = 1;

	// The folder inside a Resources folder that contains the sprites. Has to end with "/".
	public string spriteFolderPath = "Sprites/Items/";
	public bool destroyOnNonPosetiv = false;
	public bool stackable = true;
	
	private Sprite sprite;

	public IDictionary<string, object> Stats = new Dictionary<string, object>();

	// The main item class.
	public Flame_Item(int id, string slug)
	{

		this.id = id;
		this.slug = slug;

		// Load the sprite based on the slug!
		ReloadItemSprite();

		

		/*// ID
		this.ID = id;

		// Title
		this.Title = title;

		// Value of the items, in shops for example.
		this.Value = value;

		// Description
		this.Description = description;

		// If the item is Stackable or not.
		this.Stackable = stackable;

		// The rarity of the item.
		this.Rarity = rarity;

		// The slug.
		this.Slug = slug;

		// If we are going to destroy the object if it's Amount is not posetiv.
		this.DestoryOnNonPosetiv = destoryOnNonPosetiv;*/
	}

	// Get the sprite	
	public Sprite GetSprite()
	{

		// Return the referance.
		return sprite;
	}

	// Refreshes / Reloads the sprite referance of the item.
	public void ReloadItemSprite()
	{

		// Load.
		this.sprite = Resources.Load<Sprite>(spriteFolderPath + slug);

		// Check for failures.
		if (this.sprite == null)
		{

			// Error, we failed!
			Debug.LogError("Could not find sprite for: " + slug);

			// Set the referance to a garantued existing sprite. "missing_sprite".
			this.sprite = Resources.Load<Sprite>(spriteFolderPath + "missing_sprite");
		}
	}

	// Returns all the Stat names.
	public string[] GetStatNames()
	{
		// Create a array.
		string[] stats = new string[Stats.Count];

		// Create a counter.
		int i = 0;

		// Then cycle through all vars.
		foreach (string s in Stats.Keys)
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
	public Flame_Item()
	{

		// ID -1 means an empty object.
		this.id = -1;
	}
}