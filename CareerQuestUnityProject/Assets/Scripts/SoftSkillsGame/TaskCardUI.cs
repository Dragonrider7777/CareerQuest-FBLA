using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskCardUI : MonoBehaviour
{
    public TextMeshProUGUI taskNameText;
    public TextMeshProUGUI timeCostText;
    public Button actionButton;
    public TaskData taskData;
    public DayManager dayManager; // Reference to DayManager to signal end of day

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (taskData != null)
        {
            UpdateUI();
        }

        actionButton.onClick.AddListener(OnTaskClicked);
    }

    public void UpdateUI()
    {
        if (taskData != null)
        {
            taskNameText.text = taskData.taskName;
            timeCostText.text = taskData.timeCost + " hrs";
        }
    }
    void OnTaskClicked()
    {
        actionButton.interactable = false;

        if (dayManager != null)
        {
            dayManager.SubtractHours(taskData.timeCost);
            Destroy(gameObject);
        }
    }
}
