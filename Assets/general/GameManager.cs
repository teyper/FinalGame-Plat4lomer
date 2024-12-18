using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text GOmessageText; // Game Over message
    [SerializeField] TMP_Text MCmessageText; // Mission Complete message
    [SerializeField] TMP_Text timerText;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip missionCompleteSound;
    [SerializeField] AudioClip gameOverSound;

    [Header("Game Settings")]
    public int playerHealth = 100; // Health starts at 100
    public int playerScore = 0;    // Score starts at 0
    private bool gameOver = false; // Tracks game over state
    private bool missionComplete = false; // Tracks mission complete state
    private float timer = 90f; // Timer set to 2 minutes 30 seconds

    void Start()
    {
        UpdateUI();
        GOmessageText.enabled = false; // Hide Game Over message at start
        MCmessageText.enabled = false; // Hide Mission Complete message at start
        timerText.enabled = true; // Show timer text
    }

    void Update()
    {
        if (!gameOver && !missionComplete)
        {
            UpdateTimer(); // Continuously update the timer during gameplay
        }
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            TriggerGameOver(); // Trigger game over when timer runs out
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    public void UpdateHealth(int amount)
    {
        if (gameOver || missionComplete) return;

        playerHealth += amount;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            TriggerGameOver();
        }

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (gameOver || missionComplete) return;

        playerScore += amount;
        UpdateUI();
    }

    public void TriggerGameOver()
    {
        if (gameOver) return;

        gameOver = true;

        GOmessageText.text = "Game Over!";
        GOmessageText.enabled = true;

        if (audioSource != null && gameOverSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(gameOverSound);
        }

        Invoke("ReloadSplashScreen", 3f);
    }

    public void MissionComplete()
    {
        if (missionComplete) return;

        missionComplete = true;

        MCmessageText.text = "Mission Complete!";
        MCmessageText.enabled = true;

        if (audioSource != null && missionCompleteSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(missionCompleteSound);
        }

        Invoke("ReloadSplashScreen", 3f);
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + playerHealth;
        scoreText.text = "Score: " + playerScore;
    }

    void ReloadSplashScreen()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Splasher")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Splasher");
        }
    }
}
