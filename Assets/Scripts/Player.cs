using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera

    //Base Player Variables
    public float jumpForce = 10f;        // Jump force when hitting a platform
    public float fallSpeed = 2.5f;       // Speed of falling down after jump
    public float jumpDuration = 1f;      // Duration of the upward movement
    public float lowerScreenBound = -5f; // The Y position where the game ends (below screen)
    public float moveSpeed = 5f;         // Speed for horizontal movement
    private Rigidbody2D rb;
    private bool isJumping = false;      // To track if the player is currently jumping


    //Jetpack Variables
    public float normalJumpForce = 10f;   // Normal jump force
    public float jetpackBoostForce = 20f; // Jetpack boost force (higher than normal jump)
    public float jetpackDuration = 5f;    // Duration of the jetpack effect
    private bool isUsingJetpack = false;  // To track if jetpack is active
    private float jetpackEndTime;         // When the jetpack boost should end
    public GameObject jetpackParticleEffect; // The particle effect for jetpack flames

    //GameOverVariables
    public GameObject GameOverScreen;
    public bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Reference to the Rigidbody2D component
        Jump();
    }

    void Update()
    {
        
        HandleMovement();   // Call the method that handles movement
        CheckIfPlayerFell();

        // Check if player falls below the screen (game over)
        if (transform.position.y < lowerScreenBound)
        {
            GameOver();
        }

        // If the player is not jumping, apply downward force
        if (!isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
        }


        // Handle jetpack boost (active for a limited time)
        if (isUsingJetpack)
        {
            isJumping = true;
            // Boost the player upwards while jetpack is active
            rb.velocity = new Vector2(rb.velocity.x, jetpackBoostForce);

            // Check if the jetpack effect should end
            if (Time.time > jetpackEndTime)
            {
                isUsingJetpack = false;
                jetpackParticleEffect.SetActive(false);
                EndJump();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with a platform
        if (collision.gameObject.CompareTag("Platform") && !isJumping)
        {
            Jump();
        }
    }


    void HandleMovement()
    {
        // Get input for horizontal movement (A/D keys or left/right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply movement along the X-axis
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        isJumping = true;   // The player starts jumping
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply the upward jump force

        // Stop the upward movement after a second and allow the player to fall again
        Invoke("EndJump", jumpDuration);
    }

    void EndJump()
    {
        isJumping = false;  // End the jump after a second
    }

    public void ActivateJetpack(float duration)
    {
        isUsingJetpack = true;
        jetpackParticleEffect.SetActive(true); // Enables the particle effect of Jetpack
        jetpackEndTime = Time.time + duration; // Set the time when the jetpack  will end
    }

    void CheckIfPlayerFell()
    {
        // Get the position of the bottom of the camera
        float cameraLowerBound = mainCamera.transform.position.y - mainCamera.orthographicSize;

        // Check if the player's Y position is lower than the bottom of the camera
        if (transform.position.y < cameraLowerBound)
        {
            // Trigger Game Over
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        GameOverScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
