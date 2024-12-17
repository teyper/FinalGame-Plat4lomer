using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zommieGUY : MonoBehaviour 
{
    Animator animator;
    [SerializeField] GameObject player;
    SpriteRenderer spriteR;

    [SerializeField] float chaseDistance = 3f; // Distance to start chasing the player
    [SerializeField] float attackDistance = 1f; // Distance to start attacking the player
    [SerializeField] float moveSpeed = 2f; // Speed of the NPC movement

    private bool isAttacking = false;

    [SerializeField] private int health = 8; // NPC health
    [SerializeField] private int damage = 2; // damage intervals

    public void TakeDamage(int damage) // Implementing IDamageable
    {
        health -= damage; // Reduce health by damage value
        Debug.Log($"NPC took {damage} damage. Remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("NPC is destroyed.");
        animator.SetTrigger("die"); // Play death animation
        Destroy(gameObject, 1f); // Destroy object after 1 second to allow animation to play
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        animator.SetBool("idle", true); // NPC starts idle
        Debug.Log("NPC initialized and set to idle state.");
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= attackDistance && !isAttacking)
        {
            // Attack player
            Debug.Log("Player is within attack distance. Starting attack.");
            animator.SetBool("run", false); // Stop running
            animator.SetBool("attack", true); // Start attacking
            isAttacking = true; // Prevent redundant attacks
        }
        else if (distance <= chaseDistance && distance > attackDistance)
        {
            // Chase player
            Debug.Log("Player is within chase distance but outside attack range. Chasing player.");
            animator.SetBool("idle", false); // Stop idling
            animator.SetBool("run", true); // Start running
            animator.SetBool("attack", false); // Stop attacking
            isAttacking = false; // Ready to attack when in range

            // Move toward the player
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Flip sprite based on direction
            spriteR.flipX = direction.x < 0;
        }
        else
        {
            // Idle state
            Debug.Log("Player is out of range. Return to idle state.");
            animator.SetBool("idle", true);
            animator.SetBool("run", false);
            animator.SetBool("attack", false);
            isAttacking = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack")) // Player's attack collider
        {
            Debug.Log("NPC hit by player.");
            TakeDamage(2); // Take damage when hit
        }

        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone. NPC starts running.");
            animator.SetBool("idle", false); // Stop idling
            animator.SetBool("run", true); // Start running
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger zone. NPC returns to idle state.");
            animator.SetBool("idle", true); // Return to idle
            animator.SetBool("run", false); // Stop running
        }
    }
}
