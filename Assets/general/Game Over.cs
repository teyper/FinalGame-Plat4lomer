using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] string splashScreenSceneName = "Splasher"; // Splash screen scene name
    [SerializeField] AudioClip gameOverSound;                  // Game Over sound
    private AudioSource audioSource;
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
        if (other.CompareTag("Player")) // Ensure the trigger only works for the player
        {
            if (gMan != null)
            {
                gMan.UpdateHealth(-gMan.playerHealth); // Set health to 0 to trigger game over
            }
        }
    }

    void ReturnToSplashScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(splashScreenSceneName);
    }
}
