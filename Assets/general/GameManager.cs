using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text messageText;

    [Header("Game Settings")]
    public int playerHealth = 100; // Health starts at 100
    public int playerScore = 0;    // Score starts at 0
    private bool gameOver = false; // Tracks game state
    private bool missionComplete = false;

    void Start()
    {
        UpdateUI();
        messageText.enabled = false; // Hide messages at the start
    }

    // Update health and check for game over
    public void UpdateHealth(int amount)
    {
        if (gameOver || missionComplete) return;

        playerHealth += amount;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            GameOver(); // Trigger game over
        }

        UpdateUI();
    }

    // Update player score
    public void AddScore(int amount)
    {
        if (gameOver || missionComplete) return;

        playerScore += amount;
        UpdateUI();
    }

    // Trigger Game Over
    public void GameOver()
    {
        if (gameOver || missionComplete) return;

        gameOver = true;
        messageText.text = "Game Over!";
        messageText.enabled = true;

        Debug.Log("Game Over!");

        // Return to splash screen after delay
        Invoke("ReturnToSplashScreen", 5f);
    }

    // Trigger Mission Complete
    public void MissionComplete()
    {
        if (gameOver || missionComplete) return;

        missionComplete = true;
        messageText.text = "Mission Complete!";
        messageText.enabled = true;

        Debug.Log("Mission Complete!");

        // Return to splash screen after delay
        Invoke("ReturnToSplashScreen", 5f);
    }

    // Update UI elements
    void UpdateUI()
    {
        healthText.text = "Health: " + playerHealth;
        scoreText.text = "Score: " + playerScore;
    }

    // Return to Splash Screen
    void ReturnToSplashScreen()
    {
        SceneManager.LoadScene("SplashScreen");
    }
}
