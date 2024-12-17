using UnityEngine;

public class LaserMove : MonoBehaviour
{
    [SerializeField] float laserSpeed = 1f; // Speed of the laser
    [SerializeField] float lifeTime = 3f;   // Time before the laser is destroyed

    Rigidbody2D rb;

    void Start()
    {
        // Automatically destroy the laser after 'lifeTime' seconds
        Destroy(gameObject, lifeTime);

        // Move the laser with Rigidbody2D force
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * laserSpeed; // Set velocity based on direction
        }
        else
        {
            Debug.LogWarning("Rigidbody2D not found. Using Transform.Translate as fallback.");
        }
    }

    void Update()
    {
        // Fallback movement if Rigidbody2D is not found
        if (rb == null)
        {
            transform.Translate(Vector2.right * laserSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the laser when it collides with something
        Debug.Log($"Laser hit {other.gameObject.name}");
        Destroy(gameObject);
    }
}
