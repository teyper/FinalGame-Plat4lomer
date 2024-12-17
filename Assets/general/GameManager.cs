using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text gameOverText;

    public void GameOver()
    {
        Debug.Log("Game Over!");
        if (gameOverText != null)
        {
            gameOverText.text = "Game Over!";
            gameOverText.enabled = true; // Show Game Over message
        }

        // Optionally freeze the game
        Time.timeScale = 0;
    }
}
