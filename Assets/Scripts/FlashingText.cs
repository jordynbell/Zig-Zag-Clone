using System.Collections;
using TMPro;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public float flashInterval = .5f; // Flash interval in seconds

    private bool isFlashing = true;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FlashText());
    }

    void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isFlashing = false; // Stop flashing
            textMeshPro.enabled = false;
        }
    }

    IEnumerator FlashText()
    {
        while (isFlashing)
        {
            // Toggle the visibility of the text
            textMeshPro.enabled = !textMeshPro.enabled;

            // Wait for the specified interval
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
