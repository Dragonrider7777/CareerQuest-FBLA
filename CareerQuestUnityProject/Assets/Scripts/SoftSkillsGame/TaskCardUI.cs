using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskCardUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI taskNameText;
    public TextMeshProUGUI timeCostText;
    public Image urgencyIndicator;
    public Button actionButton;

    [Header("Runtime References")]
    public TaskData taskData;
    public TaskManager taskManager;
    public DayManager dayManager; // Reference to DayManager to signal end of day

    [Header("Urgency Colors")]
    [SerializeField] private Color lowColor = Color.green;
    [SerializeField] private Color mediumColor = Color.yellow;
    [SerializeField] private Color highColor = Color.orange;
    [SerializeField] private Color criticalColor = Color.red;

    public void Initialize(TaskData data, TaskManager manager, DayManager day)
    {
        taskData = data;
        taskManager = manager;
        dayManager = day;
        
        UpdateUI();

        if (actionButton != null)
        {
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(OnClicked);
        }
    }

    public void UpdateUI()
    {
        if (taskData == null) return;
        
        if (taskNameText != null)
        {
            taskNameText.text = taskData.taskName;
        }
        if (timeCostText != null)
        {
            timeCostText.text = taskData.timeCost + " hrs";
        }

        UpdateUrgencyColor();
        UpdateTooltip();
        RefreshAvailability();
    }

    void UpdateUrgencyColor()
    {
        if (urgencyIndicator == null) return;

        urgencyIndicator.color = taskData.urgency switch
        {
            UrgencyLevel.Low => lowColor,
            UrgencyLevel.Medium => mediumColor,
            UrgencyLevel.High => highColor,
            UrgencyLevel.Critical => criticalColor,
            _ => Color.white
        };
    }

    void UpdateTooltip()
    {
        if (urgencyIndicator == null) return;

        var tooltip = urgencyIndicator.GetComponent<UrgencyTooltip>();
        if (tooltip != null)
        {
            tooltip.urgency = taskData.urgency;
        }
    }

    void OnClicked()
    {
        if (taskManager == null || taskData == null || dayManager == null)
            return;

        // Notify TaskManager (applies stress for other tasks)
        taskManager.OnTaskClicked(taskData);

        // Subtract hours from DayManager
        dayManager.SubtractHours(taskData.timeCost);
    }
    
    public void RefreshAvailability()
    {
        if (actionButton == null || dayManager == null) return;

        actionButton.interactable = taskData.timeCost <= dayManager.GetRemainingHours();
    }
}