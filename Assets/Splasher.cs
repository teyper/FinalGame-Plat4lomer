using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class splash : MonoBehaviour
{
    [SerializeField] TMP_Text startText; // Reference to the blinking text
    [SerializeField] string MainScene = "Scene"; // Name of the next scene to load

    void Update()
    {
        // Detect if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed. Loading Main Scene...");
            SceneManager.LoadScene("MainScene");
        }

    }
}