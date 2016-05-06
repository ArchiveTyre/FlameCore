using UnityEngine;
using System.Collections;

public static class Flame_ColorExtension {

	public static Color From(this Color other)
	{
		return new Color(other.r, other.g, other.b);
	}
}
