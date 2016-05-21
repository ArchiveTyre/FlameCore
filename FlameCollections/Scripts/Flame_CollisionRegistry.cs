using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This class is applied to a game sobject with a collider,
 * and is used to keep track of what objects the parent object is
 * colliding with.
*/

public class Flame_CollisionRegistry : MonoBehaviour 
{

	// The objects that have these tags will not be added to the collisions list, but to the allCollisions list
	[Tooltip("Objects with tags inserted below, will not be inserted into the collisions list, but will be inserted into the allCollisions list")]
	public string[] collisionIgnoreTags;

	// The objects we are currently colliding with that are not in the ignore collision list
	public Dictionary<int, Flame_Collision> collisions = new Dictionary<int, Flame_Collision> ();


	// All objects touching this one
	public Dictionary<int, Flame_Collision> allCollisions = new Dictionary<int, Flame_Collision> ();




	void OnCollisionEnter (Collision coll)
	{
		// The transform of the collision
		//Transform transf = coll.transform;

		// Store collision data in container
		Flame_Collision collision = new Flame_Collision(coll);
	
		allCollisions.Add (collision.gameObject.GetInstanceID (), collision);
		if (!ShouldIgnoreTag (collision.tag))
		{
			collisions.Add (collision.gameObject.GetInstanceID (), collision);
		}
	}

	void OnCollisionExit (Collision coll)
	{

		// Store collision data in container
		Flame_Collision collision = new Flame_Collision(coll);

		if (!ShouldIgnoreTag (collision.tag))
		{
			collisions.Remove (collision.gameObject.GetInstanceID ());
		}
		allCollisions.Remove (collision.gameObject.GetInstanceID ());
	}

	public bool ShouldIgnoreTag (string objTag)
	{
		foreach (string ignoreTag in collisionIgnoreTags)
		{
			if (objTag == ignoreTag)
			{
				return true;
			}
		}
		return false;
	}

	// Will search for the collisions with a specific tag and then return an arraylist of Flame_Collision
	public ArrayList GetCollisionsWithTag (string tag, bool includeAllCollisions)
	{
		ArrayList collisionsWithTag = new ArrayList ();
		Dictionary<int, Flame_Collision> checkCollisions = (includeAllCollisions) ? allCollisions : collisions;
		foreach(KeyValuePair<int, Flame_Collision> kvp in checkCollisions)
		{
			Flame_Collision collision = (Flame_Collision) kvp.Value;
			if (collision.tag == tag)
			{
				collisionsWithTag.Add (collision);
			}
		}
		return collisionsWithTag;
	}

	// Returns if we are colliding with an object that has a specific tag
	public bool IsCollidingWithObjectOfTag (string tag, bool includeAllCollisions)
	{
		return GetCollisionsWithTag (tag, includeAllCollisions).Count > 0;
	}

	// Returns if we are colliding with an object
	public bool IsColliding (bool includeAllCollisins)
	{
		PrintTagsInList ((includeAllCollisins) ? allCollisions : collisions);
		return ((includeAllCollisins) ? allCollisions : collisions).Count > 0;
	}

	void PrintTagsInList (Dictionary<int, Flame_Collision> list)
	{
		string printString = "[";
		foreach(KeyValuePair<int, Flame_Collision> kvp in list)
		{
			Flame_Collision coll = (Flame_Collision)kvp.Value;
			string tag = coll.tag;
			printString += tag + ", ";
		}
		print (printString);
	}



}