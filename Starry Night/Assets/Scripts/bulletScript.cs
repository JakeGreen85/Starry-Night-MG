using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if(transform.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y){
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("bullet collision");
        if(other.gameObject.CompareTag("Astroid")){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
