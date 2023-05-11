using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollected : MonoBehaviour
{
    private float StartTime;
    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up*3f*Time.deltaTime);
        if(Time.time > StartTime+2f){
            Destroy(this.gameObject);
        }
    }
}
