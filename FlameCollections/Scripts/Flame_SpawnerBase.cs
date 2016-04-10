using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Don't use this file unless you are a expert developer or a FlameCore developer!

/*
 * Flame_SpawnerBase
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This file is a base class for spawners.
 * - Use it to create things that can spawn things conditionally.
 * History:
 * - Created on April 7 2016 By Alexander Björkman
 */

public class Flame_SpawnerBase : MonoBehaviour {

	// Set a header.
	[Header("Spawner Settings.")]
	/* Everything down below are the simple configurations. */

	// The spawn interval.
	[Tooltip("How many seconds between each spawn.")]
	public float spawnInterval = 1;

	// Objects to spawn.
	[Tooltip("What objects to spawn.")]
	public List<GameObject> spawnObjects = new List<GameObject>();

	/* Here are the advanced settings */
	[System.Serializable]
	public class AdvancedSettingsAndData
	{
		[Tooltip("The max amount of alive objects this spawner can have at the time. If negativ then infinite.")]
		public int maxSpawnsAliveAtTheTime = -1;
		public int maxTotalSpawns = -1;
		[Tooltip("If we should automaticly spawn objects as active on spawn.")]
		public Flame_Enums.UndefinedPolar setAllSpawnsAsActive = Flame_Enums.UndefinedPolar.UNDEFINED ;

		// Time passed since the last spawn in seconds.
		[Tooltip("How much time has passed since last spawn in seconds.")]
		public float timePassedSinceSpawn = 0f;

		// Objects that are alive that this has spawned.
		[ShowOnly]
		[Tooltip("How many objects this spawner has spawned that are still alive.")]
		public int aliveSpawnedObjects = 0;

		// The amount of spawns this object has done.
		public int totalSpawns = 0;
	}

	[Tooltip("Advanced settings for spawner.")]
	// Name could be more like advancedSettingsAndData. But we can't change the name to only settings in the inspector.
	public AdvancedSettingsAndData advancedSettings = null;

	public bool CanSpawn()
	{
		if ((advancedSettings.maxSpawnsAliveAtTheTime < 0
			|| advancedSettings.maxSpawnsAliveAtTheTime > advancedSettings.aliveSpawnedObjects)

			&& (advancedSettings.maxTotalSpawns < 0
				|| advancedSettings.maxTotalSpawns > advancedSettings.totalSpawns))
		{
			return true;
		}
		return false;
	}

	// Should we have this protected or public. Alternativly have both with an wrapper function.
	protected GameObject DoSpawn()
	{

		// Check if we are allowed to spawn.
		if (advancedSettings.timePassedSinceSpawn >= spawnInterval
			&& advancedSettings.maxTotalSpawns >= advancedSettings.totalSpawns)
		{
			// Spawn the objects.
			GameObject spawn = GameObject.Instantiate(spawnObjects[0]);

			Flame_SpawnedObject obj = spawn.AddComponent<Flame_SpawnedObject>();

			obj.creator = this;

			if (!(advancedSettings.setAllSpawnsAsActive == Flame_Enums.UndefinedPolar.UNDEFINED))
			{
				spawn.SetActive(Flame_Enums.EnumToBool(advancedSettings.setAllSpawnsAsActive));
			}

			// Decrease counter.
			advancedSettings.timePassedSinceSpawn -= spawnInterval;

			return spawn;
		}
		else
		{
			return null;
		}
	}

}