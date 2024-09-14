using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public float boostDuration = 5f;  // Duration of the jetpack boost

    // When the player collides with the jetpack pickup
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Get the player's JetpackController and activate the jetpack
            other.GetComponent<Player>().ActivateJetpack(boostDuration);

            // Destroy the jetpack pickup after it's collected
            Destroy(gameObject);
        }
    }
}
