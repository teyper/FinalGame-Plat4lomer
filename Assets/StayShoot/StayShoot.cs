using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayShoot : MonoBehaviour
{
    // Movement Variables
    bool movingRight = true;
    [SerializeField] float Speed = 2.8f;      // Movement speed of platform and shooter
    [SerializeField] float XLimit = 3.5f;     // Horizontal movement limit

    // Laser Instantiation Variables
    [SerializeField] GameObject LaserPrefab;  // The laser prefab
    [SerializeField] float FireRate = 1f;     // Time between shots

    // Laser Position Offset
    [SerializeField] float laser_x_offset = 0f; // Horizontal offset for laser spawn
    [SerializeField] float laser_y_offset = -0.5f; // Vertical offset for laser spawn

    private Transform platformTransform; // To reference the platform's position

    void Start()
    {
        // If the shooter is a child of the platform, get its parent's transform
        platformTransform = transform.parent;

        // Start firing lasers
        InvokeRepeating("InstantiateLaser", 0f, FireRate); // Shoot at consistent intervals
    }

    void Update()
    {
        MoveShooter();
        StickToPlatform();
    }

    // Ensures the shooter moves with the platform
    void StickToPlatform()
    {
        if (platformTransform != null)
        {
            // Lock the shooter to the platform's horizontal position
            transform.position = new Vector3(platformTransform.position.x, transform.position.y, transform.position.z);
        }
    }

    // Movement Function (Horizontal Movement)
    void MoveShooter()
    {
        if (movingRight)
        {
            transform.Translate(Speed * Time.deltaTime, 0f, 0f);  // Move right
            if (transform.position.x > XLimit)
            {
                movingRight = false;  // Switch direction at right limit
            }
        }
        else
        {
            transform.Translate(-Speed * Time.deltaTime, 0f, 0f);  // Move left
            if (transform.position.x < -XLimit)
            {
                movingRight = true;  // Switch direction at left limit
            }
        }
    }

    // Instantiate Laser Prefab
    public void InstantiateLaser()
    {
        // Set laser spawn position with offset
        Vector3 laserPosition = transform.position + new Vector3(laser_x_offset, laser_y_offset, 0f);

        // Instantiate the laser
        GameObject laser = Instantiate(LaserPrefab, laserPosition, Quaternion.identity);

        // Add a small log for debugging
        Debug.Log("Laser Fired!");

        // Destroy the laser after 3 seconds
        Destroy(laser, 3f);
    }
}
