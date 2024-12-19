using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bb : MonoBehaviour
{
    [SerializeField] AudioClip blastSound; // Sound to play when blast is triggered
    private AudioSource audioSource;
    GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();

        // Add an AudioSource component if it doesn't already exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the clip and configure AudioSource settings
        if (blastSound != null)
        {
            audioSource.clip = blastSound;
            audioSource.playOnAwake = false; // Don't play automatically on instantiation
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("shooter") || other.CompareTag("crow"))
        {
            // Play the sound effect
            if (audioSource != null && blastSound != null)
            {
                audioSource.PlayOneShot(blastSound);
            }

            // Deduct health or destroy the collided object
            Debug.Log("Player hit a target!");
            gMan.AddScore(other.CompareTag("shooter") ? 500 : 200); // Add score based on target
            Destroy(other.gameObject); // Destroy the target
            Destroy(gameObject); // Destroy the attack itself
        }
    }
}
