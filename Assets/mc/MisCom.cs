using UnityEngine;

public class MissionCompleteTrigger : MonoBehaviour
{
    [SerializeField] GameObject missionCompleteSpritePrefab; // Sprite prefab for mission complete
    [SerializeField] AudioClip missionCompleteSound;         // Audio for mission complete
    private AudioSource audioSource;
    private bool missionCompleted = false;                   // Ensure it triggers only once

    private GameManager gMan;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        gMan = FindObjectOfType<GameManager>();
        if (gMan == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !missionCompleted) // Check if it's the player and not already completed
        {
            missionCompleted = true; // Mark mission as completed to prevent multiple triggers

            Debug.Log("Mission Complete Triggered!");

            // Play mission complete sound
            if (missionCompleteSound != null)
            {
                audioSource.PlayOneShot(missionCompleteSound);
            }

            // Instantiate the mission complete sprite
            if (missionCompleteSpritePrefab != null)
            {
                Instantiate(missionCompleteSpritePrefab, transform.position, Quaternion.identity);
            }

            // Notify the GameManager
            if (gMan != null)
            {
                gMan.MissionComplete();
            }
        }
    }
}

