using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteball : MonoBehaviour
{
    [SerializeField] GameObject poopParticleEffect;  // particle effect prefab
    GameManager gMan;
    // Start is called before the first frame update
    void Start()
    {
        gMan = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerZ") || other.CompareTag("Player")) // hit by Player's attack
        {
            gMan.AddScore(200); // Add score to player's total
            // Spawn particle effect
            Instantiate(poopParticleEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
            Destroy(other.gameObject); // destroy player's attack

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ball collides with player
        {
            gMan.UpdateHealth(-10); // deduct 10 health
           
        }
    }
}