using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class Inventory_ItemSlot : MonoBehaviour, IDropHandler {
	
	// The list of items.
	private List<ItemType> invItems;
	
	// The list of slots.
	private List<GameObject> invSlots;
	
	// Where the slot is located.
	public int slotIndex;
	
	// the referance of the main inventory.
	Inventory_Inventory inventory;
	
	public void ResetRef ()
	{
		// Referances only here for extra performance.
		inventory = GetComponentInParent<Inventory_Inventory>();
		
		// Get the main items list.
		invItems = inventory.inventoryData.items;
		
		// Get the main slots list.
		invSlots = inventory.GetComponent<Inventory_Inventory>().slots;
	}
	
	// Called on init. We just get some referances.
	void Start ()
	{
		ResetRef();
	}
	
	// When we finally drop the item.
    public void OnDrop(PointerEventData eventData)
    {
		if (!inventory.isInventoryEditable)
			return;
			
		Inventory_ItemData first = eventData.pointerDrag.GetComponent<Inventory_ItemData>();
		Inventory_ItemSlot second = this;
		SwitchSlots (first, second);	
    }
	
	public static void SwitchSlots (Inventory_ItemSlot a, Inventory_ItemSlot b)
	{
		
		// Get new referance.
		Inventory_ItemData first = a.GetComponentInChildren<Inventory_ItemData>();
		
		// Use the referance.
		SwitchSlots (first, b);
	}
	
	private static void SwitchSlots (Inventory_ItemData a, Inventory_ItemSlot b)
	{
		
		// Get all the data we'll ever need.
		int aIndex = a.slotIndex;
		
		int bIndex = b.slotIndex;
		
		Inventory_Inventory aInv = a.inv;
		
		Inventory_Inventory bInv = b.inventory;
		
		Inventory_ItemData aData = a;
		
		Inventory_ItemData bData = b.GetComponentInChildren<Inventory_ItemData>();
		
		GameObject aItem = aData.gameObject;
		
		GameObject bItem;

		Transform aParent = aInv.slots[aIndex].transform;
		
		Transform bParent = bInv.slots[bIndex].transform;
		
		// For A.
		if (a)
		{

			// Set A's new index.
			aData.slotIndex = bIndex;
			
			// Set the position.
			aItem.transform.position = b.transform.position;
			
			// Move A.
			print (bParent.name);
			aItem.transform.SetParent ( bParent );
			
			// Reset the item referances etc.
			aData.ResetRef();
			a.ResetRef();
		}
		
		// For B.
		if (bData)
		{
			
			// Shortcut.
			bItem = bData.gameObject;
			
			// Set B's new index.
			//bSlot.slotIndex = aIndex;
			bData.slotIndex = aIndex;
			
			// Set the position.
			bItem.transform.position = aParent.position;
			
			// Move B.
			print (aParent.name);
			bItem.transform.SetParent (aParent) ;
			
			// Reset the item referances etc.
			bData.ResetRef();
			b.ResetRef();
		}	
	}

}
