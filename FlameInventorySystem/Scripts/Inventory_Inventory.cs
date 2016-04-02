using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class Inventory_Inventory : MonoBehaviour {

	// If in-game edits are allowed.
	public bool isInventoryEditable = true;

	GameObject slotPanel;
	public Inventory_ItemDatabase itemDatabase;
	
	// The item database. If left null then will auto search.
	
	// This is a separate class because it is easier to make a json object out of.
	public Inventory_InventoryData inventoryData = new Inventory_InventoryData ();
	public GameObject inventorySlotPrefab;
	public GameObject inventoryItemPrefab;
	
	// The amount of slots that will be created.
	[SerializeField] private int slotAmount;
	
	public List<GameObject> slots = new List<GameObject>();
	
	void Start()
	{	
		InitInventory();
	}
	
	public void InitInventory ()
	{
		
		// Log anny error we might get referance getting of item databse.
		if (itemDatabase == null)
		{
			// There was an error, log it!
			Debug.LogError("Error: no item database.");
			
			// No point in continueing.
			return;
		}
		
		// Set the amount of slots.
		slotAmount = 20-8;
		
		// Find all necessary objects
		GameObject inventoryPanel = gameObject.transform.FindChild("Inventory Panel").gameObject;
		
		// Then find the slot "container"
		slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
		
		// Create za slots.
		for(int i = 0; i < slotAmount; i++)
		{
			
			// Create a slot.
			ReCreateSlot (i);
			
			// Add an empty item.
			inventoryData.items.Add(new ItemType());	
		}
	}
	
	public void RemoveItems (ItemType item, int quantity)
	{
		for (int i = 0; i < inventoryData.items.Count; i++)
		{
			if (quantity <= 0)
				break;
				
			if (inventoryData.items[i].ID == item.ID)
			{
				quantity -= RemoveItemByIndex (i);
			}
		}
		
		// Give back the overflow. Supose quantity is negativ. - + - = +
		AddItems(item, -quantity);
	}
	
	// Find and elimante. Returns the amount removed
	public int DestroyItemAndSlot (ItemType item)
	{
		
		print ("Destroy: " + item.Slug);
		
		for (int i = 0; i < inventoryData.items.Count; i++)
		{
			if (inventoryData.items[i] == item)
			{
				return RemoveItemByIndex (i);
			}
		}
		
		//Nothing removed :(
		return 0;

	}
	
	private void ReCreateSlot (int index)
	{
		// Destory old and initialize the slot.
		if (index < slots.Count)
		{
			GameObject.Destroy(slots[index]);
			slots[index] = Instantiate(inventorySlotPrefab);
		}
		else
		{
			slots.Add(Instantiate(inventorySlotPrefab));
		}
			
			// Give it an index.
			slots[index].GetComponent<Inventory_ItemSlot>().slotIndex = index;
			
			// Set it's parent.
			slots[index].transform.SetParent(slotPanel.transform);
	}
	
	// Returns the amount destoryed.
	public int RemoveItemByIndex (int index)
	{
			int quantityRemoved = inventoryData.items[index].Amount;
		
			// Overide with new Item.
			inventoryData.items[index] = new ItemType();
			
			ReCreateSlot (index);
			
			return quantityRemoved;
	}
	
	// Returns the index where item was added. Returns -1 if no index was possible
	public int AddItems(ItemType itemToAdd, int quantity) 
	{
		
		// If it alrerady exists, then get its index. Will return -1 if does not exist.
		int coExistorIndex = DoesItemExistInInventory(itemToAdd);
		if (itemToAdd.Stackable && coExistorIndex >= 0)
		{

			//  Get the ItemData through itemToAdd.Slug (Because it gets renamed from Slot to Slug) and it's child no 0.
			Inventory_ItemData data = slots[coExistorIndex].transform.FindChild(itemToAdd.Slug).GetComponent<Inventory_ItemData>();
			
			//print (data.name); Did not exist!
			data.Amount+=quantity;
			
			// Return where the items was added.
			return coExistorIndex;
			
		}
		for (int i = 0; i < inventoryData.items.Count; i++)
		{
			// Check if slot is empty
			if(inventoryData.items[i].ID == -1)
			{

				// If it is then populate it.
				inventoryData.items[i] = itemToAdd;
				
				//Create the Item container.
				GameObject itemObj = Instantiate(inventoryItemPrefab);
				
				// Save the new item's data to the item data class.
                itemObj.GetComponent<Inventory_ItemData>().item = itemToAdd;
                
				// Save the index to item data so that we can easily use it.
				itemObj.GetComponent<Inventory_ItemData>().slotIndex = i;
				
				// Give the newly created Item container an parent.
				itemObj.transform.SetParent(slots[i].transform);
				
				// Reload item icon
				itemToAdd.ReloadItemSprite();
				
				// Change item icon.
				itemObj.GetComponent<Image>().sprite=itemToAdd.GetItemSprite();
				
				// Set the relative position to be in the center of the container.. TODO: We should automate this.
				itemObj.transform.position = new Vector2 (12, -12);
				
				// Set the name of of the Slot to the object Titel. TODO: Move some where else.
				slots[i].name = itemToAdd.Title;
				
				// Set the Item container to the object Slug.
				itemObj.name = itemToAdd.Slug;
				
				// Store the quantity.
				slots[i].transform.GetChild (0).GetComponent<Inventory_ItemData>().Amount = quantity;
				
				// If we find a slot then break the loop.
				return i;
			}
		}
		return -1;
	}
	
	
	
	//Returns the quantity of a certain item in the inventory.
	public int GetTotalQuantity (ItemType item)
	{
		
		// The main counter
		int quantity = 0;
		
		// Loop through items.
		for(int i = 0; i < inventoryData.items.Count; i++)
		{
			
			// Check if right
			if (inventoryData.items[i].ID == item.ID)
			{
				
				// If it is, add to quantity the amonut that it is.
				quantity += slots[i].transform.GetComponentInChildren<Inventory_ItemData>().Amount;
			}
		}
		
		// Return the sum of all items's amount variable.
		return quantity;
	}
	
	//Returns the inventory index of the co existor
	int DoesItemExistInInventory (ItemType item)
	{
		
		// Loop through inventory
		for (int i = 0; i < inventoryData.items.Count; i++)
		{
			// Check if they are equal
			if (inventoryData.items[i].ID == item.ID)
			{
				
				// It existed, return the pointer
				return i;
			}
		}
		
		// We didn't find anything :(
		return -1;
	}
	
	// Copy items 
	public void InventoryCopyAllItems (Inventory_InventoryData invRef)
	{
		// Load Items.
		for(int i = 0; i < invRef.items.Count - 1; i++)
		{
			
			//
			if (invRef.items[i].ID!=-1)
			
				//
				AddItems(invRef.items[i], invRef.items[i].Amount);
		}
	}
}

