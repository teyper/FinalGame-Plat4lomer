using UnityEngine;

public class crowfly : MonoBehaviour
{
    [SerializeField] GameObject FallingObjectPrefab;
    [SerializeField] GameObject crowParticleEffect; // Particle effect prefab
    [SerializeField] float DropInterval = 2f;
    [SerializeField] GameObject poopParticleEffect; // Particle effect prefab

    GameManager gMan;

    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
        InvokeRepeating("DropFallingObject", 0f, DropInterval);
    }

    void DropFallingObject()
    {
        Instantiate(FallingObjectPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ")) // Hit by Player's attack
        {
            gMan.AddScore(200); // Add score
            Instantiate(crowParticleEffect, transform.position, Quaternion.identity); // Spawn particle effect
            Destroy(gameObject); // Destroy crow
            Destroy(other.gameObject); // Destroy player's attack
        }

        if (other.CompareTag("playerZ")) // Hit by Player's attack
        {
            Instantiate(poopParticleEffect, transform.position, Quaternion.identity); // Spawn particle effect
            Destroy(gameObject); // Destroy white poop
            Destroy(other.gameObject); // Destroy player's attack
        }
    }
    

}
