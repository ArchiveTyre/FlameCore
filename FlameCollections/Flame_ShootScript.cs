using UnityEngine;
using System.Collections;

/*
 * Flame_ShootScript
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This file is used for shooting stuff... Yea!
 * - This class is not meant to be extendable.
 * History:
 * - Created on April 7 2016 By Alexander Björkman
 */

public class Flame_ShootScript : Flame_SpawnerBase 
{
	[Tooltip("If you are using projectile shooting you must use a projectile game object. Any object with a rigidbody will do. Leave empty if you do not wish to use projectile shooting")]
	public GameObject mainProjectile;
	public GameObject altProjectile;

	[Tooltip("The force of the projectile")]
	public Vector3 mainProjectileForce = new Vector3(0, 0, 10f);
	public Vector3 altProjectileForce = new Vector3(0, 0, 10f);

	[Tooltip("If we can fire")]
	public bool mainFire = true;
	public bool altFire = false;

	[Tooltip("What the length of our shoot ray is")]
	public float mainRayLength = 10f;
	public float altRayLength = 10f;

	[Tooltip("If we can hold down the shootbutton to rapid shoot")]
	public bool mainAutomaticFire = false;
	public bool altAutomaticFire = false;

	[Tooltip("If automatic fire is enabled, the time between each shot, otherwise the time before you can shoot again")]
	public float mainFireTime = 0.3f;
	public float altFireTime = 0.3f;




	[ShowOnlyAttribute] public bool mainFiring = false;
	[ShowOnlyAttribute] public bool altFiring = false;
		
	public bool cooldown = false;
	private float cooldownTime = 0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		cooldownTime -= Time.deltaTime;
		fire
		if (mainProjectile != null && mainFire)
		{
			FireProjectile(mainProjectile, mainProjectileForce, mainAutomaticFire, mainFireTime)
		}
	}

	public void FireProjectile (GameObject projectile, Vector3 ForceMode, bool autoFire, float fireTime, bool firing, string fireAxis)
	{
		if (Input.GetAxisRaw (fireAxis) == 1))
		{
			if 
		}
	}

	public void SetCooldown(float time)
	{
		cooldown = true;
		cooldownTime = time;
	}
}
