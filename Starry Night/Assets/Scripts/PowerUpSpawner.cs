using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] Powerups;
    public void RollPowerUp(){
        if(Random.Range(0, 100) < 20){
            // Spawn power up
            Instantiate(Powerups[Random.Range(0, Powerups.Length)], transform.position, transform.rotation);
        }
    }
}
