using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 1f;
    public int powerup;
    public GameObject Player;
    public PlayerData pData;
    public void GivePowerUp(GameObject player){
        pData = player.GetComponent<PlayerData>();
        Player = player;
        Destroy(this.gameObject);
        switch(powerup){
            case 1:
                HealthPowerUp();
                break;
            case 2:
                InstaKill();
                break;
            case 3:
                Money();
                break;
            case 4:
                Invinsible();
                break;
            case 5:
                Parts();
                break;
            case 6:
                WeaponsUpgrade();
                break;
            default:
                break;
        }
    }

    private void HealthPowerUp(){
        pData.health += 50;
    }

    private void InstaKill(){
        foreach(GameObject astroid in GameObject.FindGameObjectsWithTag("Astroid")){
            Destroy(astroid);
        }
    }

    private void Invinsible(){
        Player.GetComponent<PlayerController>().Invinsible = true;
        Player.GetComponent<PlayerController>().startInvinsible = Time.time;
    }

    private void Money(){
        pData.money += 100;
    }

    private void Parts(){
        // Give parts to player inventory
    }

    private void WeaponsUpgrade(){
        pData.attack *= 2;
    }

    private void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
