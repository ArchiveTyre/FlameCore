using UnityEngine;
using System.Collections;

/*
 * Controller_ClickType
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This file is an implementation of the click type of controllers where you click the way you want to go.
 * History:
 * - Created on April 2 2016 By Alexander Björkman
 */


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
                    Vector3 mapClick = hit.point;

                    // Move the destination cursor.
                    destinationCursor.transform.position = mapClick;

                    // Calculate a distance to move with.
                    float step = movementSpeed * Time.deltaTime;
                    float rotationStep = rotationSpeed * Time.deltaTime;

                    // Rotate.
                    Core_VectorUtil.LookAtWithSpeed(transform, mapClick, rotationStep);

                    // Move.
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
