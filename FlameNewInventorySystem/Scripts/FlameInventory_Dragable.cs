using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class FlameInventory_Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

	// For dubbel clicking
	private float lastClickTime;
	public float doubleClickCatchTime = 0.25f;

	// What slot is ours.
	public FlameInventory_ItemDrawerBase origin = null;

	// Wheter a use event was handeled.
	public bool PointHandeled { private set; get; }

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (PointHandeled)
			return;

		if (origin.item != null /*&& inv.isInventoryEditable*/)
		{
			this.transform.position = eventData.position;
			this.transform.SetParent(this.transform.parent.parent.parent.parent);
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{

		if (PointHandeled)
			return;

		// Check if there is an item to drag.
		if (origin.item != null /*&& inv.isInventoryEditable*/)
		{

			// Update the position of draged item.
			this.transform.position = eventData.position;
		}
	}

	public void StopDrag()
	{
		PointHandeled = true;
	}

	public void OnEndDrag(PointerEventData eventData)
	{

		// Set the parent to the new / old parent depending on wheter item was moved.
		this.transform.SetParent(origin.transform);

		// Set position to actual position.
		this.transform.position = origin.transform.position;

		// Enable interaction with the item again.
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	// For item use.
	public void OnPointerDown(PointerEventData eventData)
	{

		// Reset
		PointHandeled = false;

		// Check double click.
		if (Time.time - lastClickTime < doubleClickCatchTime)
		{

			// Double click
			origin.inventoryDrawer.CallItemClick(this, true);
		}
		else
		{
			//normal click
			origin.inventoryDrawer.CallItemClick(this, false);
		}
		lastClickTime = Time.time;
		
	}

	// Activate the tooltip so it renders.
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (origin.inventoryDrawer.tooltip != null)
			origin.inventoryDrawer.tooltip.Activate(origin.item);

	}

	// Deactivate the tooltip.
	public void OnPointerExit(PointerEventData eventData)
	{
		if (origin.inventoryDrawer.tooltip != null)
			origin.inventoryDrawer.tooltip.Deactivate();
	}

	// Swap two items.
	public void OnDrop(PointerEventData eventData)
	{
		
		// For easier access.
		FlameInventory_Dragable other = eventData.pointerDrag.GetComponent<FlameInventory_Dragable>();

		// Check if we are allowed.
		if (other.PointHandeled)
			return;

		FlameInventory_Container.SwapItem(this.origin, other.origin);
		

	}
}
