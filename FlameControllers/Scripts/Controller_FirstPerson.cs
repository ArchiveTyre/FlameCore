using UnityEngine;
using System.Collections;



public class Controller_FirstPerson : Controller_BaseController
{
	// If we can look walk and jump in the different directions
	public bool lookZAxis = true;
	public bool lookXAxis = true;
	public bool walkZAxis = true;
	public bool walkXAxis = true;
	public bool canJump = true;
	public bool canRun = true;

	// the camera which we will be rotating
	public Camera fpsCam;
	// the keybindings to use the correct keys
	public Controller_KeyBindings bindings;

	// speeds for running and accelerating
	public float runSpeed = 20;
	public float accelerationSpeed = 5;
	public float jumpForce = 10;
	public float currentSpeed = 0;

	[ReadOnlyAttribute] public bool airborne = false; 	// If we are in the air
	[ReadOnlyAttribute] public bool moving = false;	 	// If we are moving, this is walking running or having something move us
	[ReadOnlyAttribute] public bool traveling = false; // If we are walking or running
	[ReadOnlyAttribute] public bool walking = false; 	// If we are walking
	[ReadOnlyAttribute] public bool running = false;		// If we are running

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetInput ();
		Move ();
	}

	// Gets input and checks if we are moving, walking or running
	void GetInput ()
	{
		// reset walking moving travling and runnign
		walking = false;
		moving = false;
		traveling = false;
		running = false;

		float rawHorMoveAmount = (walkXAxis) ? Mathf.Abs (Input.GetAxisRaw (bindings.horizontalAxis)) : 0;
		float rawDepthMoveAmount = (walkZAxis) ? Mathf.Abs (Input.GetAxisRaw (bindings.horizontalAxis)) : 0;
		float rawMoveAmount = rawHorMoveAmount + rawDepthMoveAmount;
		// check if we are moving vertically or horizontally
		if (rawMoveAmount > 0)
		{
			moving = true;

			// check if we are running
			if (Input.GetAxisRaw (bindings.runAxis) == bindings.PRESSED && canRun)
			{
				running = true;
			} else
			{
				walking = true;
			}
		}

		if (rawMoveAmount + Core_VectorUtil.Sum (avatar_rigidbody.velocity) > 0)
		{
			traveling = true;
		}
	}

	void Move ()
	{
		// Reset velocity
		avatar_rigidbody.velocity = new Vector3 (0, avatar_rigidbody.velocity.y, 0);

		// get input
		float hor = Input.GetAxisRaw (bindings.horizontalAxis);
		float ver = Input.GetAxisRaw (bindings.verticalAxis);

		// get the speeds of the different axises and also make sure we can move in that direction
		float horMove = GetSpeed (hor) * Time.deltaTime * (walkXAxis ? 1 : 0);
		float verMove = GetSpeed (ver) * Time.deltaTime * (walkZAxis ? 1 : 0);

		// the vector which we will be moving by
		Vector3 moveVector = new Vector3 (horMove, 0, verMove);

		avatar_rigidbody.MovePosition (Avatar.transform.position + moveVector);
	}

	// Parameter is the result form Input.GetAxisRaw (axis). It lerps the movement to get smooth movement speed
	float GetSpeed (float keyState)
	{
		// what our goal speed is, that is to say what we want our acceleration to be
		float goalSpeed = keyState != 0 ? (running ? runSpeed : movementSpeed) : 0;

		// check that we have not already reached our goal speed
		if (currentSpeed != goalSpeed)
		{
			// lerp between the current speed and the goal speed
			currentSpeed = Mathf.Lerp (currentSpeed, goalSpeed, accelerationSpeed * Time.deltaTime);
		}
		// return the speed * the keystate so that if keyState = -1 we do not move forward, but in the opposite direction
		return currentSpeed * keyState;	
	}
}
