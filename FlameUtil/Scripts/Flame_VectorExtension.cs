using UnityEngine;
using System.Collections;

public static class Flame_VectorExtension  {

	public static Vector3 from(this Vector3 self)
	{
		var v = new Vector3();
		v.x = self.x;
		v.y = self.y;
		v.z = self.z;
		return v;
	}

	public static Vector2 from(this Vector2 self)
	{
		var v = new Vector2();
		v.x = self.x;
		v.y = self.y;
		return v;
	}
}
