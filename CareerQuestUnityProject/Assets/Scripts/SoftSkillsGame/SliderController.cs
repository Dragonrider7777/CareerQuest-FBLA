using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public Slider slider;

    public void UpdateBarHours(int currentHours, int maxHours)
    {
        slider.maxValue = maxHours;
        slider.value = currentHours;
        valueText.text = "Hours: " + currentHours;
    }

    public void ResetBarHours(int maxHours)
    {
        slider.maxValue = maxHours;
        slider.value = maxHours;
        valueText.text = "Hours: " + maxHours;
    }

    public void UpdateBarStress(int currentStress, int maxStress)
    {
        slider.maxValue = maxStress;
        slider.value = currentStress;
        valueText.text = "Stress Level: " + currentStress;
    }

    public void ResetBarStress(int maxStress)
    {
        slider.maxValue = maxStress;
        slider.value = maxStress;
        valueText.text = "Stress Level: " + maxStress;
    }
}