public class Inventory_InventoryData
{
	public List<ItemType>items = new List<ItemType>();
	
	// Save to file
	public void InventorySaveToFile (string filePath)
	{
		
		
		// Get content.
		JsonData jsonData = JsonMapper.ToJson(this);
		
		// save content
		File.WriteAllText(Application.dataPath + "/StreamingAssets/"+filePath,  jsonData.ToString() );
	}
	
	// Parse the inventory.
	public string InventorySaveToString ()
	{
		
		// Get content.
		JsonData jsonData = JsonMapper.ToJson(this);
		
		// Return content.
		return jsonData.ToString();
	}
	
	// Get Inventory_InventoryData from file.
	static public Inventory_InventoryData InventoryGetFromFile (string filePath)
	{
		
		// Get the data.
		string fileString = "";
		
		// Check if reading files are allowed
		#if UNITY_STANDALONE
			fileString = File.ReadAllText (Application.dataPath + "/StreamingAssets/"+filePath);
		#else
			// If not then only allow reading from the Rescources folder
			TextAsset bindata= Resources.Load(filePath) as TextAsset;
			fileString = bindata.text;
		#endif
		
		// Parse using data.
		return InventoryGetFromString (fileString);
	}
	
	public IEnumerator InventoryGetFromHttp (string address)
	{
		// Create WWW class.
		WWW www = new WWW(address);
		
		// Wait for it to get the string.
        yield return www;
		
		// Set the fileString variable.
        string fileString = www.text;
		
		// Parse using data.
		yield return InventoryGetFromString (fileString);
	}
	
	// Get Inventory_InventoryData from string.
	static public Inventory_InventoryData InventoryGetFromString (string dataString)
	{
		
		// If the file is not formated correctly then the error message is wierd. So we replace the error message and stop the game.
        try
		{
           
			// Return the data.
			return JsonMapper.ToObject<Inventory_InventoryData> (dataString);
        }
        catch (JsonException)
		{
			
			// Print the error message.
			Debug.LogAssertion(dataString+" File not formated correctly.");

			// Stop the time
			Time.timeScale = 0;
			
			// Return so that no other messages get displayed.
			 return null;
        }
	}
}
