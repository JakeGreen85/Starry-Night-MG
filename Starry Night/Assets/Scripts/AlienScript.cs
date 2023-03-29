using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    GameObject Player;
    public GameObject bullet;
    float lastShot;
    public float attackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lastShot + attackSpeed){
            Shoot();
            lastShot = Time.time;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
