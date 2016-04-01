using UnityEngine;
using System.Collections;

public class Core_VectorUtil {

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
	
}
