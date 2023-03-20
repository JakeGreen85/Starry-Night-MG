using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AstroidSpawner : MonoBehaviour
{
    // Shooting star prefab
    public GameObject[] astroidPrefab;

    // Shooting star speed
    public float starSpeed = 3f;

    // Shooting star spawn rate
    public float spawnRate = 2f;

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
        StartCoroutine(SpawnAstroids());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentState != GameManager.GameState.GameRunning) return;
        // Move shooting stars
        foreach (Transform child in transform)
        {
            child.Translate(Vector3.down * starSpeed * Time.deltaTime);

            // Wrap shooting stars around screen edges
            if (child.position.y < screenBottom)
            {
                float x = Random.Range(screenLeft, screenRight);
                float y = screenTop + Random.Range(1f, 2f);
                child.position = new Vector3(x, y, 0);
            }
        }
        starSpeed = 3f + (Time.time / 100f);
    }

    public

    IEnumerator SpawnAstroids()
    {
        while (true)
        {
            if(GameManager.Instance.currentState == GameManager.GameState.GameRunning && GameObject.FindGameObjectsWithTag("Astroid").Length < 25){
                // Spawn shooting star
                GameObject star = Instantiate(astroidPrefab[Random.Range(0, astroidPrefab.Length)]);
                star.transform.position = new Vector3(Random.Range(screenLeft, screenRight), screenTop + Random.Range(1f, 2f), 0);
                star.transform.parent = transform;
            }
            // Wait for next spawn
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
