using UnityEngine;
using System.Collections;

public static class Flame_TransformExtension{

	public static int GetSelfIndex(this Transform self)
	{
		Transform parent = self.parent;
		int childCount = parent.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (parent.GetChild(i) == self)
			{
				return i;
			}
		}
		return -1;
	}
}
