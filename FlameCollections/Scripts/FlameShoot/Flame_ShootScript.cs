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
	/// <summary>
	/// The projectile which we shoot
	/// </summary>
	public GameObject projectile;

	// If we are firing repeatedly
	private bool firing = false;

	public bool Firing
	{
		get
		{
			return firing;
		}

	}

	// If we have paused our firing
	private bool firePaused = false;

	public bool FirePaused
	{
		get
		{
			return firePaused;
		}
	}

	// How long before we can fire again
	public float fireIntervalTime = 0f;

	// Varible counting down until we can fire again
	public float fireIntervalTimeLeft = 0f;

	public bool projectileFire = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (firing && !firePaused)
		{
			fireIntervalTimeLeft -= Time.deltaTime;
			if (fireIntervalTimeLeft <= 0)
			{
				if (projectileFire)
				{
					FireProjectile ();
				}
			}
		}
	}

	/// <summary>
	/// This method fires a projectile at a direction from a point
	/// </summary>
	/// <param name="projectile">The object we launch.</param>
	/// <param name="position">Where we launch it from.</param>
	/// <param name="direction">In what direction we shoot it at.</param>
	/// <param name="force">How much force should be applied to this object.</param>
	public void FireProjectile (Flame_Projectile proj)
	{
		GameObject instProj = (GameObject) Instantiate (proj.projectile, proj.position, proj.rotation);

		Rigidbody rb = instProj.GetComponent <Rigidbody> ();
		if (rb != null)
		{
			rb.AddForce (proj.force, proj.mode);
		} else
		{
			Debug.LogError ("Error: Projectile does not have a rigidbody.");	
		}
	}

	/// <summary> Fires the set projectile </summary>
	private void FireProjectile ()
	{
		GameObject instProj = (GameObject) Instantiate (projectile, transform.position, transform.rotation);
		Rigidbody rb = instProj.GetComponent <Rigidbody> ();
	}  



	/// <summary>
	/// Starts to automatically fire a ray forward from the parent object
	/// </summary>
	/// <param name="fireIntervalTime">How long we wait before each shot</param>
	public void StartFiring (float fireIntervalTime)
	{
		firing = true;
		this.fireIntervalTime = fireIntervalTime;
		this.fireIntervalTimeLeft = fireIntervalTime;
	}


	/// <summary>
	/// Starts to automatically fire a projectile forward from the parent object
	/// </summary>
	///<param name="fireIntervalTime">How long we wait before each shot</param>
	/// <param name="projectile">The projectile that we shoot</param>
	public void StartFiring (float fireIntervalTime, GameObject projectile)
	{
		projectileFire = true;
		this.projectile = projectile;
		StartFiring (fireIntervalTime);
	}

	/// <summary>
	/// Stops the firing
	/// </summary>
	public void StopFiring ()
	{
		firing = false;
		if (projectileFire)
		{
			projectileFire = false;
			this.projectile = null;
		}
	}

	public void PauseFiring ()
	{
		firePaused = true;
	}
}
