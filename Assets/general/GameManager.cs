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
    public int playerHealth = 100;
    public int playerScore = 0;
    bool gameOver = false;

    [Header("Audio Clips")]
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip missionCompleteSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
        messageText.enabled = false;
        messageText1.enabled = false;

    }

    public void UpdateHealth(int amount)
    {
        if (gameOver) return;

        playerHealth += amount;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            GameOver();
        }

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (gameOver) return;

        playerScore += amount;
        UpdateUI();
    }

    void GameOver()
    {
        gameOver = true;
        messageText.text = "Game Over!";
        messageText.enabled = true;

        // Play Game Over Sound
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        Time.timeScale = 0;
    }

    public void MissionComplete()
    {
        if (gameOver) return;

        gameOver = true;
        messageText1.text = "Mission Complete!";
        messageText1.enabled = true;

        // Play Mission Complete Sound
        if (missionCompleteSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(missionCompleteSound);
        }

        Time.timeScale = 0;
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + playerHealth;
        scoreText.text = "Score: " + playerScore;
    }
}
