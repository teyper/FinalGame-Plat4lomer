using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text messageText;
    [SerializeField] TMP_Text messageText1;


    [Header("Game Settings")]
    public int playerHealth = 100; // Health starts at 100
    public int playerScore = 0;    // Score starts at 0
    bool gameOver = false;         // Tracks game state

    void Start()
    {
        UpdateUI();
        messageText.enabled = false; // Hide messages at the start
        messageText1.enabled = false;
    }

    // Update health and check for game over
    public void UpdateHealth(int amount)
    {
        if (gameOver) return;

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
        if (gameOver) return;

        playerScore += amount;
        UpdateUI();
    }

    // Trigger Game Over
    void GameOver()
    {
        gameOver = true;
        messageText.text = "Game Over!";
        messageText.enabled = true;
        Time.timeScale = 0; // Freeze the game
    }

    // Trigger Mission Complete
    public void MissionComplete()
    {
        if (gameOver) return;

        gameOver = true;
        messageText1.text = "Mission Complete!";
        messageText1.enabled = true;
        Time.timeScale = 0; // Freeze the game
    }

    // Update UI elements
    void UpdateUI()
    {
        healthText.text = "Health: " + playerHealth;
        scoreText.text = "Score: " + playerScore;
    }
}
