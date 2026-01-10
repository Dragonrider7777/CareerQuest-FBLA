using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int totalDays = 10; // Total number of days in the game
    public int currentDay = 1; // Start at day 1
    public int maxHoursPerDay = 8; // Working hours in a day
    public int remainingHours; // Hours left in the current day
    public TextMeshProUGUI dayText; // UI element to display current day
    public TextMeshProUGUI hoursText; // UI element to display remaining hours
    public TextMeshProUGUI notEnoughHoursText; // UI element to display "Not Enough Hours" message
    public TaskManager taskManager; // Reference to the TaskManager script
    public Transform taskContainer; // Container holding the task cards

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        remainingHours = maxHoursPerDay; // Reset hours for the new day
        dayText.text = "Day " + currentDay;
        hoursText.text = "Hours Left: " + remainingHours;
        taskManager.SpawnTasks(); // Spawn tasks for the day
    }

    public void EndDay()
    {
        currentDay++;

        if (currentDay > totalDays)
        {
            Debug.Log("All days completed! Game Over.");
            // Implement end-of-game logic here
        }
        else
        {
            StartDay(); // Start the next day
        }
    }

    public void SubtractHours(int hours)
    {
        int placeholder = remainingHours - hours;

        if (placeholder == 0)
        {
            EndDay(); // No hours left --> end the day
        }
        else if (placeholder < 0)
        {
            notEnoughHoursText.gameObject.SetActive(true);
        }
        else if (placeholder > 0)
        {
            remainingHours = placeholder;
            hoursText.text = "Hours Left: " + remainingHours;
            notEnoughHoursText.gameObject.SetActive(false);
            taskManager.SpawnTasks(); // Refresh tasks for the remaining hours
        }
    }
}
