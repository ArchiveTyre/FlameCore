using UnityEngine;
using System.Collections;

public class FlameInventory_ExtendedInventory : FlameInventory_Inventory {


	void Start()
	{	
		base.InitInventory();
		
		print (AddItems(2, 1));
		print (AddItems(1, 1));
		print (AddItems(2, 1));
		print (AddItems(1, 1));
		print (AddItems(2, 1));
		print (AddItems(1, 1));
		print (AddItems(2, 1));
		print (AddItems(1, 1));
		print (AddItems(2, 1));
		print (AddItems(1, 1));
		print (AddItems(2, 1));
		print (AddItems(1, 1));
		
		//Do za testing.
		AddItems(0, 32);
		//(0, 16);
		//RemoveItems(0, 17);
		//AddItems(1, 40);
		//RemoveItems(1, 40);
		RemoveItemByIndex(0);
		//RemoveItemByIndex(1);
		
		// Test shoping
		//AddItems (1, 6);
		//if ( RemoveItemsIfExists(1, 4) )
		//	AddItems(0, 1);
		
		//AddItems(0, 1);
		//AddItems(1, 1);
		//AddItems(2, 1);
		
	}
	
	// Add an item by id.
	public int AddItems(int id, int quantity)
	{
		// Get an Item Database referance.
		ItemType itemToAdd = itemDatabase.FetchItemByID(id);
		
		// Then do the operation.
		return base.AddItems(itemToAdd, quantity);
	}
	
	// Remove item by id.
	public void RemoveItems (int id, int quantity)
	{
		
		// Get an Item Database referance.
		ItemType itemToRemove = itemDatabase.FetchItemByID(id);
		
		// Then do the operation.
		base.RemoveItems(itemToRemove, quantity);
		
	}

	public bool RemoveItemsIfExists (int id, int quantity)
	{
		// Get an Item Database referance.
		ItemType itemToRemove = itemDatabase.FetchItemByID(id);
		
		// Then do the operation.
		return RemoveItemsIfExists (itemToRemove, quantity);
	}

	// Get the total amount by id.
	public int GetTotalQuantity (int id)
	{
		// Get an Item Database referance.
		ItemType itemToAdd = itemDatabase.FetchItemByID(id);
		
		// Do the counting with the refernace.
		return base.GetTotalQuantity(itemToAdd);

	}
	
	// Return true if remove was possible and succeded.
	public bool RemoveItemsIfExists (ItemType item, int quantity)
	{
		
		// If we have enough items
		if (base.GetTotalQuantity (item) >= quantity)
		{
			
			// Then remove them
			RemoveItems(item, quantity);
			
			// And return succes.
			return true;
		}
		
		// Else	return false.
		return false;
	}
}
