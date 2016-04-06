
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
}
