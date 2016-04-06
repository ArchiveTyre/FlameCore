using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Core_CustomSpawner
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This file is a custom spawner.
 * History:
 * - Created on April 2 2016 By Alexander Björkman
 */

public class Core_CustomSpawner : MonoBehaviour {

    // Set a header.
    [Header("Spawner Settings")] 
    /* Everything down below are the simple configurations. */

    // First positions.
    [Tooltip("Position 1")]
    public Transform point1;

    // Second position.
    [Tooltip("Position 2")]
    public Transform point2;

    // The spawn interval.
    [Tooltip("How many seconds between each spawn")]
    public float spawnInterval = 1;

    // Objects to spawn.
    [Tooltip("What objects to spawn")]
    public List<GameObject> spawnObjects = new List <GameObject>();

    // Time passed since the last spawn in seconds.
    [HideInInspector]
    public float timePassedSinceSpawn = 0f;

    // Objects that are alive that this has spawned.
    [ShowOnly] public int aliveSpawnedObjects = 0;
    [ShowOnly] [SerializeField] private int totalSpawns = 0;

    // Advanced settings
    [System.Serializable]
    class AdvancedSettings
    {
        [Tooltip("The max amount of alive objects this spawner can have at the time. If negativ then infinite")]
        public int maxSpawnsAliveAtTheTime = -1;
        public int maxTotalSpawns = -1;
    }

    [Tooltip("Advanced settings for spawner")]
    [SerializeField]
    private AdvancedSettings advancedSettings = null;

    // Use this for initialization
    void Start()
    {
    
    }

    // Update is called once per frame
    void  Update()
    {

        // Either we don't have a max amount or we have to have less or equals than the max amount. 
        if ( (advancedSettings.maxSpawnsAliveAtTheTime < 0 || advancedSettings.maxSpawnsAliveAtTheTime > aliveSpawnedObjects) && (advancedSettings.maxTotalSpawns < 0 || advancedSettings.maxTotalSpawns > totalSpawns) )
        {
            // Increase counter if we need to.
            timePassedSinceSpawn += Time.deltaTime;

            // Check if we are allowed to spawn.
            if (timePassedSinceSpawn > spawnInterval)
            {

                // Spawn the objects.
                GameObject spawn = GameObject.Instantiate(spawnObjects[0]);

                Core_SpawnedObject obj = spawn.AddComponent<Core_SpawnedObject>();
                obj.creator = this;


                // Make sure is on.
                spawn.SetActive(true);

                // Set a new position between point 1 and 2.
                spawn.transform.position = Core_VectorUtil.RandomPositionBetweenPoints(point1.position, point2.position);

                // Increase the total spawned counter.
                totalSpawns++;
                // Decrease counter.
                timePassedSinceSpawn -= spawnInterval;
            }
        }  
    }

}
