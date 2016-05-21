using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Linq;

/*
 * This class adds GetValue to SerializedProperties!
 * Thank you: Johannski and HiddenMonk
 * J: http://answers.unity3d.com/users/296097/johannski.html
 * H: http://answers.unity3d.com/users/496850/hiddenmonk.html
 * See also: http://answers.unity3d.com/questions/627090/convert-serializedproperty-to-custom-class.html
 */

public static class SerializedPropertyExtension
{
	// Implementation pending...
	public static object GetValue(this SerializedProperty property)
	{
		return property.objectReferenceValue;
		//return null;
	}
}
