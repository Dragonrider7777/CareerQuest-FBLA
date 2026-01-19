using UnityEngine;

public enum UrgencyLevel { Low, Medium, High, Critical }

[CreateAssetMenu(menuName = "SoftSkills/Task")]
public class TaskData : ScriptableObject
{
    public string taskName;
    public int timeCost;
    public UrgencyLevel urgency;
}
