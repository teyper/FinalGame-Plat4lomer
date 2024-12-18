using System.Collections; // Required for IEnumerator and coroutines
using UnityEngine;
using TMPro;

public class blink : MonoBehaviour
{
    [SerializeField] float blinkInterval = 0.5f; // Time between blinks
    TMP_Text textComponent;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>(); // Get the TextMeshPro component
        StartCoroutine(BlinkText()); // Start the blinking coroutine
    }

    IEnumerator BlinkText()
    {
        while (true) // Infinite loop for blinking
        {
            textComponent.enabled = !textComponent.enabled; // Toggle text visibility
            yield return new WaitForSeconds(blinkInterval); // Wait for the interval
        }
    }
}
