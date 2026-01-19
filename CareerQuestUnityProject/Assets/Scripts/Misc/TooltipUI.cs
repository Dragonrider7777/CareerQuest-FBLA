using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    public TextMeshProUGUI tooltipText;
    public RectTransform rectTransform;
    public Canvas canvas;

    public Vector2 cursorOffset = new Vector2(16, -16);
    public Vector2 padding = new Vector2(10, 10);

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Instance = this;
        Hide();
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        FollowCursor();
    }

    public void Show(string text)
    {
        tooltipText.text = text;
        gameObject.SetActive(true);

        // Force layout update so size is correct immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void FollowCursor()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue() + cursorOffset;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePos,
            null,
            out Vector2 localPos
        );

        rectTransform.localPosition = localPos;

        ClampToCanvas();
    }

    void ClampToCanvas()
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        RectTransform canvasRect = canvas.transform as RectTransform;
        Vector3[] canvasCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners);

        Vector3 offset = Vector3.zero;

        if (corners[0].x < canvasCorners[0].x + padding.x)
            offset.x += (canvasCorners[0].x - corners[0].x) + padding.x;

        if (corners[2].x > canvasCorners[2].x - padding.x)
            offset.x -= (corners[2].x - canvasCorners[2].x) + padding.x;

        if (corners[0].y < canvasCorners[0].y + padding.y)
            offset.y += (canvasCorners[0].y - corners[0].y) + padding.y;

        if (corners[2].y > canvasCorners[2].y - padding.y)
            offset.y -= (corners[2].y - canvasCorners[2].y) + padding.y;

        rectTransform.position += offset;
    }
}
