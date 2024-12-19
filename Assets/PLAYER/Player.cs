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
    [SerializeField] GameObject bsPrefab;
    [SerializeField] float bs_x_offset = 0.5f;
    [SerializeField] float bs_y_offset = -0.5f;
    [SerializeField] float bsSpeed = 3f;
    [SerializeField] float bsLifeTime = 1f;

    [Header("Audio Settings")]
    [SerializeField] AudioSource attackAudioSource;

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

        if (attackAudioSource == null)
        {
            Debug.LogError("Attack AudioSource is not assigned!");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
        ClampPosition();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(Speed * x, rb.velocity.y, 0f);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpF, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetTrigger("jump");
        }

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("attack", true);

            if (attackAudioSource != null)
            {
                attackAudioSource.Play();
                Debug.Log("Attack sound played!");
            }

            Vector3 spawnPosition = transform.position + new Vector3(bs_x_offset * (spriteRenderer.flipX ? -1 : 1), bs_y_offset, 0f);
            GameObject bs = Instantiate(bsPrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D bsRb = bs.GetComponent<Rigidbody2D>();
            if (bsRb != null)
            {
                float direction = spriteRenderer.flipX ? -1f : 1f;
                bsRb.velocity = new Vector2(bsSpeed * direction, 0f);
            }

            Destroy(bs, bsLifeTime);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("attack", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("shooter") || other.CompareTag("crow"))
        {
            gMan.UpdateHealth(other.CompareTag("shooter") ? -5 : -10);
        }
    }

    void ClampPosition()
    {
        float clampedX = Mathf.Clamp(transform.position.x, Xmin, Xmax);
        float clampedY = Mathf.Clamp(transform.position.y, Ymin, Ymax);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
