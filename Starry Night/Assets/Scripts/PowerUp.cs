using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 1f;
    public int powerup;
    public GameObject Player;
    public PlayerData pData;
    public GameObject ShieldSprite;
    public GameObject MoneyEffect;
    public GameObject HealthEffect;
    public GameObject InstaKillEffect;
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
        pData.health = pData.maxHealth;
        // show health effect (Particle effect?)
    }

    private void InstaKill(){
        foreach(GameObject astroid in GameObject.FindGameObjectsWithTag("Astroid")){
            Destroy(astroid);
        }
        foreach(GameObject alien in GameObject.FindGameObjectsWithTag("Alien")){
            Destroy(alien);
        }
        foreach(GameObject alienProj in GameObject.FindGameObjectsWithTag("AlienProjectile")){
            Destroy(alienProj);
        }
        // Flash screen white and fade in
    }

    private void Invinsible(){
        Player.GetComponent<PlayerController>().Invinsible = true;
        Player.GetComponent<PlayerController>().startInvinsible = Time.time;
        GameObject SS = Instantiate(ShieldSprite, transform.position, transform.rotation);
        SS.transform.parent = Player.transform;
    }

    private void Money(){
        pData.money += 100;
        // show money effect (Particle effect?)
    }

    private void Parts(){
        GameManager.Instance.AddItem(GameManager.Instance.levels[GameManager.Instance.pData.level].GetComponent<LevelData>().RandomReward());
    }

    private void WeaponsUpgrade(){
        pData.attack *= 2; // Temporary effect until actual power up has been implemented
        // Add 1 tier to projectiles
        // Each tier increases n by 1 (2^n projectiles)
    }

    private void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
