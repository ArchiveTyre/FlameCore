using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class FlameInventory_Container : MonoBehaviour {

	public List<Flame_Item> items = new List<Flame_Item>();

	public delegate void ItemSwapAction(int index);
	//[SyncEvent]
	public event ItemSwapAction OnSwap;


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
		LoadContainer(json);
	}

	public void LoadContainer(string json)
	{

		// Load the items
		items = JsonMapper.ToObject<List<Flame_Item>>(json);

		// Reload their sprites...
		foreach (Flame_Item item in items)
		{
			item.ReloadItemSprite();
		}
	}

	// Swaps two items.
	public static void SwapItem(FlameInventory_ItemDrawerBase a, FlameInventory_ItemDrawerBase b)
	{

		// For convinence.
		FlameInventory_Container aContainer = a.itemContainer;
		FlameInventory_Container bContainer = b.itemContainer;

		// Get the drawers.
		FlameInventory_InventoryDrawer aDraw = a.GetComponentInParent<FlameInventory_InventoryDrawer>();
		FlameInventory_InventoryDrawer bDraw = b.GetComponentInParent<FlameInventory_InventoryDrawer>();

		// TODO: A and B should be sepparated.
		if (aContainer != aDraw.container
			|| bContainer != bDraw.container)
		{

			a.itemContainer = a.GetComponentInParent<FlameInventory_Container>();
			b.itemContainer = b.GetComponentInParent<FlameInventory_Container>();
			Debug.Log("Refresh a and b");
			// Refresh
			aDraw.RefreshContainer();
			bDraw.RefreshContainer();

			return;
		}
		
		// Get the positions.
		int aIndex = a.transform.GetSelfIndex();
		int bIndex = b.transform.GetSelfIndex();

		// Check if we should block
		{
			bool aA = !aDraw.CallMoveAction(bDraw, a.item, b.item);
			bool bA = !bDraw.CallMoveAction(aDraw, b.item, a.item);
			bool shouldBlock = aA || bA;
			if (shouldBlock)
				return;
		}

		// Perform swap
		Flame_Item tempItem = aContainer.items[aIndex];
		aContainer.items[aIndex] = bContainer.items[bIndex];
		bContainer.items[bIndex] = tempItem;

		// Activate event.
		if (aContainer.OnSwap != null)
			aContainer.OnSwap(aIndex);
		if (bContainer.OnSwap != null)
			bContainer.OnSwap(bIndex);

		// Update refs. (In case there was a -1 to a posetiv swap.
		a.UpdateItemRef(aContainer.items[aIndex]);
		b.UpdateItemRef(bContainer.items[bIndex]);

	}

	// To get an item ref by slug.
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
public class Flame_Item : IComparable
{

	public string slug = "";
	public int id;
	public string title = "Undefined";
	public string description = "No information";
	public int amount = 1;

	// The folder inside a Resources folder that contains the sprites. Has to end with "/".
	public string spriteFolderPath = "Sprites/Items/";
	public bool removeIfNan = false;
	public bool stackable = true;
	
	private Sprite sprite;

	//public IDictionary<string, object> Stats = new Dictionary<string, object>();
	public Flame_Attr stats = new Flame_Attr();

	// The main item class.
	public Flame_Item(int id, string slug)
	{

		this.id = id;
		this.slug = slug;

		// Load the sprite based on the slug!
		ReloadItemSprite();

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
			if (this.slug == "")
			{
				Debug.LogError("Slug not set.");
			}

			else
			{
				// Error, we failed!
				Debug.LogError("Could not find sprite: " + spriteFolderPath + slug);
			}

			// Set the referance to a garantued existing sprite. "missing_sprite".
			this.sprite = Resources.Load<Sprite>(spriteFolderPath + "missing_sprite");
		}
	}

	// Returns all the Stat names.
	public string[] GetStatNames()
	{
		// Create a array.
		string[] lstats = new string[stats.content.Count];

		// Create a counter.
		int i = 0;

		// Then cycle through all vars.
		foreach (string s in stats.content.Keys)
		{

			// Set the vars.
			lstats[i] = s;

			// Increment the counter.
			i++;
		}
		return lstats;

	}

	public void SetStat(string statName, int value)
	{
		stats.content[statName] = ""+value;
	}

	// Returns an T.
	public string GetStat (string statName)
	{
		return stats.content[statName];
	}

	public int CompareTo(object obj)
	{
		if (obj == this)
			return 0;
		return 1;
	}

	// For empty items
	public Flame_Item()
	{

		// ID -1 means an empty object.
		this.id = -1;
	}

}