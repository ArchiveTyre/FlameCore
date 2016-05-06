using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class FlameInventory_Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
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
			this.transform.SetParent(this.transform.parent.parent.parent);
			//this.transform.position = eventData.position;
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

	// Start moving the object around with the cursur.
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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// TODO: Fix!!
	static public int GetSelfIndex(Transform self)
	{
		Transform parent = self.parent;
		int childCount = parent.childCount;
		Debug.Log(childCount);
		for (int i = 0; i < childCount; i++)
		{
			if (parent.GetChild(i) == self)
			{

				return i;
			}
		}
		return -1;
	}
	public void OnDrop(PointerEventData eventData)
	{

		//if (!inventory.isInventoryEditable)
		//	return;

		
		FlameInventory_Dragable otherDrag = eventData.pointerDrag.GetComponent<FlameInventory_Dragable>();

		otherDrag.gameObject.transform.SetParent(otherDrag.origin.transform);


		FlameInventory_Container otherCont = otherDrag.origin.itemContainer;
		FlameInventory_Container myCont = origin.itemContainer;

		int otherIndex = GetSelfIndex(otherDrag.origin.transform);
		int myIndex = GetSelfIndex(origin.transform);
		/*int otherIndex = otherCont.items.BinarySearch(otherDrag.origin.item);
		if (otherIndex < 0)
			otherIndex = -otherIndex;

		int myIndex = myCont.items.BinarySearch(origin.item);
		if (myIndex < 0)
			myIndex = -myIndex;*/

		Debug.Log(otherIndex + " " + myIndex);
		//Flame_Item a = otherCont.items[otherIndex];
		//Flame_Item b = myCont.items[myIndex];

		//FlameInventory_Container.SwapItem(ref a, ref b);

		//bool myItemExists = myCont.items[myIndex] != null;
		//bool otherItemExists = otherCont.items[otherIndex] != null;

		// Save otherItem
		Flame_Item temp = otherCont.items[otherIndex];

		otherCont.items[otherIndex] = myCont.items[myIndex];
		myCont.items[myIndex] = temp;
		/*otherCont.items[otherIndex] = a;
		myCont.items[myIndex] = b;
		*/
		

		otherDrag.origin.UpdateContainer(myCont);
		origin.UpdateContainer(otherCont);

		otherDrag.origin.UpdateItemRef(otherCont.items[otherIndex]);
		origin.UpdateItemRef(myCont.items[myIndex]);



		//ref Flame_Item my_item = ;


	}
}
