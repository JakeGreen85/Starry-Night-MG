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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentState != GameManager.GameState.GameRunning) return;
        GetTouch();
    }

    private void GetTouch(){
        if(Input.touchCount > 0){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0), moveSpeed * Time.deltaTime);
            if(Input.touchCount > 1 && Time.time > lastBullet + fireRate){
                lastBullet = Time.time;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Astroid")){
            GameManager.Instance.LevelOver(false);
        }
    }
}
