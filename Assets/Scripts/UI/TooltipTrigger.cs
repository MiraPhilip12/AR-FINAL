using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tooltipText = "Feature description";
    public float delay = 0.5f;

    private float timer = 0f;
    private bool isPointerOver = false;

    void Update()
    {
        if (isPointerOver)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                Canvas canvas = GetComponentInParent<Canvas>();
                if (canvas != null)
                    TooltipManager.Instance.ShowTooltip(tooltipText, transform.position, canvas);
                isPointerOver = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        timer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        timer = 0f;
        TooltipManager.Instance.HideTooltip();
    }
}