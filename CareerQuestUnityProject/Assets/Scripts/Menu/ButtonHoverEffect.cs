using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScale : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    private Vector3 originalScale;
    public GameObject border;

    public float hoverScale = 1.05f;
    public float pressedScale = 0.95f;

    void Start()
    {
        originalScale = transform.localScale;
        if (border != null)
        {
            border.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;
        if (border != null)
        {
            border.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        if (border != null)
        {
            border.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = originalScale * pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;
    }
}
