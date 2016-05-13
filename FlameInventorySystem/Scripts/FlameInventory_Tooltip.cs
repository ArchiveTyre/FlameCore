using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/*
 * Definition:
 * The base class for tooltips. They have two basic methods. Activate and Deactivate. That's it, you are on your own now...
 * History:
 * Created by: Alexander Björkman Friday, May, 13, 2016
 */
[Serializable]
public class FlameInventory_Tooltip : MonoBehaviour
{
	public virtual void Activate(Flame_Item item)
	{
		gameObject.SetActive(true);
		Text text = GetComponentInChildren<Text>();
		text.text = item.title;
	}
	public virtual void Deactivate()
	{
		gameObject.SetActive(false);
	}

	void Start()
	{
		gameObject.SetActive(false);
	}

	void Update()
	{
		// Update position.
		transform.position = Input.mousePosition;
	}
}
