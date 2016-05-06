using UnityEngine;
using System.Collections;

public class Flame_Projectile
{
	public GameObject projectile;
	public Vector3 position;
	public Vector3 direction;
	public Quaternion rotation;
	public Vector3 force;
	public ForceMode mode = ForceMode.Impulse;

	public Flame_Projectile (GameObject projectile, Vector3 position, Vector3 direction, Quaternion rotation, Vector3 force)
	{
		Initialize (projectile, position, direction, rotation, force);
	}

	public Flame_Projectile (GameObject projectile, Vector3 position, Vector3 direction, Quaternion rotation, Vector3 force, ForceMode mode)
	{
		Initialize (projectile, position, direction, rotation, force);
		this.mode = mode;
	}


	private void Initialize (GameObject projectile, Vector3 position, Vector3 direction, Quaternion rotation, Vector3 force)
	{
		this.projectile = projectile; 
		this.position = position;
		this.rotation = rotation;
		this.direction = direction;
		this.force = force;
	}


}
