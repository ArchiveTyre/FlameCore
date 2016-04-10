using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Flame_CustomSpawner
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This file is a custom spawner with position
 
    
    .
 * History:
 * - Created on April 2 2016 By Alexander Björkman
 */

public class Flame_CustomSpawner : Flame_SpawnerBase {

    // First positions.
    [Tooltip("Position 1")]
    public Transform point1;

    // Second position.
    [Tooltip("Position 2")]
    public Transform point2;

    // Update is called once per frame
    void  Update()
    {

        // Either we don't have a max amount or we have to have less or equals than the max amount. 
        if (IsEmptyEnoughForSpawn())
        {
            // Increase counter if we need to.
            advancedSettings.timePassedSinceSpawn += Time.deltaTime;

            GameObject spawn = DoSpawn();

            if (spawn) // If we managed to spawn something.
            {
                // Set a new position between point 1 and 2.
                spawn.transform.position = Flame_VectorUtil.RandomPositionBetweenPoints(point1.position, point2.position);
            }

        }  
    }

}
