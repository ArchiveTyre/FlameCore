using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Flame_ArrayPicker {

	// Possible ways of iterating,
	[HideInInspector]
	public enum Behaviour
	{
		ROUTE,
		STRAFE,
		LOOP
	};

	[ShowOnly]
	public bool pause = false;
	[HideInInspector]
	public Behaviour behaviour;
	[HideInInspector]
	Array array;

	// What direction to go.
	[HideInInspector]
	public Flame_Enums.MonoDirection direction;

	// What type should this actually be? Quick and dirty implementation.
	public int position;

	public object Pick ()
	{

		// Itiration complete. No need to continue
		if (direction == Flame_Enums.MonoDirection.STOP || pause)
		{
			return null;
		}

		//object target = array[position];
		object target = array.GetValue(position);

		Behave();
		int step = Flame_Enums.EnumToInt(direction);
		position += step;

		// Test
		if (target != null)

			// We succeded
			return target;
		// We failed...
		return null;
	}

	// If we should loop around or etc.
	private void Behave()
	{
		// Get array size.
		int size = array.Length - 1;

		switch (behaviour)
		{
			case Behaviour.ROUTE:
				if (position >= size)
				{
					// End
					direction = Flame_Enums.MonoDirection.STOP;
				}
				break;
			case Behaviour.LOOP:
				if (position >= size)
				{
					// Should we check overlapp.
					position = -1;
				}
				break;
			case Behaviour.STRAFE:
				if (position >= size)
				{
					// End
					direction = Flame_Enums.MonoDirection.BACKWARD;
				}
				else if (position <= 0)
				{
					direction = Flame_Enums.MonoDirection.FORWARD;
				}
		
				break;
		}
	}

	public Flame_ArrayPicker(Array array, Behaviour behaviour)
	{
		this.array = array;
		this.behaviour = behaviour;
	}

}
