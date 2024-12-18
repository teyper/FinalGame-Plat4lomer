using UnityEngine;

public class StayShoot : MonoBehaviour
{
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] float FireRate = 1f;

    GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        InvokeRepeating("InstantiateLaser", 0f, FireRate);
    }

    void InstantiateLaser()
    {
        Instantiate(LaserPrefab, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ"))
        {
            gMan.AddScore(500); // Add score
            Destroy(gameObject); // Destroy shooter
            Destroy(other.gameObject); // Destroy player's attack
        }
    }
}

