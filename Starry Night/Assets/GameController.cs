using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Shooting star prefab
    public GameObject starPrefab;

    // Shooting star speed
    public float starSpeed = 5f;

    // Shooting star spawn rate
    public float spawnRate = 0.5f;

    // Game screen boundaries
    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;

    // Start is called before the first frame update
    void Start()
    {
        // Set game screen boundaries
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        // Start shooting star spawner
        StartCoroutine(SpawnStars());
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    IEnumerator SpawnStars()
    {
        while (true)
        {
            // Spawn shooting star
            GameObject star = Instantiate(starPrefab);
            star.transform.position = new Vector3(Random.Range(screenLeft, screenRight), screenTop + Random.Range(1f, 2f), 0);
            star.transform.parent = transform;

            // Wait for next spawn
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
