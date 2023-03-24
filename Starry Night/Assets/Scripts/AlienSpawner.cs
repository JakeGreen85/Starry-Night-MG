using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    private float speed = 2f;
    private float spawnRate = 5f;
    public GameObject[] AlienPrefab;

    // Game screen boundaries
    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.score = 0;
        // Set game screen boundaries
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        // Start shooting star spawner
        StartCoroutine(SpawnAliens());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentState != GameManager.GameState.GameRunning) return;
        // Move shooting stars
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.down * speed * Time.deltaTime);

            // Wrap shooting stars around screen edges
            if (child.position.y < screenBottom - 5)
            {
                Destroy(child.gameObject);
            }
        }
    }

    IEnumerator SpawnAliens()
    {
        while (true)
        {
            if(GameManager.Instance.currentState == GameManager.GameState.GameRunning && GameObject.FindGameObjectsWithTag("Alien").Length < 5){
                // Spawn shooting star
                GameObject star = Instantiate(AlienPrefab[Random.Range(0, AlienPrefab.Length)]);
                star.transform.position = new Vector3(Random.Range(screenLeft, screenRight), screenTop + Random.Range(1f, 2f), 0);
                star.transform.parent = transform;
            }
            // Wait for next spawn
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
