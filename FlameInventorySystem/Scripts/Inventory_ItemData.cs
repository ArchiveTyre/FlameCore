using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Text;

public class Inventory_ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
	public ItemType item;
	
	// For dubbel clicking

	// Wheter a use event was handeled.
	private bool pointHandeled;

	
	private Text amountText;
	public int Amount {
		
		get
		{
			return item.Amount;
		}
		set
		{
			item.Amount = value;		
		}
		
	}
	[HideInInspector] public int slotIndex;
	private Vector2 offset;
	[HideInInspector] public Inventory_Inventory inv;
	
	private Inventory_ItemTooltip tooltip;
	
	public void ResetRef ()
	{
		inv = GetComponentInParent<Inventory_Inventory>();
	}
	
	void Start ()
	{
		ResetRef();
		tooltip = inv.GetComponent<Inventory_ItemTooltip>();
		amountText = gameObject.transform.FindChild("Stack Amount").GetComponent<Text>();
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		
		if (pointHandeled)
			return;
		
		if (item != null && inv.isInventoryEditable)
		{
			this.transform.SetParent(this.transform.parent.parent);
			this.transform.position = eventData.position;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		
		if (pointHandeled)
			return;
		
		// Check if there is an item to drag.
		if (item != null && inv.isInventoryEditable)
		{
			
			// Update the position of draged item.
			this.transform.position = eventData.position;
		}
	}
	
	// Update transform when the drag is ended.
	public void OnEndDrag(PointerEventData eventData)
	{
		
		// Check if edits are allowed.
		if (!inv.isInventoryEditable)
		
			// If not, then quickly return;
			return;
			
		// Set the parent to the new / old parent depending on wheter item was moved.
		this.transform.SetParent(inv.slots[slotIndex].transform);
		
		// Set position to actual position.
		this.transform.position = inv.slots[slotIndex].transform.transform.position;
		
		// Enable interaction with the item again.
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	// You can overidde this if you want a custom handler. return true if handled.
	public virtual bool OnUse()
	{
		return false;
	}


	// Start moving the object around with the cursur.
	public void OnPointerDown(PointerEventData eventData)
	{
		
		pointHandeled = this.OnUse();
		
		if (pointHandeled)
			return;
		
		// Check if edits are allowed.
		if (!inv.isInventoryEditable)
		
			// If not, then quickly return;
			return;
		
		// Check if there is an item to drag.
		if (item != null)
		{
			
			// Update the position of draged item.
			offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
		}
	}

	// Activate the tooltip so it renders.
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
		
    }

	// Deactivate the tooltip.
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactive();
    }
	
	void OnGUI ()
	{
		
		// Make sure amountText exists!
		if (!amountText)
			{
				
				// If it doesn't then make it exist!
				Start();
			}
			
			// We only display quantity if necessary
				if (item.Stackable)
				{
					
					// Check if we are allowed to remove it.
					if (Amount <= 0 && item.DestoryOnNonPosetiv)
					{
						
						// We were, remove it!
						transform.parent.transform.parent.transform.parent.GetComponentInParent<Inventory_Inventory>().DestroyItemAndSlot(item);
					}
					
					// Display was neccesary
					amountText.text = Amount.ToString();
					
				}
				else
				{
					
					// Display nothing, override with "".
					amountText.text = "";
				}
	}

}
