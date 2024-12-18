using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] AudioClip gameOverSound; // Audio for game over
    private AudioSource audioSource;
    private bool gameOverTriggered = false;  // Ensure it triggers only once

    private GameManager gMan;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        gMan = FindObjectOfType<GameManager>();
        if (gMan == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !gameOverTriggered) // Check if it's the player and not already triggered
        {
            gameOverTriggered = true; // Mark game over as triggered to prevent multiple activations

            Debug.Log("Game Over Triggered!");

            // Play game over sound
            if (gameOverSound != null)
            {
                audioSource.PlayOneShot(gameOverSound);
            }

            // Notify the GameManager
            if (gMan != null)
            {
                gMan.TriggerGameOver();
            }

                // Set health to 0
                gMan.UpdateHealth(-gMan.playerHealth); // Deduct all remaining health
            }
        }
    }

