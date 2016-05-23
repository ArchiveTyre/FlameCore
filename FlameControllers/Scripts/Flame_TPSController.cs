using UnityEngine;
using System.Collections;
using FlameCore.Controllers;

public class Flame_TPSController : Flame_3DBaseController
{
	// If we can look walk and jump in the different directions
	public bool lookXAxis = true;
	public bool walkZAxis = true;
	public bool walkXAxis = true;
	public bool canJump = true;
	public bool canRun = true;

	// speeds for running and accelerating
	public float runSpeed = 20;
	public float accelerationSpeed = 5;
	public float jumpForce = 10;
	public float rotateSpeed = 5;

	[ShowOnlyAttribute] public float currentSpeed = 0;

	[ShowOnlyAttribute] public bool airborne = false; 	// If we are in the air
	[ShowOnlyAttribute] public bool moving = false;	 	// If we are moving, this is walking running or having something move us
	[ShowOnlyAttribute] public bool traveling = false; // If we are walking or running
	[ShowOnlyAttribute] public bool walking = false; 	// If we are walking
	[ShowOnlyAttribute] public bool running = false;		// If we are running


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move ();
		Look ();
	}

	void Move ()
	{
		
	}

	void Look ()
	{
		
	}


}
