using UnityEngine;

public class StressManager : MonoBehaviour
{
    [Header("Stress Settings")]
    public int maxStress = 100; // Maximum stress level
    public int currentStress = 0; // Current stress level

    [Header("Stress Recovery")]
    public int baseRecoveryAmount = 3; // Stress reduced at the end of each day
    public int recoveryPerUnusedHour = 2; // Additional stress reduced per unused hour

    [Header("UI")]
    public SliderController stressSlider; // Slider to represent stress level visually
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddStress(int amount)
    {
        currentStress = Mathf.Clamp(currentStress + amount, 0, maxStress);
        UpdateUI();

        if (currentStress >= maxStress)
        {
            TriggerBurnout();
        }
    }

    public void ReduceStress(int amount)
    {
        currentStress = Mathf.Clamp(currentStress - amount, 0, maxStress);
        UpdateUI();
    }

    public void EndOfDayRecovery(int amount)
    {
        int recovery = baseRecoveryAmount + (amount * recoveryPerUnusedHour);
        currentStress = Mathf.Clamp(currentStress - recovery, 0, maxStress);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (stressSlider != null)
            stressSlider.UpdateBarStress(currentStress, maxStress);
    }

    void TriggerBurnout()
    {
        Debug.Log("Burnout triggered! Implement burnout consequences here.");
        // Implement burnout logic (e.g., end game, reduce player abilities, etc.)
    }

    public int GetCurrentStress()
    {
        return currentStress;
    }
}
