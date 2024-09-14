using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject thePlayer;
    public GameObject platformPrefab;      // The platform prefab to spawn
    public float spawnInterval = 1f;       // Time interval between platform spawns
    public float platformSpeed = 2f;       // Speed at which the platforms descend
    public float screenWidth = 9f;         // The width of the screen (X-axis range)
    public float spawnHeight = 10f;         // The height above the screen where platforms spawn
    public float resetHeight = -5f;        // The height below the screen where platforms reset

    void Start()
    {
        // Continuously spawn platforms at intervals
        InvokeRepeating("SpawnPlatform", 0f, spawnInterval);

        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void SpawnPlatform()
    {
        // Random X position within screen bounds
        float randomX = Random.Range(-screenWidth / 2f, screenWidth / 2f);
        Vector2 spawnPosition = new Vector2(randomX,  thePlayer.transform.position.y +  spawnHeight);

        // Instantiate the platform at the spawn position
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Attach the movement script to the platform
        platform.AddComponent<PlatformMovement>().Init(platformSpeed, resetHeight, spawnHeight);
    }
}

public class PlatformMovement : MonoBehaviour
{
    private float speed;             // Speed at which the platform descends
    private float resetPositionY;    // Y position where the platform resets to the top
    private float spawnPositionY;    // Y position where the platform reappears

    // Initialization method to pass in values from the spawner
    public void Init(float platformSpeed, float resetY, float spawnY)
    {
        speed = platformSpeed;
        resetPositionY = resetY;
        spawnPositionY = spawnY;
    }

    void Update()
    {
        // Move the platform down
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // If the platform goes below the reset height, move it back to the top
        if (transform.position.y < resetPositionY)
        {
            // Random X position within the screen bounds for the reset position
            float randomX = Random.Range(-9f / 2f, 9f / 2f);  // Adjust based on screen width
            transform.position = new Vector2(randomX, spawnPositionY);
        }
    }
}
