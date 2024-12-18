using UnityEngine;

public class crowfly : MonoBehaviour
{
    [SerializeField] GameObject FallingObjectPrefab;
    [SerializeField] GameObject crowParticleEffect; // Particle effect prefab
    [SerializeField] float DropInterval = 2f;
    [SerializeField] AudioClip crowDestroySound; // Crow destruction sound
    [SerializeField] AudioClip dropSound; // Sound for dropping objects
    AudioSource audioSource;

    GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("DropFallingObject", 0f, DropInterval);
    }

    void DropFallingObject()
    {
        Instantiate(FallingObjectPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);

        // Play drop sound
        if (dropSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dropSound);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ") || other.CompareTag("Player"))
        {
            gMan.AddScore(200);
            Instantiate(crowParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            // Play destruction sound
            if (crowDestroySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(crowDestroySound);
            }

            Destroy(other.gameObject);
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
