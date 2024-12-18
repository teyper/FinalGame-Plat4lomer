using UnityEngine;

public class StayShoot : MonoBehaviour
{
    [SerializeField] GameObject LaserPrefab;            // Prefab for the laser
    [SerializeField] GameObject shooterParticleEffect;  // Particle effect prefab
    [SerializeField] float FireRate = 1f;               // Rate at which lasers are fired
    [SerializeField] AudioSource audioSource;           // AudioSource for playing sounds
    [SerializeField] AudioClip explosionAudio;          // Explosion sound clip

    GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        if (gMan == null) Debug.LogError("GameManager not found!");

        InvokeRepeating("InstantiateLaser", 0f, FireRate);
    }

    void InstantiateLaser()
    {
        Vector3 laserPosition = transform.position + new Vector3(0f, -0.5f, 0f);
        GameObject laser = Instantiate(LaserPrefab, laserPosition, Quaternion.identity);
        Destroy(laser, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ")) // Hit by Player's attack
        {
            if (gMan != null)
            {
                gMan.AddScore(500); // Add score
            }

            // Play explosion sound
            if (audioSource != null && explosionAudio != null)
            {
                audioSource.PlayOneShot(explosionAudio);
            }

            // Spawn particle effect
            Instantiate(shooterParticleEffect, transform.position, Quaternion.identity);

            Destroy(gameObject); // Destroy shooter
            Destroy(other.gameObject); // Destroy player's attack
        }
    }





void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Collides with Player
        {
            if (gMan != null)
            {
                gMan.UpdateHealth(-10); // Deduct health
            }
            //Instantiate(shooterParticleEffect, transform.position, Quaternion.identity); // Spawn particle effect
            //Destroy(gameObject); // Destroy shooter
        }
    }
}
