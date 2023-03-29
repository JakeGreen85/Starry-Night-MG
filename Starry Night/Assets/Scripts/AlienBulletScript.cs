using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBulletScript : MonoBehaviour
{
    GameObject Player;
    Vector3 target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        target = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
        if(transform.position == target){
            Destroy(this.gameObject);
        }
    }
}
