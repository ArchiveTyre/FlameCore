/*
 * Core_SpawnedObject
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This components is automaticly added to all objects created by a core spawner.
 * History:
 * - Created on April 2 2016 By Alexander Björkman
 */

using UnityEngine;
using System.Collections;

public class Flame_SpawnedObject : MonoBehaviour {

    [ShowOnly] public Flame_SpawnerBase creator;

	// To keep tract of spawns!
	void Start () {

        // Increase values.
        creator.advancedSettings.aliveSpawnedObjects++;
        creator.advancedSettings.totalSpawns++; // We place it here instead of in the spawner itself to assure that the spawn is actually succesful.
    }
    
    void OnDestroy() {

        // Make sure creator is still alive!
        if (creator)
            creator.advancedSettings.aliveSpawnedObjects--;
    }
}
