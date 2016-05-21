using UnityEngine;
using System.Collections;

/*
 * This is a container class for
 * a collision, storing the gameObject,
 * magnitude, position and rotation.
 * Used by Flame_CollisionRegistry, to store
 * collisions neatly
*/

// TODO: Why doesn't this extend Collision?
public class Flame_Collision 
{

	#pragma warning disable 169, 414

	// The game object we collide with
	public GameObject gameObject;

	// The magnitude of the collision
	public float magnitude;

	// Where we collided
	public Vector3 pointAtContact;

	// What our rotation was when we collided
	public Quaternion rotationAtContact;

	// What the tag of the gameObject is
	public string tag;

	// What the layer of teh gameObjet is
	public int layer;

	// Where we contacted
	public Vector3 wasAtContact;

	// What the transform of the gameObject is
	public Transform transform;

	public Collider collider;

    // TODO: All of these variables should be cloned me thinks.
	public Flame_Collision (Collision coll)
	{
		// Get the properties of the collision
		GameObject gameObject = coll.gameObject;
		float magnitude = coll.impulse.magnitude;
		wasAtContact = coll.transform.position;
		Quaternion rotation = coll.transform.rotation;
        
		this.gameObject = gameObject;
		this.magnitude = magnitude;
        this.pointAtContact = coll.contacts[0].point;
        this.rotationAtContact = rotation;
		this.tag = gameObject.tag;
		this.transform = coll.transform;
		this.layer = gameObject.layer;
		this.collider = coll.collider;
	}
}
