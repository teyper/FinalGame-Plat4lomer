using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text GOmessageText; // Game Over message
    [SerializeField] TMP_Text MCmessageText; // Mission Complete message

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip missionCompleteSound;
    [SerializeField] AudioClip gameOverSound;

    [Header("Game Settings")]
    public int playerHealth = 100; // Health starts at 100
    public int playerScore = 0;    // Score starts at 0
    private bool gameOver = false; // Tracks game over state
    private bool missionComplete = false; // Tracks mission complete state

    void Start()
    {
        UpdateUI();
        GOmessageText.enabled = false; // Hide Game Over message at start
        MCmessageText.enabled = false; // Hide Mission Complete message at start
    }

    // Update health and check for game over
    public void UpdateHealth(int amount)
    {
        if (gameOver || missionComplete) return; // Stop if game is already over or mission is complete

        playerHealth += amount;
        Debug.Log($"Player health updated: {playerHealth}");

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            TriggerGameOver(); // Trigger Game Over
        }

        UpdateUI();
    }

    // Update player score
    public void AddScore(int amount)
    {
        if (gameOver || missionComplete) return; // Stop if game is already over or mission is complete

        playerScore += amount;
        UpdateUI();
    }

    // Trigger Game Over
    public void TriggerGameOver()
    {
        if (gameOver) return; // Prevent multiple triggers

        gameOver = true;

        Debug.Log("Game Over triggered."); // Debug log for confirmation

        GOmessageText.text = "Game Over!";
        GOmessageText.enabled = true;

        // Play Game Over sound
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.Stop(); // Stop any other sounds
            audioSource.PlayOneShot(gameOverSound);
        }

        // Schedule the splash screen reload
        Invoke("ReloadSplashScreen", 3f);
    }

    // Trigger Mission Complete
    public void MissionComplete()
    {
        if (missionComplete) return; // Prevent multiple triggers
        missionComplete = true;

        MCmessageText.text = "Mission Complete!";
        MCmessageText.enabled = true;

        // Play Mission Complete sound
        if (audioSource != null && missionCompleteSound != null)
        {
            audioSource.Stop(); // Stop any other sounds
            audioSource.PlayOneShot(missionCompleteSound);
        }

        // Schedule the splash screen reload
        Invoke("ReloadSplashScreen", 3f);
    }

    // Update UI elements
    void UpdateUI()
    {
        healthText.text = "Health: " + playerHealth;
        scoreText.text = "Score: " + playerScore;
    }

    // Reload the splash screen
    void ReloadSplashScreen()
    {
        Debug.Log("Reloading Splash Screen...");
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "SplashScreen")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScreen");
        }
    }
}