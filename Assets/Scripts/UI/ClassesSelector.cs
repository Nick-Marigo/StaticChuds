using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/* This class is responsible for generating the difficulty buttons and invoking the enemySpawner when a button is pressed.*/
public class ClassesSelector : MonoBehaviour {

    Dictionary<string, Classes> classes;
    GameObject classSelector;

    PlayerController playerController;
    [SerializeField] GameObject difficultySelector;

    [SerializeField]
    GameObject button;
    
    void Start() 
    {
        classes = ClassesLoader.GetClasses();
        playerController = FindFirstObjectByType<PlayerController>();
        classSelector = this.gameObject;
        GenerateButtons();
    }

    public void SelectClass(string selectedClass) 
    {
        if (!classes.ContainsKey(selectedClass))
        {
            Debug.Log($"Failed to find selected class: {selectedClass}");
            return;
        }

        playerController.InitPlayer(classes[selectedClass]);
        classSelector.SetActive(false);
        difficultySelector.SetActive(true);
    }

    void GenerateButtons() 
    {
        int buttonYOffset = 50;
        int startY = 50;
        for (int i = 0; i < classes.Count; i++) 
        {
            float xPos = -50;
            float yPos = startY -buttonYOffset*i;
            GameObject newButton = Instantiate(button, classSelector.transform);
            newButton.transform.localPosition = new Vector3(xPos, yPos);
            newButton.GetComponent<ButtonController>().SetClass(classes.ElementAt(i).Key);
        }
    }
}
