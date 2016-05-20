using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomPropertyDrawer(typeof(Flame_Attr))]
//[CustomEditor(typeof(Flame_Attr))]
public class Flame_AttrEditor : PropertyDrawer
{

	//string valText = "Foo";
	//string keyText = "Bar";

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		if (property.isExpanded)
			return EditorGUI.GetPropertyHeight(property) + 80f;
		return EditorGUI.GetPropertyHeight(property);
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{

		EditorGUI.PropertyField(position, property, label, true);
		if (property.isExpanded)
		{
			Rect r1 = new Rect(position.xMin + 60f, position.yMax - 40f, (position.width - 30f) / 2f, 20f);
			Rect r2 = new Rect(position.xMin + 60f + (position.width - 30f) / 2f, position.yMax - 40f, (position.width - 30f) / 2f, 20f);
			Rect r3 = new Rect(position.xMin + 60f, position.yMax - 20f, position.width - 30f, 20f);

			Rect a1 = new Rect(position.xMin + 60f, position.yMax - 60f, (position.width - 30f) / 2f, 20f);
			Rect a2 = new Rect(position.xMin + 60f + (position.width - 30f) / 2f, position.yMax - 60f, (position.width - 30f) / 2f, 20f);

			EditorGUI.LabelField(a1, "Key");
			EditorGUI.LabelField(a2, "Value");

			property.FindPropertyRelative("_current_key").stringValue = EditorGUI.TextField(r1, property.FindPropertyRelative("_current_key").stringValue);
			property.FindPropertyRelative("_current_value").stringValue = EditorGUI.TextField(r2, property.FindPropertyRelative("_current_value").stringValue);

			if (GUI.Button(r3, "Add Entry"))
			{
				var keyValue = property.FindPropertyRelative("_keys");
				keyValue.arraySize++;
				var stp = keyValue.GetArrayElementAtIndex(keyValue.arraySize - 1);
				stp.stringValue = property.FindPropertyRelative("_current_key").stringValue;

				var valValue = property.FindPropertyRelative("_values");
				valValue.arraySize++;
				var stv = valValue.GetArrayElementAtIndex(valValue.arraySize - 1);
				stv.stringValue = property.FindPropertyRelative("_current_value").stringValue;
				GUI.FocusControl("");

			}
		}
	}
}