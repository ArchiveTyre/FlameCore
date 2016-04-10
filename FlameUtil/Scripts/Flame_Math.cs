using UnityEngine;
using System.Collections;


public class Flame_Math
{
	public static int Raw (bool b)
	{
		return (b) ? 1 : 0;
	}

	public static int Raw (Object o)
	{
		return (o) ? 1 : 0;
	}

	// Takes a number and returns it in base 360
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
}
