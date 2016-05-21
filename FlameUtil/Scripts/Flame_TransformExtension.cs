using UnityEngine;
using System.Collections;

public static class Flame_TransformExtension{

	public static int GetSelfIndex(this Transform self)
	{
		Transform parent = self.parent;
		int childCount = parent.childCount;
		for (int index = 0; index < childCount; index++)
		{
			if (parent.GetChild(index) == self)
			{
				return index;
			}
		}
		return -1;
	}
}
