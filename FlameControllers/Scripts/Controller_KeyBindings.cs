using UnityEngine;
using System.Collections;

public class Controller_KeyBindings : MonoBehaviour 
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

	// the raw state of an axis when it is pressed
	[HideInInspector] public int PRESSED = 1;

	// the raw state of an axis when it is released
	[HideInInspector] public int RELEASED = 0;

}
