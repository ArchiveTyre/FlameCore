using UnityEngine;
using System.Collections;

public class Flame_KeyBindings : MonoBehaviour 
{
	/// <summary> Use this tag to find the keybindings object in the scene. </summary>
	public const string TAG = "KeyBindings";

	/// <summary> The raw state of an axis when it is pressed </summary>
	public const int PRESSED = 1;

	/// <summary> The raw state of an axis when it is released </summary>
	public const int RELEASED = 0;


	[Header("Key Bindings")]
	// All the key bindings go down here
	[Tooltip("The key we press to jump")]
	public string jumpAxis = "Jump";

	[Tooltip("Horizontal strafing (x)")]
	public string horizontalAxis = "Horizontal";

	[Tooltip("Vertical (z axis) strafing")]
	public string verticalAxis = "Vertical";

	[Tooltip("The button we use to run")]
	public string runAxis = "Run";

	[Tooltip("What we use too look in the x axis with")]
	public string xLook = "Mouse X";

	[Tooltip("What we use too look in the y axis with")]
	public string yLook = "Mouse Y";

	[Tooltip("Third Person rotate yaw")]
	public string tpsYLook = "Horizontal";

	[Tooltip("The button we use to shoot")]
	public string fire = "Fire";

	[Tooltip("Alternative fire button")]
	public string altFire = "Fire2";


	/// <summary>
	///  If you have a keybindings object in the scene with the appropriate tag, use this function to
	/// find it
	///  </summary>
	public static Flame_KeyBindings FindLocalKeyBindings ()
	{
		GameObject bindingsObject = GameObject.FindGameObjectWithTag (TAG);
		Flame_KeyBindings bindings = bindingsObject.GetComponent <Flame_KeyBindings> ();
		if (bindings != null)
		{
			return bindings;
		} else
		{
			Debug.LogError ("ERROR: COULD NOT FIND KEY BINDINGS OBJECT IN SCENE");
			return bindings;
		}
	}

	/// <summary> The raw state of an axis when it is pressed </summary>
	public int Pressed ()
	{
		return Flame_KeyBindings.PRESSED;	
	}

	/// <summary> The raw state of an axis when it is released </summary>
	public int Released ()
	{
		return Flame_KeyBindings.RELEASED;	
	}


}
