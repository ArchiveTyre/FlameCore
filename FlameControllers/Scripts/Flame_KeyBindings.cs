using UnityEngine;
using System.Collections;

public class Flame_KeyBindings : MonoBehaviour 
{

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

	// the raw state of an axis when it is pressed
	[HideInInspector] public int PRESSED = 1;

	// the raw state of an axis when it is released
	[HideInInspector] public int RELEASED = 0;

}
