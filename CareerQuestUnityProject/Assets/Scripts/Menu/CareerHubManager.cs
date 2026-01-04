using UnityEngine;

public class CareerHubManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCareer(string careerName)
    {
        GameManager.Instance.selectedCareer = careerName;
        Debug.Log("Career selected: " + careerName);
    }
}
