using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Flame_AttrDisplay : NetworkBehaviour {

	Flame_Attr attr;

	public string key;

	[SyncVar]
	public string value;

	// Use this for initialization
	void Start () {
		
	}

	[ClientCallback]
	// Update is called once per frame
	void Update () {

		if (isServer )
		{

		}
	}

}
