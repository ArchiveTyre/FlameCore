using UnityEngine;
using System.Collections;

/*
 * This class is applied to a game object with a collider,
 * and is used to keep track of what objects the parent object is
 * colliding with.
*/

public class Flame_CollisionRegistry : MonoBehaviour 
{
	[ShowOnlyAttribute] public ArrayList collisions = new ArrayList(); 

	void OnCollisionEnter (Collision coll)
	{
		// The transform of the collision
		Transform transf = coll.transform;

		// Store collision data in container
		Flame_Collision collision = new Flame_Collision(coll);
		collisions.Add (collision);
	}

	void OnCollisionExit (Collision coll)
	{
		// The transform of the collision
		Transform transf = coll.transform;

		// Store collision data in container
		Flame_Collision collision = new Flame_Collision(coll);

		collisions.Remove (collision);
	}

	// Will search for the collisions with a specific tag and then return an arraylist of Flame_Collision
	public ArrayList GetCollisionsWithTag (string tag)
	{
		ArrayList collisionsWithTag = new ArrayList ();
		foreach (var collObj in collisions)
		{
			Flame_Collision collision = (Flame_Collision) collObj;
			if (collision.tag == tag)
			{
				collisionsWithTag.Add (collision);
			}
		}
		return collisionsWithTag;
	}

	// Returns if we are colliding with an object that has a specific tag
	public bool IsCollidingWithObjectOfTag (string tag)
	{
		return GetCollisionsWithTag (tag).Count > 0;
	}

	// Returns if we are colliding with an object
	public bool IsColliding ()
	{
		return collisions.Count > 0;
	}
}