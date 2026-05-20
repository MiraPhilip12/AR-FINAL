using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    public GameObject tooltipPrefab;
    private GameObject currentTooltip;
    private TextMeshProUGUI tooltipText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowTooltip(string text, Vector3 position, Canvas canvas)
    {
        if (currentTooltip != null)
            HideTooltip();

        if (tooltipPrefab == null)
        {
            Debug.LogError("Tooltip prefab not assigned!");
            return;
        }

        currentTooltip = Instantiate(tooltipPrefab, canvas.transform);
        tooltipText = currentTooltip.GetComponentInChildren<TextMeshProUGUI>();
        if (tooltipText != null)
            tooltipText.text = text;

        RectTransform rect = currentTooltip.GetComponent<RectTransform>();
        rect.position = Input.mousePosition + new Vector3(15, -15, 0);
    }

    public void HideTooltip()
    {
        if (currentTooltip != null)
            Destroy(currentTooltip);
        currentTooltip = null;
    }
}