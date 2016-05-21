using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;

[Serializable]
public class Flame_Attr : ICloneable, ISerializationCallbackReceiver
{

	[SerializeField]
	private List<string> _keys = new List<string>();

	[SerializeField]
	private List<string> _values = new List<string>();

	#pragma warning disable 169, 414
	[SerializeField, HideInInspector]
	private string _current_key = "Foo";

	[SerializeField, HideInInspector]
	private string _current_value = "Bar";
	#pragma warning restore 169, 414

	[SerializeField]
	public Dictionary<string, string> content;

	public Flame_Attr()
	{
		content = new Dictionary<string, string>();
	}

	public void OnBeforeSerialize()
	{
		_keys.Clear();
		_values.Clear();
		foreach (var kvp in content)
		{
			_keys.Add(kvp.Key);
			_values.Add(kvp.Value);
		}
	}

	public void OnAfterDeserialize()
	{
		content.Clear();
		for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
			if (_keys[i] != "")
			{
				content[_keys[i]] = _values[i];
			}
			else
			{
				GUI.FocusControl("");
			}
	}

	void OnGUI()
	{

		foreach (var kvp in content)
			GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
	}

	private static string addTwoStringEntries(string a, string b)
	{

		if (a == "true" && b == "false")
		{
			return "true";
		}

		if (b == "true" && a == "false")
		{
			return "true";
		}

		if (b == "true" && a == "true")
		{
			return "true";
		}

		if (b == "false" && a == "false")
		{
			return "false";
		}

		// Try to convert a
		float j;
		float k;

		if (float.TryParse(a, out j) && float.TryParse(b, out k))
		{
			return "" + (j + k);
		}
		else
		{
			return a + b;
		}
	}

	private static string minusTwoStringEntries(string a, string b)
	{

		if (a == "true" && b == "true")
		{
			return "false";
		}

		if (a == "true" && b == "false")
		{
			return "true";
		}

		if (a == "false" && b == "false")
		{
			return "false";
		}

		if (a == "false" && b == "true")
		{
			return "false";
		}

		// Try to convert a
		float j;
		float k;

		if (float.TryParse(a, out j) && float.TryParse(b, out k))
		{
			return (j - k).ToString();
		}
		else
		{
			return a.Replace(b, "");
		}
	}
	public static Flame_Attr operator +(Flame_Attr a1, Flame_Attr a2)
	{
		Flame_Attr a3 = a1.Clone();
		

		foreach(var entry in a1.content)
		{
			string a = "";
			if (a1.content.ContainsKey(entry.Key))
				 a = a1.content[entry.Key];
			string b = "";
			if (a2.content.ContainsKey(entry.Key))
				b = a2.content[entry.Key];
			a3.content[entry.Key] = addTwoStringEntries(a, b);
		}

		return a3;

	}

	public static Flame_Attr operator -(Flame_Attr a1, Flame_Attr a2)
	{
		Flame_Attr a3 = a1.Clone();


		foreach (var entry in a1.content)
		{
			string a = "";
			if (a1.content.ContainsKey(entry.Key))
				a = a1.content[entry.Key];
			string b = "";
			if (a2.content.ContainsKey(entry.Key))
				b = a2.content[entry.Key];
			a3.content[entry.Key] = minusTwoStringEntries(a, b);
		}

		return a3;

	}

	public Flame_Attr Clone()
	{
		Flame_Attr a1 = new Flame_Attr();
		a1.content = new Dictionary<string, string>(this.content.Count, this.content.Comparer);

		foreach (KeyValuePair<string, string> entry in this.content)
		{
			a1.content.Add(entry.Key, (string)entry.Value.Clone());
		}

		return a1;
	}

	object ICloneable.Clone()
	{
		return Clone();
	}
}
