using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;  // Speed of the platform movement
    public float moveRange = 3f;  // Range the platform moves from its starting position

    private Vector3 startPosition;  // Initial position of the platform
    private bool movingRight = true;  // Direction flag

    void Start()
    {
        // Store the starting position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the platform left and right
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // If the platform reaches the right limit, change direction
            if (transform.position.x >= startPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // If the platform reaches the left limit, change direction
            if (transform.position.x <= startPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }
}
