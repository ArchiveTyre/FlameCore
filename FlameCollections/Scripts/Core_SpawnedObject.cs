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

public class Core_SpawnedObject : MonoBehaviour {

    [ShowOnly] public Core_CustomSpawner creator;

	// Use this for initialization
	void Start () {
        creator.aliveSpawnedObjects++;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnDestroy() {
        creator.aliveSpawnedObjects--;
    }
}
