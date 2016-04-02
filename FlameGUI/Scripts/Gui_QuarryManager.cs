using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Gui_QuarryManager : MonoBehaviour {

	// The prefab used for adding new options.
	[SerializeField] private GameObject optionPrefab; 
	
	[SerializeField] private GameObject inputTextPrefab; 

	// The saved inputs
	private IDictionary<string, GameObject> textInputs = new Dictionary<string, GameObject>();

	private string slug = null;

	// Used for adding options.
	private GameObject areaObject; // Name always "Area"
	
	// The titel of the promt.
	private Text titel; // Name always "Titel"
	
	//Stack for querry options
	Stack quarryOptions = new Stack();

	//private string returnValue = null;

	[HideInInspector] public IQuarrier creator;

	void Update ()
	{
		// Create a querry.
		if (doQuerry)
			DoQuerry ();
			
		// Display all new options.
		AddOption();
		
		
		
			
	}


	// Get some referances.
	void Start ()
	{
		
		// Get ref.
		areaObject = gameObject.transform.FindChild("Area").gameObject;
		
		// Get ref for another ref.
		GameObject titelObject = gameObject.transform.FindChild("Titel").gameObject;
	
		// Get ref.
		titel = titelObject.GetComponent<Text>();
		
		// To asure existense.
		optionPrefab = Instantiate (optionPrefab);
		optionPrefab.transform.SetParent(gameObject.transform);
		optionPrefab.SetActive(false);
		
		// To asure existense.
		inputTextPrefab = Instantiate (inputTextPrefab);
		inputTextPrefab.transform.SetParent(gameObject.transform);
		inputTextPrefab.SetActive(false);
	}

	// Apply click
	public void QuerryClick (string ret)
	{
		print ("CLIIICK!: " + ret);
		// Set our ret value.
		//returnValue = ret;
		return;
	}

	// Function vars
	private string querryStringData;
	private bool doQuerry;
	
	// We have a wrapper in case we call in non game thread.
	public void DoQuerry (string titel, string slug)
	{
		// Set that querrying is needed.
		doQuerry = true;
		querryStringData = titel;
		
		// For mobile peasents!
		//TouchScreenKeyboard.Open("", TouchScreenKeyboardType.ASCIICapable, false, false, false, false);
	}

	// More like reset & init.
	private void DoQuerry()
	{
		// Set that querrying has begun.
		doQuerry = false;
		
		// Destroy sub objects in Area.
		foreach (Transform child in areaObject.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		
		// Set the titel
		this.titel.text = querryStringData;;
	}
	
	public string GetTextByReturn (string returnValue)
	{
		return textInputs[returnValue].GetComponent<InputField>().text;
	}
	
	// TODO: Depricate
	public void AddOption (string titel, string return_value, QuarryItem.Type type)
	{
		// Push the values to stack.
		quarryOptions.Push(new QuarryItem (titel, return_value, type) );
	}
	
	public void AddOption (QuarryItem quarryItem)
	{
		quarryOptions.Push(quarryItem);
	}
	
	private void AddOption ()
	{
		// While there are querries
		while (quarryOptions.Count > 0)
		{
			// Pop the values.
			QuarryItem buttonQuerry = (QuarryItem)quarryOptions.Pop();
			string returnValue = buttonQuerry.returnValue;
			string titel = buttonQuerry.titel;
			QuarryItem.Type type = buttonQuerry.type;
			GameObject option;
			
			// Create the object.
			if (type == QuarryItem.Type.BUTTON)
				option = Instantiate (optionPrefab);
			else
			{
				option = Instantiate (inputTextPrefab);
			}
			option.SetActive (true);
			
			// Set it's parent.
			option.transform.SetParent (areaObject.transform);
		
			// Set it's name to return value.
			option.name = returnValue;
		
			// Set the caller. based on type
			Button button = option.GetComponent<Button>();
			
			if (type == QuarryItem.Type.BUTTON)
				button.onClick.AddListener(() => creator.OnQuarrier (slug ,returnValue));
			else if (type == QuarryItem.Type.STRING)
				// Add so that we can get by name.
				textInputs.Add(returnValue, option);
			// Set the titel.
			option.GetComponentInChildren<Text>().text=titel;
		}
	}
	
	/*private class ButtonQuerry
	{
		// The titel.
		public readonly string titel;
		public readonly string returnValue;
		
		public ButtonQuerry (string titel, string return_value)
		{
			this.titel = titel;
			this.returnValue = return_value;
		}
	}*/
}

