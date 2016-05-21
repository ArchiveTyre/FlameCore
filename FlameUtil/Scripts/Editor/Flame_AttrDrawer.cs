using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomPropertyDrawer(typeof(Flame_Attr))]
//[CustomEditor(typeof(Flame_Attr))]
public class Flame_AttrEditor : PropertyDrawer
{

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

			// Create some positions.
			Rect r1 = new Rect(position.xMin + 60f, position.yMax - 40f, (position.width - 30f) / 2f, 20f);
			Rect r2 = new Rect(position.xMin + 60f + (position.width - 30f) / 2f, position.yMax - 40f, (position.width - 30f) / 2f, 20f);
			Rect r3 = new Rect(position.xMin + 60f, position.yMax - 20f, position.width - 30f, 20f);

			Rect a1 = new Rect(position.xMin + 60f, position.yMax - 60f, (position.width - 30f) / 2f, 20f);
			Rect a2 = new Rect(position.xMin + 60f + (position.width - 30f) / 2f, position.yMax - 60f, (position.width - 30f) / 2f, 20f);

			// Create two labels.
			EditorGUI.LabelField(a1, "Key");
			EditorGUI.LabelField(a2, "Value");

			// Get two properties.
			SerializedProperty key = property.FindPropertyRelative("_current_key");
			SerializedProperty value = property.FindPropertyRelative("_current_value");

			// Create two text fields.
			key.stringValue = EditorGUI.TextField(r1, key.stringValue);
			value.stringValue = EditorGUI.TextField(r2, value.stringValue);

			// If "Add Entry" is clicked.
			if (GUI.Button(r3, "Add Entry"))
			{

				// Expand keys and add "key" property to it.
				var keyValue = property.FindPropertyRelative("_keys");
				keyValue.arraySize++;
				var stp = keyValue.GetArrayElementAtIndex(keyValue.arraySize - 1);
				stp.stringValue = key.stringValue;

				// Expand values and add "value" property to it.
				var valValue = property.FindPropertyRelative("_values");
				valValue.arraySize++;
				var stv = valValue.GetArrayElementAtIndex(valValue.arraySize - 1);
				stv.stringValue = value.stringValue;

				// Unselect the button.
				GUI.FocusControl("");
			}
		}
	}
}