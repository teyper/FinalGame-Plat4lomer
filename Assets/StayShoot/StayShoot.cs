using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] GameObject laserPrefab;           // Laser bullet prefab
    [SerializeField] float fireRate = 2f;              // Time between shots
    [SerializeField] float laserSpeed = 5f;           // Speed of the laser

    [Header("Destruction Settings")]
    [SerializeField] GameObject shooterParticleEffect; // Particle effect prefab
    [SerializeField] AudioClip destructionSound;       // Sound effect for shooter destruction

    private GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        if (gMan == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        // Start firing lasers at intervals
        InvokeRepeating("FireLaser", 0f, fireRate);
    }

    void FireLaser()
    {
        if (laserPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0f, -0.5f, 0f); // Adjust spawn position
            GameObject laser = Instantiate(laserPrefab, spawnPosition, Quaternion.identity);

            // Add movement to the laser
            Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
            if (laserRb != null)
            {
                laserRb.velocity = Vector2.down * laserSpeed; // Move laser downward
            }
            else
            {
                Debug.LogWarning("Laser prefab is missing Rigidbody2D component!");
            }

            // Destroy laser after 3 seconds
            Destroy(laser, 3f);
        }
        else
        {
            Debug.LogWarning("Laser prefab is not assigned!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ")) // Hit by Player's attack
        {
            gMan.AddScore(500); // Add score to player's total

            // Spawn particle effect
            if (shooterParticleEffect != null)
            {
                Instantiate(shooterParticleEffect, transform.position, Quaternion.identity);
            }

            // Play destruction sound
            if (destructionSound != null)
            {
                PlaySound(destructionSound);
            }

            Destroy(gameObject);         // Destroy the shooter
            Destroy(other.gameObject);   // Destroy player's attack
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

