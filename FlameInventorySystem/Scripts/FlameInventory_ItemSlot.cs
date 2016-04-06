using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class FlameInventory_ItemSlot : MonoBehaviour, IDropHandler {
	
	// Where the slot is located.
	public int slotIndex;
	
	// the referance of the main inventory.
	FlameInventory_Inventory inventory;
	
	public void ResetRef ()
	{
		// Referances only here for extra performance.
		inventory = GetComponentInParent<FlameInventory_Inventory>();
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
			
		FlameInventory_ItemData first = eventData.pointerDrag.GetComponent<FlameInventory_ItemData>();
		FlameInventory_ItemSlot second = this;
		SwitchSlots (first, second);	
    }
	
	public static void SwitchSlots (FlameInventory_ItemSlot a, FlameInventory_ItemSlot b)
	{
		
		// Get new referance.
		FlameInventory_ItemData first = a.GetComponentInChildren<FlameInventory_ItemData>();
		
		// Use the referance.
		SwitchSlots (first, b);
	}
	
	private static void SwitchSlots (FlameInventory_ItemData a, FlameInventory_ItemSlot b)
	{
		
		// Get all the data we'll ever need.
		int aIndex = a.slotIndex;
		
		int bIndex = b.slotIndex;
		
		FlameInventory_Inventory aInv = a.inv;
		
		FlameInventory_Inventory bInv = b.inventory;
		
		FlameInventory_ItemData aData = a;
		
		FlameInventory_ItemData bData = b.GetComponentInChildren<FlameInventory_ItemData>();
		
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
