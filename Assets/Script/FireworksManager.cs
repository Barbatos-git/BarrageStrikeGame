using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksManager : MonoBehaviour
{
    public GameObject fireworksPrefab;
    public int fireworksCount = 10;
    public float duration = 2f;
    public Vector2 screenBounds;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;
        screenBounds = new Vector2(screenWidth / 2, screenHeight / 2);
        InvokeRepeating("SpawnFirework", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            CancelInvoke("SpawnFirework");
        }
    }

    void SpawnFirework()
    {
        if (fireworksPrefab != null)
        {
            float x = Random.Range(-screenBounds.x, screenBounds.x);
            float y = Random.Range(-screenBounds.y, screenBounds.y);
            Vector3 spawnPosition = new Vector3(x, y, 0f);

            GameObject firework = Instantiate(fireworksPrefab, spawnPosition, Quaternion.identity);
            Destroy(firework, 2f);
        }
    }
}
