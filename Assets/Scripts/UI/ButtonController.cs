using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI label;
    private LevelSelector levelSelector;
    private string level;

    private ClassesSelector classesSelector;
    private string selectedClass;

    void Start() {
        levelSelector = FindFirstObjectByType<LevelSelector>();
        classesSelector = FindFirstObjectByType<ClassesSelector>();
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

    public void SetClass(string text)
    {
        selectedClass = text;
        label.text = text;
    }

    public void SelectedClass()
    {
        classesSelector.SelectClass(selectedClass);
    }
}
