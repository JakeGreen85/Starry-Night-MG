using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidData : MonoBehaviour
{
    public float attack;
    public float health;

    private void Update() {
        if(health <= 0){
            GetComponent<PowerUpSpawner>().RollPowerUp();
            Destroy(this.gameObject);
        }
    }
}