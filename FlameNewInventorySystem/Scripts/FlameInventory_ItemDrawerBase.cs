using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlameInventory_ItemDrawerBase : MonoBehaviour {

	// The container that the item is in.
	[HideInInspector]
	public FlameInventory_Container itemContainer = null;

	// The current item
	public Flame_Item item = null;

	[HideInInspector]
	public FlameInventory_InventoryDrawer inventoryDrawer = null;

	//Transform image = null;
	private Image image;
	private Text text;

	// Whether we have initialised.
	private bool hasInitialised = false;

	void Start()
	{
	
		// Get some refs and set flag hasInitialised.
		image = transform.Find("Item").Find("Image").GetComponent<Image>() ?? GetComponent<Image>();
		text = image.transform.Find("Quantity").GetComponent<Text>();
		transform.Find("Item").GetComponent<FlameInventory_Dragable>().origin = this;
		inventoryDrawer = GetComponentInParent<FlameInventory_InventoryDrawer>();
		hasInitialised = true;
	}

	public void UpdateContainer(FlameInventory_Container container)
	{

		// If we have not initialised, then do so.
		if (hasInitialised != true)
			Start();

		// This is only neccessary if the new container is not the old container.
		if (itemContainer != container)
		{

			// Update it.
			itemContainer = container;

			// Item isn't in the new container
			UpdateItemRef(new Flame_Item());
		}
		
	}

	// This is supposed to be overwritten
	public virtual void UpdateItemRef(Flame_Item item)
	{

		if (item == null) item = new Flame_Item();

		// Update the new item.
		this.item = item;

		// Check whether item is populated or not.
		if (item.id != -1 )

		// Populated slot
		{

			// Make not transparent.
			Color color = image.color.From();
			color.a = 255f;
			image.color = color;

			// Update the sprite
			image.sprite = item.GetSprite();
			
			// Add the amount.
			text.text = item.amount.ToString();
		}

		else

		// Blank slot
		{

			// Make transparent.
			Color color = image.color.From();
			color.a = 0f;
			image.color = color;
			
			// Remove any other side effects.
			text.text = "";
		}
	}

}
