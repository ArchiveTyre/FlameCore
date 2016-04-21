using UnityEngine;
using System.Collections;


public class Flame_Math
{
	public const int RAW_TRUE = 1;
	public const int RAW_FALSE = 0;
	public const int RAW_EXIST = 1;
	public const int RAW_NULL = 0;


	// get integer value of a boolean (1 = true, 0 = false), used for comparison and operations
	public static int Raw (bool b)
	{
		return (b) ? 1 : 0;
	}

	// get integer value of an integer (1 = not null, 0 = null), used for comparison and operations
	public static int Raw (Object o)
	{
		return (o) ? 1 : 0;
	}

	// Takes a number and returns it in base 360 (0 - 359)
	public static float Base360 (float n)
	{
		while (n >= 360)
		{
			n -= 360;
		} 
		while (n <= 0)
		{
			n += 360;
		}

		return n;
	}

	// Check if a float is close enough to another in a provided ragne
	public static bool CloseTo (float current, float goal, float range)
	{
		return Mathf.Abs (goal - current) < range;
	}
}
