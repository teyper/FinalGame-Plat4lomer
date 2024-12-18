using UnityEngine;

public class StayShoot : MonoBehaviour
{
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject shooterParticleEffect; // Particle effect prefab
    [SerializeField] float FireRate = 1f;

    void Start()
    {
        InvokeRepeating("InstantiateLaser", 0f, FireRate);
    }

    void InstantiateLaser()
    {
        Vector3 laserPosition = transform.position + new Vector3(0f, -0.5f, 0f);
        GameObject laser = Instantiate(LaserPrefab, laserPosition, Quaternion.identity);
        Destroy(laser, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ")) // Hit by Player's attack
        {
            GameManager gMan = FindObjectOfType<GameManager>();
            gMan.AddScore(500); // Add score
            Instantiate(shooterParticleEffect, transform.position, Quaternion.identity); // Spawn particle effect
            Destroy(gameObject); // Destroy shooter
            Destroy(other.gameObject); // Destroy player's attack
        }
    }
}
