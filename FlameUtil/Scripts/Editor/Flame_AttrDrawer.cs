using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(Flame_Attr))]
public class Flame_AttrEditor : PropertyDrawer
{

	string valText = "Value";
	string keyText = "Key";

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

			valText = EditorGUI.TextField(r2, valText);
			keyText = EditorGUI.TextField(r1, keyText);

			if (GUI.Button(r3,"Add Entry"))
			{
				Flame_Attr attr = fieldInfo.GetValue(property.serializedObject.targetObject) as Flame_Attr;
				attr.content[keyText] = valText;


			}
		}
	}
}