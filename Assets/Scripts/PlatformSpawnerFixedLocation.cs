using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerFixedLocation : MonoBehaviour
{
    public GameObject platformTypeA;  // The most common platform
    public GameObject platformTypeB;  // Less common platform
    public GameObject platformTypeC;  // Rare platform

    public float screenWidth = 9f;    // The width of the screen (X-axis range)
    public int initialPlatformCount = 10; // The initial number of platforms to spawn
    public float maxHeight = 5000f;   // The maximum height to scatter platforms up to
    public float platformsPer1000Y = 2f; // Decrease factor for platforms every 1000 Y units

    // Weights for each platform type
    public float weightA = 0.7f;  // 70% chance for Type A
    public float weightB = 0.2f;  // 20% chance for Type B
    public float weightC = 0.1f;  // 10% chance for Type C

    void Start()
    {
        // Call the method to spawn platforms at the start of the game
        SpawnPlatforms();
    }

    void SpawnPlatforms()
    {
        // Loop through the Y axis from 0 to maxHeight
        for (int i = 0; i < 5; i++)   // Loop through 5 sections (5000/1000)
        {
            float heightStart = i * 1000f; // Start of the current Y section
            float heightEnd = heightStart + 1000f; // End of the current Y section
            int platformCount = Mathf.Max(1, Mathf.RoundToInt(initialPlatformCount - platformsPer1000Y * i)); // Number of platforms for this section

            // Randomly spawn platforms for the current section
            SpawnPlatformsInSection(heightStart, heightEnd, platformCount);
        }
    }

    void SpawnPlatformsInSection(float heightStart, float heightEnd, int platformCount)
    {
        for (int i = 0; i < platformCount; i++)
        {
            // Random X position within screen bounds
            float randomX = Random.Range(-screenWidth / 2f, screenWidth / 2f);

            // Random Y position within the current section's bounds
            float randomY = Random.Range(heightStart, heightEnd);

            // Determine which platform type to spawn based on random chance
            GameObject platformPrefab = GetRandomPlatformType();

            // Create the platform at the random position
            Vector2 spawnPosition = new Vector2(randomX, randomY);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    GameObject GetRandomPlatformType()
    {
        // Get a random float between 0 and 1
        float randomValue = Random.Range(0f, 1f);

        // Determine which platform to spawn based on the weights
        if (randomValue <= weightA)
        {
            return platformTypeA;  // Type A (most common)
        }
        else if (randomValue <= weightA + weightB)
        {
            return platformTypeB;  // Type B (less common)
        }
        else
        {
            return platformTypeC;  // Type C (rare)
        }
    }
}
