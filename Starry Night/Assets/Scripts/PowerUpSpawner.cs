using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] Powerups;

    /// <summary>
    /// Randomly chooses to spawn power up and which power up to spawn. 20% chance of spawning power up and each power up has equal chance of being spawned
    /// </summary>
    public void RollPowerUp(){
        if(Random.Range(0, 100) < 20){
            // Spawn power up
            Instantiate(Powerups[Random.Range(0, Powerups.Length)], transform.position, transform.rotation);
        }
    }
}
