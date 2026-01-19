using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    [Header("Day Settings")]
    public int totalDays = 10; // Total number of days in the game
    public int currentDay = 1; // Start at day 1
    public int maxHoursPerDay = 8; // Working hours in a day
    public int remainingHours; // Hours left in the current day

    [Header("UI References")]
    public Button endDayButton; // Button to manually end the day
    public TextMeshProUGUI dayText; // UI element to display current day
    public TextMeshProUGUI notEnoughHoursText; // UI element to display "Not Enough Hours" message
    public SliderController sliderController; // Reference to the SliderController script

    [Header("Score")]
    public int score;

    [Header("Managers")]
    public TaskManager taskManager; // Reference to the TaskManager script
    public StressManager stressManager; // Reference to the StressManager script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (endDayButton != null)
            endDayButton.onClick.AddListener(EndDay);

        StartDay();
    }

    public void StartDay()
    {
        remainingHours = maxHoursPerDay; // Reset hours for the new day
        
        if (dayText != null)
        {
            dayText.text = "Day " + currentDay;
        }
        if (notEnoughHoursText != null)
            notEnoughHoursText.gameObject.SetActive(false);

        sliderController.ResetBarHours(maxHoursPerDay); // Reset the slider UI
        taskManager.SpawnTasks(); // Spawn tasks for the day
    }

    public void SubtractHours(int hours)
    {
        if (remainingHours - hours < 0)
        {
            if (notEnoughHoursText != null)
                notEnoughHoursText.gameObject.SetActive(true);
            return; // Not enough hours
        }

        remainingHours -= hours; // Deduct hours
        sliderController.UpdateBarHours(remainingHours, maxHoursPerDay);
        
        if (notEnoughHoursText != null)
            notEnoughHoursText.gameObject.SetActive(false);
        
        if (remainingHours > 0)
        {
            taskManager.SpawnTasks(); // Spawn new tasks if hours remain
        }
        else
        {
            EndDay(); // No hours left --> end the day
        }
    }
    
    public int GetRemainingHours()
    {
        return remainingHours;
    }

    public void EndDay()
    {
        int unusedHours = remainingHours;
        CalculateDailyScore();
        // Notify StressManager for end-of-day stress recovery
        if (stressManager != null)
            stressManager.EndOfDayRecovery(unusedHours);

        ResetDailyTaskPoints();
        currentDay++;

        if (currentDay > totalDays)
        {
            Debug.Log("All days completed! Game Over.");
            return;
        }

        StartDay(); // Start the next day
    }

    public int CalculateDailyScore()
    {
        int stress = stressManager != null ? stressManager.GetCurrentStress() : 0;
        int taskPoints = Mathf.Max(1, taskManager.dailyTaskPoints); // Avoid division by zero

        int dailyScore = (taskPoints == 0) ? stress + 10 : stress / taskPoints;
    
        score += dailyScore;
        Debug.Log($"Day {currentDay} Score: {dailyScore} | Total Score: {score}");
        
        return dailyScore;
    }

    void ResetDailyTaskPoints()
    {
        taskManager.dailyTaskPoints = 0;
    }
}