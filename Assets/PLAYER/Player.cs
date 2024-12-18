using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float Speed = 2f;
    [SerializeField] float jumpF = 50f;
    [SerializeField] float ControlSensX = 0.05f;

    [Header("Bounds Settings")]
    [SerializeField] float Xmin = -8f;
    [SerializeField] float Xmax = 8f;
    [SerializeField] float Ymin = -125f;
    [SerializeField] float Ymax = 35f;

    [Header("Attack Settings")]
    [SerializeField] GameObject bsPrefab; // Drag the "bs" prefab in the inspector
    [SerializeField] float bs_x_offset = 0.5f; // Horizontal offset for bs spawn
    [SerializeField] float bs_y_offset = -0.5f; // Vertical offset for bs spawn
    [SerializeField] float bsSpeed = 3f; // Speed of the attack projectile
    [SerializeField] float bsLifeTime = 1f; // Time before projectile is destroyed

    [Header("References")]
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    GameManager gMan;

    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        gMan = FindObjectOfType<GameManager>();
        if (gMan == null) Debug.LogError("GameManager not found!");
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
        ClampPosition(); // Ensure the player stays within bounds
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");

        // Movement logic
        rb.velocity = new Vector3(Speed * x, rb.velocity.y, 0f);

        // Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpF, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetTrigger("jump");
        }

        // Flip player sprite based on direction
        if (x > ControlSensX)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("walk", true);
        }
        else if (x < -ControlSensX)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    void HandleAttack()
    {
        // Start attack when space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("attack", true);

            // Instantiate attack prefab
            Vector3 spawnPosition = transform.position + new Vector3(bs_x_offset * (spriteRenderer.flipX ? -1 : 1), bs_y_offset, 0f);
            GameObject bs = Instantiate(bsPrefab, spawnPosition, Quaternion.identity);

            // Add velocity to the projectile
            Rigidbody2D bsRb = bs.GetComponent<Rigidbody2D>();
            if (bsRb != null)
            {
                float direction = spriteRenderer.flipX ? -1f : 1f; // Flip projectile direction based on player orientation
                bsRb.velocity = new Vector2(bsSpeed * direction, 0f);
            }

            Destroy(bs, bsLifeTime); // Destroy projectile after its lifetime
        }

        // Stop attack when space key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("attack", false);
        }
    }

    // Ground detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    // Handle projectile collisions
   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("shooter") || other.CompareTag("crow"))
        {
            Debug.Log("Player hit by projectile!");
            gMan.UpdateHealth(other.CompareTag("shooter") ? -5 : -10); // Lose health
            Destroy(other.gameObject); // Destroy the projectile
        }
    }*/

    // Constrain player position within the specified bounds
    void ClampPosition()
    {
        float clampedX = Mathf.Clamp(transform.position.x, Xmin, Xmax);
        float clampedY = Mathf.Clamp(transform.position.y, Ymin, Ymax);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
