using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Task Setup")]
    public GameObject taskCardPrefab;
    public Transform container;
    public List<TaskData> taskPool;
    public int tasksPerDay = 3;

    [Header("Managers")]
    public DayManager dayManager;
    public StressManager stressManager; // Reference to the StressManager script

    [Header("Task Settings")]
    public TaskData lastTask;
    public List<TaskData> currentTasks = new List<TaskData>(); // List to hold current 3 tasks on screen

    [Header("Tracking")]
    public int dailyTaskPoints = 0;

    public void SpawnTasks()
    {
        ClearUI();
        currentTasks.Clear();

        List<TaskData> tempPool = new(taskPool);

        // Pick random tasks from the pool
        if (lastTask != null && tempPool.Contains(lastTask))
        {
            tempPool.Remove(lastTask);
        }

        // Pick random tasks from pool
        for (int i = 0; i < tasksPerDay && tempPool.Count > 0; i++)
        {
            int index = Random.Range(0, tempPool.Count);
            TaskData selectedTask = tempPool[index];
            tempPool.RemoveAt(index); // Remove to avoid duplicates

            currentTasks.Add(selectedTask);

            // Instantiate task card UI
            GameObject taskCard = Instantiate(taskCardPrefab, container);
            taskCard.GetComponent<TaskCardUI>().Initialize(selectedTask, this, dayManager);
        }
    }

    // Called by TaskCardUI when a task is clicked
    public void OnTaskClicked(TaskData clickedTask)
    {
        lastTask = clickedTask;

        ApplyStressFromIgnoredTasks(clickedTask);

        dailyTaskPoints += GetTaskPoints(clickedTask);
        stressManager.ReduceStress(GetStressReduction(clickedTask));

        currentTasks.Remove(clickedTask);

        // Spawn new tasks if hours remain
        if (dayManager != null && dayManager.GetRemainingHours() > 0)
        {
            SpawnTasks();
        }
    }

    int GetTaskPoints(TaskData task)
    {
        return task.urgency switch
        {
            UrgencyLevel.Low => 1,
            UrgencyLevel.Medium => 0,
            UrgencyLevel.High => -2,
            UrgencyLevel.Critical => -3,
            _ => 0
        };
    }

    int GetStressReduction(TaskData task)
    {
        return task.urgency switch
        {
            UrgencyLevel.Low => 0,
            UrgencyLevel.Medium => 0,
            UrgencyLevel.High => 0,
            UrgencyLevel.Critical => 1,
            _ => 0
        };
    }

    public void ApplyStressFromIgnoredTasks(TaskData clickedTask)
    {
        foreach (var task in currentTasks)
        {
            if (task != clickedTask)
            {
                int stressAmount = task.urgency switch
                {
                    UrgencyLevel.Low => 0,
                    UrgencyLevel.Medium => 1,
                    UrgencyLevel.High => 2,
                    UrgencyLevel.Critical => 3,
                    _ => 1
                };
                stressManager.AddStress(stressAmount);
            }
        }
    }

    public void ResetDailyTaskPoints()
    {
        dailyTaskPoints = 0;
    }

    void ClearUI()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
        
