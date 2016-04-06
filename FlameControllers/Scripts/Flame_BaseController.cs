using UnityEngine;
using System.Collections;

/*
 * Controller_ClickType
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This class is the base of all classes. 
 * - It can be used for global configurations 
 * - like 2 different types of controlls with the 
 * - same avatar and speed.
 * History:
 * - Created on April 2 2016 By Alexander Björkman
 */

public class Flame_BaseController : MonoBehaviour {

	// Never ever EVER directly modifie these variables directly. Use the Avatar instead.
	[ShowOnly] public GameObject avatar;
	
	// The avatar that is used in the begining.
	[SerializeField] private GameObject startAvatar; 
	
	// Rigidbody for the avatar.
	protected Rigidbody avatar_rigidbody;

	// the speed of movement.
	public float movementSpeed = 1f;

    public float rotationSpeed = 1f;

	// Use this to get the Avatar to move. Or set it...
	public GameObject Avatar
	{
		get
		{
			return avatar;
		}
		set
		{
			avatar = value;
			avatar_rigidbody = avatar.GetComponent<Rigidbody>();
		}
	}

	


	// THIS METHOD MUST BE CALLED AT THE START METHOD OF ANY CONTROLLER IF IT WANTS TO ACCES Avator or avatar_rigidbody

	void OnEnable ()
	{
		if (startAvatar == null)
		{
			Debug.LogError("Start Avatar not set");
			startAvatar = gameObject;
		}

		// Set the avatar to the "first" avatar.
		Avatar = startAvatar;
	}
}
