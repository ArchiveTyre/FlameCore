using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System;

public class FlameInventory_Drawer : MonoBehaviour {

	// The container to draw.
	public FlameInventory_Container container = null;

	// The "item container represent".
	private GameObject itemContainerObject = null;

	public GameObject itemRep = null;

	void OnContainerUpdate()
	{

	}

	public void ChangeContainer(FlameInventory_Container container)
	{
		if (itemContainerObject == null)
		{
			Debug.LogError("Trying to ChangeContainer before init!");
			return;
		}

		this.container = container;
		int children = itemContainerObject.transform.childCount;

		for (int i = 0; i < children; ++i)
		{
			GameObject slot = itemContainerObject.transform.GetChild(i).gameObject;
			Flame_Item item = null;

			// TODO: Better solution....
			try
			{
				item = container.items[i];
			} catch (ArgumentOutOfRangeException)
			{
				item = CreateEmptyItem(i);
			}

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

	private Flame_Item CreateEmptyItem(int index)
	{
		Flame_Item item = new Flame_Item();
		//container.items[index] = item;
		container.items.Insert(index, item);
		Debug.Log("Inserting at: " + index);
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
		cont.AddItem(item);
		ChangeContainer(cont);
		container.SaveContainer();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
