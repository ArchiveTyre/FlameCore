using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlameInventory_ItemDrawerBase : MonoBehaviour {

	[HideInInspector]
	public FlameInventory_Container itemContainer = null;

	public Flame_Item item = null;

	Transform image = null;

	void Start()
	{
		image = transform.Find("Item").Find("Image");
	}

	public void UpdateContainer(FlameInventory_Container container)
	{
		if (itemContainer != container)
		{
			itemContainer = container;

			// Item isn't in the new container
			UpdateItemRef(null);
		}
		
	}

	// This is supposed to be overwritten
	public virtual void UpdateItemRef(Flame_Item item)
	{
		
		Image image = transform.Find("Item").Find("Image").GetComponent<Image>() ?? GetComponent<Image>();
		Text text = image.transform.Find("Quantity").GetComponent<Text>();

		transform.Find("Item").GetComponent<FlameInventory_Dragable>().origin = this;
		this.item = item;

		if (item != null)

		// Populated slot
		{

			//image.gameObject.SetActive(true);

			image.sprite = item.GetSprite();
			
			text.text = item.amount.ToString();
		}
		else

		// Blank slot
		{
			image.sprite = null;
			//image.gameObject.SetActive(false);
			text.text = "";
		}
	}

}
