using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class FlameInventory_Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

	// What slot is ours.
	public FlameInventory_ItemDrawerBase origin = null;

	// Wheter a use event was handeled.
	private bool pointHandeled;

	// You can overidde this if you want a custom handler. return true if handled. TODO: Use events instead...
	public virtual bool OnUse()
	{
		return false;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (pointHandeled)
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
		if (pointHandeled)
			return;

		// Check if there is an item to drag.
		if (origin.item != null /*&& inv.isInventoryEditable*/)
		{

			// Update the position of draged item.
			this.transform.position = eventData.position;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// Check if edits are allowed.
		//if (!inv.isInventoryEditable)

			// If not, then quickly return;
			//return;

		// Set the parent to the new / old parent depending on wheter item was moved.
		this.transform.SetParent(origin.transform);

		// Set position to actual position.
		this.transform.position = origin.transform.position;

		// Enable interaction with the item again.
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	// TODO: Is this neccesary? If so we need better handeling.
	public void OnPointerDown(PointerEventData eventData)
	{

		pointHandeled = this.OnUse();

		if (pointHandeled)
			return;

		// Check if edits are allowed.
		//if (!inv.isInventoryEditable)

			// If not, then quickly return;
			//return;
	}

	// Activate the tooltip so it renders.
	public void OnPointerEnter(PointerEventData eventData)
	{
		//tooltip.Activate(item);

	}

	// Deactivate the tooltip.
	public void OnPointerExit(PointerEventData eventData)
	{
		//tooltip.Deactive();
	}

	// Swap two items.
	public void OnDrop(PointerEventData eventData)
	{

		//if (!inventory.isInventoryEditable)
		//	return;
		
		// For easier access.
		FlameInventory_Dragable other = eventData.pointerDrag.GetComponent<FlameInventory_Dragable>();

		FlameInventory_Container.SwapItem(this.origin, other.origin);
		

	}
}
