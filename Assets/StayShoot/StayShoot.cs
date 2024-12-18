using UnityEngine;

public class StayShoot : MonoBehaviour
{
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject shooterParticleEffect; // Particle effect prefab
    [SerializeField] float FireRate = 1f;
    [SerializeField] AudioClip laserSound; // Laser sound
    AudioSource audioSource;
    AudioSource audi;
    GameManager gMan;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audi = GetComponent<AudioSource>();
        InvokeRepeating("InstantiateLaser", 0f, FireRate);
    }

    void InstantiateLaser()
    {
        Vector3 laserPosition = transform.position + new Vector3(0f, -0.5f, 0f);
        GameObject laser = Instantiate(LaserPrefab, laserPosition, Quaternion.identity);
        Destroy(laser, 3f);

        // Play laser sound
        if (laserSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(laserSound);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ") || other.CompareTag("Player"))
        {
            GameManager gMan = FindObjectOfType<GameManager>();
            gMan.AddScore(500);
            Instantiate(shooterParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            // Play destroy sound
            if (audi != null)
            {
                audi.Play();
            }

            Destroy(other.gameObject);
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
