using UnityEngine;
using System.Collections;
using System;

public class Quarrier : MonoBehaviour
{
	
	Stack createData = new Stack ();
	
	[SerializeField] private GameObject canvas;
	
	[SerializeField] private GameObject promtPrefab;
	
	private GameObject quarryObj;
	
	/*private*/ /*[SerializeField]*/ private Gui_QuarryManager quarryManager;
	
	private bool doClose = false; 
	
	public void Create (IQuarrier creator ,string titel, string quarrySlug, QuarryItem [] options)
	{
		CreateData data = new CreateData (titel, quarrySlug, options, creator);
		createData.Push ( data ) ;
	}
	
	public string GetTextByReturn (string returnValue)
	{
		return quarryManager.GetTextByReturn (returnValue);
	}
	private void DoCreate ()
	{
		while (createData.Count > 0)
		{
			CreateData createDataLocal = (CreateData)createData.Pop();	
			string titel = createDataLocal.titel;
			string quarrySlug = createDataLocal.quarrySlug;
			QuarryItem [] options = createDataLocal.options;
			IQuarrier creator = createDataLocal.creator;
			
			quarryObj = Instantiate (promtPrefab);
			
			Vector3 topCenter = new Vector3 ((Screen.width / 2),(Screen.height/ 2), 32);
			
			quarryObj.transform.position = topCenter;
			
			quarryObj.SetActive (true);
			
			quarryManager = quarryObj.GetComponent<Gui_QuarryManager>();
			
			//
			quarryManager.creator = creator;
			
			quarryObj.transform.SetParent(canvas.transform);
			
			quarryObj.transform.localScale = promtPrefab.transform.localScale;
			
			quarryManager.DoQuerry(titel, quarrySlug);
			
			
			// For every game object.
			for (int i = 0; i < options.Length; i++)
			{
				quarryManager.AddOption( options[i] );
			}
		}
	}
	
	private class CreateData
	{
		public readonly string titel;
		public readonly string quarrySlug;
		public readonly QuarryItem [] options;
		public readonly IQuarrier creator;
		
		public CreateData (string titel, string quarrySlug, QuarryItem [] options, IQuarrier creator)
		{
			this.titel = titel;
			this.quarrySlug = quarrySlug;
			this.options = options;
			this.creator = creator;
		}
	}
	
	void Update ()
	{
		// First see if close existing.
		if (doClose)
			DoClose();
			
		// Then create.
		DoCreate();
		
	}
	private void DoClose ()
	{
		GameObject.Destroy(quarryObj);
		quarryObj = null;
		doClose = false;
	}
	public void Close ()
	{
		doClose = true;
	}
}

public interface IQuarrier
{
	void OnQuarrier (string quarrySlug, string value);

}

public class QuarryItem 
{
	// The titel.
	public readonly string titel;
	public readonly string returnValue;
		
	public enum Type {
		STRING,
		BUTTON
	};
	public readonly Type type;
	
	public QuarryItem (string titel, string return_value, Type type)
	{
		this.titel = titel;
		this.returnValue = return_value;
		this.type = type;
		
	}
}