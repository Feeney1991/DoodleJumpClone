using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    public Transform player;         // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // Smooth factor for camera movement
    public Vector3 offset;           // Offset to keep the camera at a desired distance from the player

    private float initialCameraY;    // Initial Y position of the camera

    void Start()
    {
        // Store the initial camera Y position
        initialCameraY = transform.position.y;
    }

    void LateUpdate()
    {
        // Only follow if the player is above the current camera position and moving up
        if (player.position.y > transform.position.y && player.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Target position to follow the player with an offset
        Vector3 targetPosition = new Vector3(transform.position.x, player.position.y, transform.position.z) + offset;

        // Smoothly interpolate the camera's position towards the target
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Set the new camera position
        transform.position = smoothedPosition;
    }
}
