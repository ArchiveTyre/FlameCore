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


    // the keybindings to use the correct keys
    public Flame_KeyBindings bindings;
	
	// The avatar that is used in the begining.
	[SerializeField] private GameObject startAvatar;

    // Never ever EVER directly modifie these variables directly. Use the Avatar instead.
    [ShowOnly]
    public GameObject avatar;

    // Rigidbody for the avatar.
    protected Rigidbody avatar_rigidbody;

	// the speed of movement.
	public float movementSpeed = 1f;

    // Camera of avatar.
    public Camera avatarCamera = null;

    // The speed of rotation
    public float rotationSpeed = 1f;

	// Use this to get the Avatar to move. Or set it...
	[HideInInspector] public GameObject Avatar
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


    void SetupBase()
    {
        if (startAvatar == null)
        {
            Debug.LogError("Start Avatar not set");
            startAvatar = gameObject;
        }

        // Set the avatar to the "first" avatar.
        Avatar = startAvatar;
    }

    // THIS METHOD MUST BE CALLED AT THE START METHOD OF ANY CONTROLLER IF IT WANTS TO ACCES Avator or avatar_rigidbody
    void OnEnable()
    {
        SetupBase();
    }
		
}
