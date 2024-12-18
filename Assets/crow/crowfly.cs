using UnityEngine;

using UnityEngine;

public class crowfly : MonoBehaviour
{
    [SerializeField] GameObject FallingObjectPrefab; // Falling object prefab
    [SerializeField] GameObject crowParticleEffect;  // Particle effect prefab
    [SerializeField] float DropInterval = 1f;        // Interval for dropping objects
    [SerializeField] AudioClip explosionAudio;       // Explosion sound clip
    [SerializeField] AudioSource audioSource;        // AudioSource to play sounds

    GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        InvokeRepeating("DropFallingObject", 0f, DropInterval);
    }

    void DropFallingObject()
    {
        Instantiate(FallingObjectPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ") || other.CompareTag("Player")) // Hit by Player's attack
        {
            gMan.AddScore(200); // Add score to player's total

            // Play explosion sound
            if (audioSource != null && explosionAudio != null)
            {
                audioSource.PlayOneShot(explosionAudio);
            }

            // Spawn particle effect
            Instantiate(crowParticleEffect, transform.position, Quaternion.identity);

            Destroy(gameObject); // Destroy the crow
            Destroy(other.gameObject); // Destroy player's attack
        }
    }





// Handle collision with the player
void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Crow collides with player
        {
            gMan.UpdateHealth(-10); // Deduct 10 health
            //Instantiate(crowParticleEffect, transform.position, Quaternion.identity); // Spawn particle effect
            //Destroy(gameObject); // Destroy crow
        }
    }
}
