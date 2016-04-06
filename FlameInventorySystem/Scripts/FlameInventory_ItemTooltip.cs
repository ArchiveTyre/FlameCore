using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FlameInventory_ItemTooltip : MonoBehaviour {

	private float lastClickTime = 0;

	// Time between a suposed dubbel click.
	[SerializeField] private float catchTime = 0.25f;

	// Item referance, used for constructing the data.
	private ItemType item;
	
	// The text object. We have it here for extra performance. TODO: Should we be re-checking this one?
	private Text dataTextObject;
	
	// The main tooltip object.
	private GameObject tooltip;
	
	// On start find all necesarry referances.
	void Start ()
	{
		
		// For the tooltip object.
		tooltip = gameObject.transform.FindChild("Tooltip").gameObject;
		
		// For the text inside tooltip.
		dataTextObject = tooltip.transform.FindChild("Data").GetComponent<Text>();

        // Disable the tooltip for now.
        tooltip.SetActive(false);
	}
	
	void Update ()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			if(Time.time-lastClickTime<catchTime)
			{
				//double click
				//item.
			}
			else
			{
				//normal click
			}
			lastClickTime=Time.time;
		}
		// Update only necessary if tooltip is enabled.
		if (tooltip.activeSelf)
		{
				
			// Update position.
			tooltip.transform.position = Input.mousePosition;
		}
		
	}
	
	// Show the tooltip as well as store important data.
	public void Activate(ItemType item)
	{
		
		// Reset the item.
		this.item = item;
		
		// Construct.
		ConstructDataString();
		
		// Set as active.
		tooltip.SetActive(true);
	}
	
	// Hide the tooltip.
	public void Deactive ()
	{
		
		// Hide the text.
		tooltip.SetActive(false);
	}
	
	// To generate a data string for the Text object.
	public void ConstructDataString()
	{
		
		// The main data string we'll be appending to.
		string data;
		
		// The bullet char.
		char bullet ='\u2022';
		
		// Generate the data, begin with the Title. 
		data = "<b> <color=#0473f0>" + item.Title + "</color> </b> \n\n";
		
		// For stats, etc
		data += bullet.ToString()+" Strength: "+item.GetStat<int>("power")+"\n";
		
		// Item description.
		data += "<color=#8C5948>"+item.Description+"</color>"+"\n";
		
		// Set the data.
		dataTextObject.text = data;
	}
	
}
