using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI label;
    private LevelSelector levelSelector;
    private string level;

    void Start() {
        levelSelector = FindFirstObjectByType<LevelSelector>();
    }

    // Set label details
    public void SetLevel(string text)
    {
        level = text;
        label.text = text;
    }

    public void StartLevel()
    {
        levelSelector.StartLevel(level);
    }
}
