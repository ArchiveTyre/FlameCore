using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Flame_GameManager : NetworkManager {

	[SerializeField] public Flame_Enums.GameType type = Flame_Enums.GameType.MULTIPLAYER;

	void Awake()
	{
		if (type == Flame_Enums.GameType.SINGLEPLAYER){
			this.StartHost();
		}
	}
}
