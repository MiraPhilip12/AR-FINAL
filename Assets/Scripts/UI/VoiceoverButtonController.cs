using UnityEngine;
using UnityEngine.UI;

public class VoiceoverButtonController : MonoBehaviour
{
    private Button button;
    private int carIndex = 0;
    private AudioManager audioManager;

    void Start()
    {
        // Get the button component
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayVoiceover);
        }

        // Find AudioManager
        audioManager = FindObjectOfType<AudioManager>();

        // Find which car this button belongs to
        FindCarIndex();
    }

    void FindCarIndex()
    {
        // Start from parent and go up until we find VehicleIdentifier
        Transform current = transform.parent;
        while (current != null)
        {
            VehicleIdentifier identifier = current.GetComponent<VehicleIdentifier>();
            if (identifier != null)
            {
                carIndex = identifier.carIndex;
                Debug.Log("Voiceover button attached to car index: " + carIndex);
                return;
            }
            current = current.parent;
        }

        Debug.LogWarning("VoiceoverButton could not find VehicleIdentifier on parent!");
    }

    void PlayVoiceover()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogError("AudioManager not found!");
                return;
            }
        }

        audioManager.PlayVoiceover(carIndex);

        // Optional: Visual feedback - briefly change button color
        if (button != null)
        {
            StartCoroutine(FlashButton());
        }
    }

    System.Collections.IEnumerator FlashButton()
    {
        ColorBlock colors = button.colors;
        Color originalColor = colors.normalColor;
        colors.normalColor = Color.green;
        button.colors = colors;

        yield return new WaitForSeconds(0.5f);

        colors.normalColor = originalColor;
        button.colors = colors;
    }
}