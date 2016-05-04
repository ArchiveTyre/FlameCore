using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlameInventory_ItemDrawerBase : MonoBehaviour {

	private FlameInventory_Container itemContainer = null;

	public Flame_Item item = null;

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
		
		Image image = transform.Find("Item Rep").GetComponent<Image>() ?? GetComponent<Image>();
		Text text = transform.Find("Quantity").GetComponent<Text>();

		if (item != null)

		// Populated slot
		{

			transform.Find("Item Rep").gameObject.SetActive(true);

			image.sprite = item.GetSprite();
			
			text.text = item.amount.ToString();
		}
		else

		// Blank slot
		{
			image.sprite = null;
			transform.Find("Item Rep").gameObject.SetActive(false);
			text.text = "";
		}
	}

}
