using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System;

public class FlameInventory_InventoryDrawer : MonoBehaviour {

	public delegate void ItemClickAction(FlameInventory_Dragable drag, bool isDoubleClick);
	//[SyncEvent]
	public event ItemClickAction OnItemClick;

	public delegate void MoveItemAction(FlameInventory_InventoryDrawer self, FlameInventory_InventoryDrawer other, Flame_Item a, Flame_Item b);
	//[SyncEvent]
	public event MoveItemAction OnMove;

	

	// The container to draw.
	public FlameInventory_Container container = null;

	// The "item container represent".
	private GameObject itemContainerObject = null;

	[Tooltip("This object will be activate upon hover and disabled upon non-hover")]
	public FlameInventory_Tooltip tooltip = null;

	public GameObject itemRep = null;

	[HideInInspector]
	public bool blockingMove = false;

	public void BlockMove ()
	{
		blockingMove = true;
	}

	// Returns false if move should be avoided.
	public bool CallMoveAction(FlameInventory_InventoryDrawer other, Flame_Item a, Flame_Item b)
	{
		this.blockingMove = false;
		if (OnMove != null)
			OnMove(this, other, a, b);
		return !blockingMove;
	}

	public void CallItemClick(FlameInventory_Dragable drag, bool isDoubleClick)
	{
		if (OnItemClick != null)
			OnItemClick(drag, isDoubleClick);
	}

	void OnItemSwap(int index)
	{
		GameObject slot = itemContainerObject.transform.GetChild(index).gameObject;
		FlameInventory_ItemDrawerBase itr = slot.GetComponentInChildren<FlameInventory_ItemDrawerBase>();
		itr.UpdateContainer(container);
		Flame_Item item = (index >= container.items.Count) ? CreateEmptyItem(index) : container.items[index];
		itr.UpdateItemRef(item);

	}

	public void RefreshContainer()
	{
		// Loop through each child object.
		for (int index = 0; index < itemContainerObject.transform.childCount; ++index)
		{
			GameObject slot = itemContainerObject.transform.GetChild(index).gameObject;

			Flame_Item item = (index >= container.items.Count) ? CreateEmptyItem(index) : container.items[index];

			FlameInventory_ItemDrawerBase itr = slot.GetComponentInChildren<FlameInventory_ItemDrawerBase>();

			if (itr == null)
			{
				Debug.LogError("Error child " + slot + " in " + itemContainerObject + " does not have an ItemRep");
			}

			else
			{
				itr.UpdateContainer(container);
				itr.UpdateItemRef(item);
			}

		}
	}
	public void ChangeContainer(FlameInventory_Container newContainer)
	{
		
		if (itemContainerObject == null)
		{
			Debug.LogError("Trying to ChangeContainer before init!");
			return;
		}

		// First remove old event.
		this.container.OnSwap -= OnItemSwap;

		// Then re-add the old event.
		newContainer.OnSwap += OnItemSwap;

		// Set the new container.
		this.container = newContainer;

		// Refresh container.
		RefreshContainer();
		
	}

	private Flame_Item CreateEmptyItem(int index)
	{
		Flame_Item item = new Flame_Item();
		int adjustedIndex = index;
		if (index > container.items.Count)
			adjustedIndex = container.items.Count;

		Debug.Log(adjustedIndex);
		container.items.Insert(/*index*/ adjustedIndex, item);

		return item;
	}

	private GameObject CreateRep()
	{
		GameObject itr = GameObject.Instantiate(itemRep);
		itr.transform.SetParent(itemContainerObject.transform);
		return itr;

	}

	// Use this for initialization
	void Start () {

		if (itemContainerObject == null)
		{
			LayoutGroup lg = gameObject.GetComponentInChildren<LayoutGroup>();
			itemContainerObject = lg.gameObject;
		}


		// For tests...
		/*Flame_Item item = new Flame_Item(0, "old_rusty_sword");
		item.SetStat("Strength", 16);
		container.AddItem(item, 32);
		container.SaveContainer();
		ChangeContainer(container);*/
			FlameInventory_Container cont = gameObject.AddComponent<FlameInventory_Container>();
		cont.LoadContainer(@"[{""slug"":""old_rusty_sword"",""id"":0,""title"":""An old rusty sword"",""Stats"":{""Strength"":16}}]");
		Flame_Item item = new Flame_Item(0, "old_rusty_sword");
		cont.AddItem(item, 1);
		ChangeContainer(cont);
		container.SaveContainer();


	}
}
