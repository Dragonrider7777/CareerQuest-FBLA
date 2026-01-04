using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PresetButtonName : MonoBehaviour
{
    public string buttonName;
    public TextMeshProUGUI label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        label.text = buttonName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
