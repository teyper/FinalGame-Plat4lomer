using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteball : MonoBehaviour
{
    [SerializeField] GameObject poopParticleEffect;  // Particle effect prefab
    [SerializeField] AudioClip explosionSound;       // Sound effect for whiteball destruction
    private GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        if (gMan == null)
        {
            Debug.LogError("GameManager not found! Make sure it exists in the scene.");
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

            // Play explosion sound using a temporary GameObject
            if (explosionSound != null)
            {
                PlaySound(explosionSound);
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

    private void PlaySound(AudioClip clip)
    {
        // Create a temporary GameObject to play the sound
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
        tempAudioSource.clip = clip;
        tempAudioSource.Play();

        // Destroy the temporary GameObject after the clip duration
        Destroy(tempAudio, clip.length);
    }
}
