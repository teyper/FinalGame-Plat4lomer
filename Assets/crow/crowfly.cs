using UnityEngine;

public class crowfly : MonoBehaviour
{
    [Header("Falling Object Settings")]
    [SerializeField] GameObject FallingObjectPrefab; // White poop prefab
    [SerializeField] float DropInterval = 2f;        // Time between drops

    [Header("White Poop Settings")]
    [SerializeField] float WhitePoopSpeed = 3f; // Speed of the falling white poop

    GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        if (gMan == null) Debug.LogError("GameManager not found!");

        InvokeRepeating("DropFallingObject", 0f, DropInterval); // Start dropping objects
    }

    void DropFallingObject()
    {
        GameObject fallingPoop = Instantiate(FallingObjectPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);

        // Add velocity to the falling poop
        Rigidbody2D rb = fallingPoop.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = fallingPoop.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Disable gravity
        }
        rb.velocity = Vector2.down * WhitePoopSpeed; // Make it fall vertically
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Crow hit by player's attack
        if (other.CompareTag("playerZ"))
        {
            gMan.AddScore(200); // Add 200 points
            Destroy(gameObject); // Destroy crow
            Destroy(other.gameObject); // Destroy player's attack
        }
    }
}

public class WhitePoopHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // White poop is destroyed when hit by the player's attack
        if (other.CompareTag("playerZ"))
        {
            Debug.Log("White poop destroyed!");
            Destroy(gameObject); // Destroy white poop
            Destroy(other.gameObject); // Destroy player's attack
        }
    }
}


