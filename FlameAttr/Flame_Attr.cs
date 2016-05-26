using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using System.ComponentModel;

[Serializable]
public class Flame_Attr : ICloneable, ISerializationCallbackReceiver
{

	public delegate void DelOnChange(Flame_Attr self);
	public event DelOnChange OnChange;


	[SerializeField]
	private List<string> _keys = new List<string>();

	[SerializeField]
	private List<string> _values = new List<string>();

	private List<Type> _types = new List<Type>();

#if UNITY_EDITOR
	[SerializeField, HideInInspector]
	private string _entry_key = "";

	[SerializeField, HideInInspector]
	private string _entry_value = "";

	#pragma warning disable 169, 414
	[SerializeField, HideInInspector]
	private string _current_key = "Foo";

	[SerializeField, HideInInspector]
	private string _current_value = "Bar";
#endif

	private Dictionary<string, object> content;

	public Flame_Attr()
	{
		content = new Dictionary<string, object>();
	}

	public void OnBeforeSerialize()
	{
		_keys.Clear();
		_values.Clear();
		_types.Clear();
		foreach (var kvp in content)
		{
			_keys.Add(kvp.Key);
			_values.Add(kvp.Value.ToString());
			_types.Add(kvp.Value.GetType());
		}
	}

	public void OnAfterDeserialize()
	{
		content.Clear();
		for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
		{
			if (_keys[i] != "")
			{

				if (_types.Count > i)
				{
					var conv = TypeDescriptor.GetConverter(_types[i]);
					content[_keys[i]] = conv.ConvertFrom(_values[i]);
				}
				else
				{
					content[_keys[i]] = _values[i];
				}
			}
			else
			{
				GUI.FocusControl("");
			}
		}

#if UNITY_EDITOR
		// Check if we should add a new entry!
		if (_entry_key != "")
		{

			// Convert to the right type...
			double d = 0f;
			bool b = false;

			if (double.TryParse(_entry_value, out d))
				content[_entry_key] = d;
			else if (bool.TryParse(_entry_value, out b))
				content[_entry_key] = b;
			else
				content[_entry_key] = _entry_value;

			// Reset
			_entry_key = "";
		}
#endif
	}

	public Dictionary<string, object>  GetContent()
	{
		if (OnChange != null)
			OnChange(this);
		return content;
	}

	public object this[string index]
	{

		// Read one byte at offset index and return it.
		get
		{
			return content[index];
		}

		set
		{
			if (OnChange != null && content[index] != value)
				OnChange(this);
			content[index] = value;
			
		}
	}

	public int Count
	{
		get
		{
			return content.Count;
		}
	}

	void OnGUI()
	{

		foreach (var kvp in content)
			GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
	}

	/*private static object DetectAndConvert(string stringValue)
	{
		var expectedTypes = new List<Type> { typeof(DateTime),typeof(float), typeof(int), typeof(bool), typeof(string) };
		foreach (var type in expectedTypes)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter.CanConvertFrom(typeof(string)))
			{
				try
				{
					// You'll have to think about localization here
					object newValue = converter.ConvertFromInvariantString(stringValue);
					if (newValue != null)
					{
						return newValue;
					}
				}
				catch
				{
					// Can't convert given string to this type
					continue;
				}

			}
		}

		return null;
	}*/
	
	private static object AddTwoObjects(object a, object b)
	{
		if (a is string & b != null)
		{

			return a as string + b.ToString();
		}
		if (a is bool & b is bool)
		{
			return (bool)a | (bool)b;
		}
		if (a.IsNumericType() & b.IsNumericType())
		{
			double x = Convert.ToDouble(a);
			double y = Convert.ToDouble(b);
			return x + y;
		}
		return null;
	}

	private static object MinusTwoObjects(object a, object b)
	{
		if (a is string & b != null)
		{

			return (a as string).Replace(b.ToString(), "");
		}
		if (a is bool & b is bool)
		{
			int x = (bool)a ? 1 : 0;
			int y = (bool)b ? 1 : 0;

			if (x > y)
				return true;
			else
				return false;
		}
		if (a.IsNumericType() & b.IsNumericType())
		{
			double x = Convert.ToDouble(a);
			double y = Convert.ToDouble(b);
			return x - y;
		}
		return null;
	}

	public static Flame_Attr operator +(Flame_Attr a1, Flame_Attr a2)
	{
		Flame_Attr a3 = a1.Clone();
		

		foreach(var entry in a1.content)
		{
			object a = null;
			if (a1.content.ContainsKey(entry.Key))
				 a = a1.content[entry.Key];
			object b = null;
			if (a2.content.ContainsKey(entry.Key))
				b = a2.content[entry.Key];
			a3.content[entry.Key] = AddTwoObjects(a, b);
		}

		return a3;

	}

	public static Flame_Attr operator -(Flame_Attr a1, Flame_Attr a2)
	{
		Flame_Attr a3 = a1.Clone();


		foreach (var entry in a1.content)
		{
			object a = null;
			if (a1.content.ContainsKey(entry.Key))
				a = a1.content[entry.Key];
			object b = null;
			if (a2.content.ContainsKey(entry.Key))
				b = a2.content[entry.Key];
			a3.content[entry.Key] = MinusTwoObjects(a, b);
		}

		return a3;

	}

	public Flame_Attr Clone()
	{
		Flame_Attr a1 = new Flame_Attr();
		a1.content = new Dictionary<string, object>(this.content.Count, this.content.Comparer);

		foreach (KeyValuePair<string, object> entry in this.content)
		{
			if (entry.Value != null && entry.Value is ICloneable)
				a1.content.Add(entry.Key, (entry.Value as ICloneable).Clone());
			else
				a1.content.Add(entry.Key, entry.Value);
		}

		return a1;
	}

	object ICloneable.Clone()
	{
		return Clone();
	}
}
