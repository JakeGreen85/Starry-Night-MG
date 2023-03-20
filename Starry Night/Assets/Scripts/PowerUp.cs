using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 1f;
    public int powerup;
    public void GivePowerUp(GameObject player){
        PlayerData pData = player.GetComponent<PlayerData>();
        Destroy(this.gameObject);
        switch(powerup){
            case 1:
                HealthPowerUp(pData);
                break;
            case 2:
                InstaKill();
                break;
            case 3:
                Money(pData);
                break;
            case 4:
                Invinsible(player);
                break;
            case 5:
                Parts();
                break;
            case 6:
                WeaponsUpgrade(pData);
                break;
            default:
                break;
        }
    }

    private void HealthPowerUp(PlayerData pData){
        pData.health += 50;
    }

    private void InstaKill(){
        foreach(GameObject astroid in GameObject.FindGameObjectsWithTag("Astroid")){
            Destroy(astroid);
        }
    }

    private void Invinsible(GameObject player){ // FIX
        player.GetComponent<BoxCollider2D>().enabled = false; // CANT PICK UP ANYTHING
    }

    private void Money(PlayerData pData){
        pData.money += 100;
    }

    private void Parts(){
        // Give parts to player inventory
    }

    private void WeaponsUpgrade(PlayerData pData){
        pData.attack *= 2;
    }

    private void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
