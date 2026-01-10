using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject taskCardPrefab;
    public DayManager dayManager;
    public Transform container;
    public List<TaskData> taskPool;
    public int tasksPerDay = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnTasks();
    }

    public void SpawnTasks()
    {
        // Clear existing tasks first
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        // Pick random tasks from the pool
        List<TaskData> tempPool = new List<TaskData>(taskPool);
        for (int i = 0; i < tasksPerDay && tempPool.Count > 0; i++)
        {
            int index = Random.Range(0, tempPool.Count);
            TaskData selectedTask = tempPool[index];
            tempPool.RemoveAt(index); // Remove to avoid duplicates

            // Instantiate task card UI
            GameObject taskCard = Instantiate(taskCardPrefab, container);
            TaskCardUI cardUI = taskCard.GetComponent<TaskCardUI>();
            cardUI.taskData = selectedTask;
            cardUI.UpdateUI();

            cardUI.dayManager = dayManager;
        }
    }
}
