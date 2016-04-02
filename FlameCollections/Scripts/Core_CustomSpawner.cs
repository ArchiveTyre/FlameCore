using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void  Update()
    {

        // Increase counter.
        timePassedSinceSpawn += Time.deltaTime;

        // Check if we are allowed to spawn.
        if (timePassedSinceSpawn > spawnInterval)
        {

            // Spawn the objects.
            GameObject spawn = GameObject.Instantiate(spawnObjects[0]);

            // Make sure is on.
            spawn.SetActive(true);

            // Set a new position between point 1 and 2.
            spawn.transform.position = Core_VectorUtil.RandomPositionBetweenPoints(point1.position, point2.position);

            // Decrease counter.
            timePassedSinceSpawn -= spawnInterval;
        }
    }

}
