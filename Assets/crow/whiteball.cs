using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteball : MonoBehaviour
{
    [SerializeField] GameObject poopParticleEffect;  // Particle effect prefab
    [SerializeField] AudioClip explosionSound;       // Sound effect for whiteball destruction
    private AudioSource audioSource;                // Audio source for playing sound
    private GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        if (gMan == null)
        {
            Debug.LogError("GameManager not found! Make sure it exists in the scene.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ")) // Hit by Player's attack
        {
            gMan.AddScore(200); // Add score to player's total

            // Spawn particle effect
            if (poopParticleEffect != null)
            {
                Instantiate(poopParticleEffect, transform.position, Quaternion.identity);
            }

            // Play explosion sound
            if (explosionSound != null)
            {
                audioSource.PlayOneShot(explosionSound);
            }

            Destroy(gameObject); // Destroy the white ball
            Destroy(other.gameObject); // Destroy player's attack
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // White ball collides with the player
        {
            gMan.UpdateHealth(-10); // Deduct 10 health points from the player

        }
    }
}
