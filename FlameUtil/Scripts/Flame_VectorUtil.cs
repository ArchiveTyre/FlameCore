
/*
 * Core_VectorUtil
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This file is a custom spawner.
 * History:
 * - Created on April 2 2016 By Alexander Björkman
 * - Added RandomPositionBetweenPoints April 2 2015 / Alex
 */

using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Flame_VectorUtil {

	private static System.Random random = new System.Random(123445556);
	
	// Sets the seed of our randomizer.
	public static void SetSeed (string s)
	{
		int i = s.GetHashCode();
		random = new System.Random(i);
	}

	
	// Returns an random position between two points.
	public static Vector3 RandomPositionBetweenPoints (Vector3 a, Vector3 b)
	{
		Vector3 c = new Vector3();
		c.x = (float)random.NextDouble() * (b.x - a.x) + a.x;
		c.y = (float)random.NextDouble() * (b.y - a.y) + a.y;
		c.z = (float)random.NextDouble() * (b.z - a.z) + a.z;
		return c;
	}

	// Returns an random position between two points.
	public static Vector2 RandomPositionBetweenPoints (Vector2 a, Vector2 b)
	{
		Vector2 c = new Vector2();
		c.x = (float)random.NextDouble() * (b.x - a.x) + a.x;
		c.y = (float)random.NextDouble() * (b.y - a.y) + a.y;
		return c;
	}

	// Returns the given vector with all values positive
	public static Vector3 AbsVec (Vector3 v)
	{
		return new Vector3 (Mathf.Abs (v.x), Mathf.Abs (v.y), Mathf.Abs (v.z));
	}

	public static Vector3 AbsVec (Vector2 v)
	{
		return new Vector2 (Mathf.Abs (v.x), Mathf.Abs (v.y));
	}

	// Simply adds up the vectors axises
	public static float Sum (Vector3 v)
	{
		return v.x + v.y + v.z; 
	}

	public static float Sum (Vector2 v)
	{
		return v.x + v.y; 
	}

    // Transform is the object to rotate. And target is the object to look at. 
    // Speed is the speed to rotate. And if 0 then no rotation and if under 0 then instant.
	public static void LookAtWithSpeed(Transform transform, Vector3 target, float speed)
    {

        // No point in continuing!
        if (speed == 0)
        {
            return;
        }

        // Instant speed!
		if (speed < 0)
		{
			transform.LookAt (target);
			return;
		}
        // We have an actual defined speed. So a slower (CPU resocures) custom method is needed.
        else
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed);
            var goalRotation = Quaternion.LookRotation(target - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, Time.deltaTime * speed);
        }
    }

	public static Quaternion LockRotation (Quaternion rot, bool pitch, bool yaw, bool roll)
	{
		Vector3 euler = rot.eulerAngles;
		euler.x = (pitch) ? 0 : euler.x;
		euler.y = (yaw) ? 0 : euler.y;
		euler.z = (roll) ? 0 : euler.z;
		return Quaternion.Euler (euler);
	}

	// checks if a vector is close to another, in a certain range
	public static bool CloseTo (Vector3 current, Vector3 goal, float range)
	{
		float dist = Vector3.Distance (current, goal);
		return dist >= -range && dist <= range;
	}

	public static bool CloseTo (Quaternion current, Quaternion goal, float range)
	{
		Vector3 currentEuler = current.eulerAngles;
		Vector3 goalEuler = current.eulerAngles;
		return CloseTo (currentEuler, goalEuler, range);
	}

	public static Vector3 Normalized (Vector3 v)
	{
		float length = Length (v);
		return new Vector3 (v.x / length, v.y / length, v.z / length);
	}

	public static float Length(Vector3 v)
	{
		return Mathf.Sqrt (v.x * v.x + v.y * v.y + v.z * v.z);
	}

	public static GameObject GetLookObject (float maxDist, Vector3 pos, Vector3 dir)
	{
		// the object that we look at's data
		RaycastHit hit;

		if (Physics.Raycast (pos, dir, out hit, maxDist))
		{
			return hit.transform.gameObject;
		}
		return null;
	}
}


