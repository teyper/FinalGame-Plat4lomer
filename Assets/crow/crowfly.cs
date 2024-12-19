using UnityEngine;

public class crowfly : MonoBehaviour
{
    [SerializeField] GameObject FallingObjectPrefab;  // The white ball prefab
    [SerializeField] GameObject crowParticleEffect;   // Particle effect prefab
    [SerializeField] AudioClip destructionSound;      // Sound effect for crow destruction
    [SerializeField] float DropInterval = 2f;         // Time between white ball drops

    private GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        if (gMan == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        InvokeRepeating("DropFallingObject", 0f, DropInterval);
    }

    void DropFallingObject()
    {
        // Instantiate the white ball prefab below the crow
        if (FallingObjectPrefab != null)
        {
            Instantiate(FallingObjectPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("FallingObjectPrefab not assigned!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ")) // Hit by Player's attack
        {
            gMan.AddScore(200); // Add score to player's total

            // Spawn particle effect
            if (crowParticleEffect != null)
            {
                Instantiate(crowParticleEffect, transform.position, Quaternion.identity);
            }

            // Play destruction sound
            if (destructionSound != null)
            {
                PlaySound(destructionSound);
            }

            Destroy(gameObject);         // Destroy the crow
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
