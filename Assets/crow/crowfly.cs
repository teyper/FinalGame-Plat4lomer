using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowfly : MonoBehaviour
{
    // Position boundaries
    [SerializeField] float Left = -8f;
    [SerializeField] float Right = 8f;
    [SerializeField] float Top = 5f;
    [SerializeField] float Bottom = -5f;

    // Speed and velocity settings
    [SerializeField] float MaxVelocity = 4.5f;
    [SerializeField] float MaxRotationVelocity = 0f;

    // Falling Object Settings
    [SerializeField] GameObject FallingObjectPrefab; // Drag the falling prefab in the Inspector
    [SerializeField] float DropInterval = 2f;
    [SerializeField] float LifeTime = 2f;// Time interval between drops

    // Other components
    GameManager gameManager;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        // Initialize GameManager and Rigidbody
        gameManager = FindObjectOfType<GameManager>();
        Rigidbody2D rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set bird position
        transform.position = new Vector3(Random.Range(Left, Right), Random.Range(Top, Bottom), 0f);

        // Randomize bird velocity
        float RandomXVelocity = Random.Range(-MaxVelocity, MaxVelocity);
        float RandomYVelocity = Random.Range(-MaxVelocity, MaxVelocity);
        rigidbody2.velocity = new Vector3(RandomXVelocity, RandomYVelocity, 0f);

        // Randomize bird rotation
        rigidbody2.angularVelocity = Random.Range(-MaxRotationVelocity, MaxRotationVelocity);

        // Start dropping objects
        InvokeRepeating("DropFallingObject", LifeTime, DropInterval);
    }

    void Update()
    {
        // Wrap position horizontally
        if (transform.position.x < Left)
            transform.position = new Vector3(Right, transform.position.y, transform.position.z);

        if (transform.position.x > Right)
            transform.position = new Vector3(Left, transform.position.y, transform.position.z);

        // Wrap position vertically
        if (transform.position.y > Top)
            transform.position = new Vector3(transform.position.x, Bottom, transform.position.z);

        if (transform.position.y < Bottom)
            transform.position = new Vector3(transform.position.x, Top, transform.position.z);
    }

    // Drop a falling object directly below the crow
    void DropFallingObject()
    {
        if (FallingObjectPrefab != null)
        {
            Vector3 dropPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, 0f);
            Instantiate(FallingObjectPrefab, dropPosition, Quaternion.identity);
            Debug.Log("Falling object dropped!");
        }
        else
        {
            Debug.LogError("FallingObjectPrefab not assigned in the Inspector.");
        }
    }
}
