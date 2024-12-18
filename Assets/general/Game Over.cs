using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        if (gMan == null)
        {
            Debug.LogError("GameManager not found! Make sure it exists in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player collides with the trigger
        {
            Debug.Log("Player hit Game Over trigger!");

            // Set health to 0
            gMan.UpdateHealth(-gMan.playerHealth); // Deduct all remaining health
        }
    }
}
