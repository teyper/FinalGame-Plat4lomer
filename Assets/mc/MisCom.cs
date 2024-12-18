using UnityEngine;

public class MisComp : MonoBehaviour
{
    [Header("Mission Accomplished Settings")]
    [SerializeField] GameObject risingSpritePrefab; // Prefab for the rising sprite
    [SerializeField] float riseSpeed = 2f; // Speed at which the sprite rises
    [SerializeField] float riseDuration = 3f; // Time the sprite takes to rise

    private bool hasTriggered = false; // Prevents multiple triggers

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Prevent retriggering
            InstantiateRisingSprite();
        }
    }

    void InstantiateRisingSprite()
    {
        // Instantiate the rising sprite prefab at the current position
        GameObject risingSprite = Instantiate(risingSpritePrefab, transform.position, Quaternion.identity);

        // Start rising motion
        StartCoroutine(RiseSprite(risingSprite));
    }

    System.Collections.IEnumerator RiseSprite(GameObject sprite)
    {
        float elapsedTime = 0f;

        // Save the starting position
        Vector3 startPosition = sprite.transform.position;

        while (elapsedTime < riseDuration)
        {
            // Calculate the new position based on rise speed
            sprite.transform.position = startPosition + Vector3.up * (riseSpeed * elapsedTime);

            elapsedTime += Time.deltaTime; // Increment time
            yield return null; // Wait for the next frame
        }

        Destroy(sprite); // Optionally destroy the sprite after rising
    }
}
