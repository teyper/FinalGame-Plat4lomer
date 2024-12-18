using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] string splashScreenSceneName = "Splasher"; // Splash screen scene name
    [SerializeField] AudioClip gameOverSound; // Assign Game Over sound in Inspector

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if missing
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player falls and hits the trigger
        {
            Debug.Log("Game Over!");
            if (gameOverSound != null)
            {
                audioSource.PlayOneShot(gameOverSound); // Play game over sound
            }

            Invoke("ReturnToSplashScreen", 2f); // Wait 2 seconds, then return to splash screen
        }
    }

    void ReturnToSplashScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(splashScreenSceneName); // Load splash screen
    }
}
