using UnityEngine;
using System.Collections;

/*
 * This controller is a simple first person controller,
 * with adjustable speed, look and move axises, acceleration and etc. It can 
 * also Jump and run, and has information such as wether it is moving, in the air, walking, running
 * turning traveling etc.
 * 
 * TODO: Work on Looking, Jumping and Running. Also head bob?!
 * Work smoother movement, kind of jerky now
 * Smoth look option?!
*/


public class Flame_FPSController : Flame_BaseController
{
	// If we can look walk and jump in the different directions
	public bool lookYAxis = true;
	public bool lookXAxis = true;
	public bool walkZAxis = true;
	public bool walkXAxis = true;
	public bool canJump = true;
	public bool canRun = true;

	// The camera which we will be rotating
	public Camera fpsCam;
	// the keybindings to use the correct keys
	public Flame_KeyBindings bindings;

	// Speeds for running and accelerating
	public float runSpeed = 20;
	public float accelerationSpeed = 5;
	public float jumpForce = 10;
	public float currentSpeed = 0;
	public float lookSpeed = 10;

	// Rotation constraints in the x axis (pitch)
	public float pitchMax = 360;
	public float pitchMin = 0;


	[ShowOnlyAttribute] public bool airborne = false; 	// If we are in the air
	[ShowOnlyAttribute] public bool moving = false;	 	// If we are moving, this is walking running or having something move us
	[ShowOnlyAttribute] public bool traveling = false; // If we are walking or running
	[ShowOnlyAttribute] public bool walking = false; 	// If we are walking
	[ShowOnlyAttribute] public bool running = false;		// If we are running
	[ShowOnlyAttribute] public bool turning = false;	// if we are moving our mouse to turn/look

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetInput ();
		Move ();
		Look ();
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
			traveling = true;
		}

		if (rawMoveAmount + Flame_VectorUtil.Sum (avatar_rigidbody.velocity) > 0)
		{
			moving = true;
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
		Vector3 moveVector = horMove * Avatar.transform.right + verMove * Avatar.transform.forward;//new Vector3 (horMove, 0, verMove);

		// where we want to move to 
		Vector3 goalPos = Avatar.transform.position + moveVector;

		// move to that destination
		avatar_rigidbody.MovePosition (goalPos);

	}

	void Look ()
	{
		float hor = Input.GetAxis (bindings.xLook) * lookSpeed * Flame_Math.Raw (lookXAxis);
		float ver = Input.GetAxis (bindings.yLook) * lookSpeed * Flame_Math.Raw (lookYAxis);

		fpsCam.transform.rotation = Quaternion.Euler (fpsCam.transform.rotation.eulerAngles.x, fpsCam.transform.rotation.eulerAngles.y, 0);


		fpsCam.transform.Rotate (-ver, 0, 0);
		avatar.transform.Rotate (0, hor, 0);



	
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

	public GameObject GetLookObject (float maxDist)
	{
		// the object that we look at's data
		RaycastHit hit;

		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, maxDist))
		{
			return hit.transform.gameObject;
		}
		return null;
	}
}
