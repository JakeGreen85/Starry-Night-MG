using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Touch touch;
    public GameObject bullet;
    float lastBullet = 0f;
    float fireRate = 0.5f;
    float moveSpeed = 10f;
    float ypos = -8f;
    public bool Invinsible = false;
    public float startInvinsible;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ChangeShip(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentState != GameManager.GameState.GameRunning) return;
        GetTouch();
        if(Invinsible && Time.time > startInvinsible + 3f){
            Invinsible = false;
            Destroy(GameObject.FindGameObjectWithTag("Shield"));
        }
        if(Time.time > lastBullet + fireRate){
            lastBullet = Time.time;
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    private void GetTouch(){
        if(Input.touchCount > 0){
            foreach(Touch t in Input.touches){
                // Move towards first touch
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0), moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(Invinsible) return;
        if(other.gameObject.CompareTag("Astroid") || other.gameObject.CompareTag("AlienProjectile")){
            Destroy(other.gameObject);
            GetComponent<PlayerData>().health -= other.gameObject.GetComponent<HostileData>().attack;
            if(GetComponent<PlayerData>().health <= 0){
                // var ps = GetComponentInChildren<ParticleSystem>();
                // ps.Play();
                GameManager.Instance.LevelOver(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("PowerUp")){
            other.GetComponent<PowerUp>().GivePowerUp(this.gameObject);
        }
    }

    public void ChangeSprite(GameObject newSprite){
        GameObject SS = Instantiate(newSprite, transform.position, transform.rotation);
        SS.transform.parent = transform;
        // Should simply subtract stats from other ship and add stats from this ship to keep stats from turrets equipped
        GetComponent<PlayerData>().attack = SS.GetComponent<SpaceShipStats>().attack;
        GetComponent<PlayerData>().health = SS.GetComponent<SpaceShipStats>().health;
        GetComponent<PlayerData>().maxHealth = SS.GetComponent<SpaceShipStats>().maxHealth;
    }
}
