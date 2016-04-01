using UnityEngine;
using System.Collections;

/*
 * This class is the base of all classes. 
 * It can be used for global configurations 
 * like 2 different types of controlls with the 
 * same avatar and speed.
 */
public class Controller_BaseController : MonoBehaviour {

	// Never ever EVER directly modifie these variables directly. Use the Avatar instead.
	[ReadOnlyAttribute] public GameObject avatar;
	
	// The avatar that is used in the begining.
	[SerializeField] private GameObject startAvatar = this.g; 
	
	// Rigidbody for the avatar.
	private Rigidbody avatar_rigidbody;

	// the speed of movement.
	public float movementSpeed = 1f;
	
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
	
	void Start ()
	{
		
		// Set the avatar to the "first" avatar.
		Avatar = startAvatar;
	}
}
