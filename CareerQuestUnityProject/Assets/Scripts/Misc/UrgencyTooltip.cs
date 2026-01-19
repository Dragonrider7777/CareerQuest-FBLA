using UnityEngine;
using UnityEngine.EventSystems;

public class UrgencyTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UrgencyLevel urgency;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TooltipUI.Instance == null)
        {
            Debug.LogError("TooltipUI.Instance is NULL! Tooltip object missing.");
            return;
        }

        TooltipUI.Instance.Show(GetText());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (TooltipUI.Instance != null)
            TooltipUI.Instance.Hide();
    }

    string GetText()
    {
        return urgency switch
        {
            UrgencyLevel.Low => "Low urgency\nCan be safely delayed.",
            UrgencyLevel.Medium => "Medium urgency\nShould be done soon.",
            UrgencyLevel.High => "High urgency\nConsequences if ignored.",
            UrgencyLevel.Critical => "CRITICAL\nImmediate action required!",
            _ => ""
        };
    }
}
