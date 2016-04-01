using UnityEngine;
using System.Collections;

// TODO: Cooler name XD

public class Controller_ClickType : Controller_BaseController { 
	
	// The object that will show where the avatar is headed.
	public GameObject destinationCursor;
	
	void Update () {
		if (Input.GetMouseButton(0))
		{
			// Enable the destination cursor
			destinationCursor.SetActive(true);
			
			// Create raycast.
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))
			{
				
				// Only if we have not reached our desination.
				if (hit.transform.gameObject != avatar.transform.gameObject)
				{
					// Where we clicked. Later set. At first null.
					Vector3 mapClick;
					mapClick = hit.point;
					destinationCursor.transform.position = mapClick;
					avatar.transform.LookAt(mapClick);
					float step = movementSpeed * Time.deltaTime;
					avatar.transform.position = Vector3.MoveTowards(avatar.transform.position, mapClick , step);
				}
			}
		
			
		}
		else
		{
			
			// Disable destination cursor.
			destinationCursor.SetActive(false);
		}
		
	}
}
