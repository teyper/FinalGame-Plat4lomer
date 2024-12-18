using UnityEngine;

public class MissionComplete : MonoBehaviour
{
    [SerializeField] GameObject missionCompleteDisplay; // Assign the "Mission Complete" UI GameObject in Inspector
    [SerializeField] AudioClip missionCompleteSound;    // Assign the mission complete sound in Inspector
    [SerializeField] string splashScreenSceneName = "Splasher"; // Splash screen scene name

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if missing
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player reaches the mission complete area
        {
            Debug.Log("Mission Complete!");
            if (missionCompleteDisplay != null)
            {
                missionCompleteDisplay.SetActive(true); // Show Mission Complete display
            }

            //if (missionCompleteSound != null)
           // {
           //     audioSource.PlayOneShot(missionCompleteSound); // Play mission complete sound
            //}

            Invoke("ReturnToSplashScreen", 5f); // Wait 5 seconds, then return to splash screen
        }
    }

    void ReturnToSplashScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(splashScreenSceneName); // Load splash screen
    }
}
