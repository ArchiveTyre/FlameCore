using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Flame_AttrDisplay : /*MonoBehaviour*/ NetworkBehaviour {

	[SerializeField]
	public Flame_Attr attr = new Flame_Attr();

	public bool isAttrLocal = false;
	public Text text = null;

	public string key;

	[SyncVar, ShowOnly]
	public string value;

	void Update ()
	{
		if (isServer || isAttrLocal)
		if (attr[key].ToString() != value)
			value = attr[key].ToString();
		text.text = value;
	}

}
