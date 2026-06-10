using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Camera;

/* This class is responsible for generating the difficulty buttons and invoking the enemySpawner when a button is pressed.*/
public class ClassesSelector : MonoBehaviour {

    Dictionary<string, Classes> classes;
    GameObject classSelector;

    PlayerInstance playerController;
    [SerializeField] GameObject difficultySelector;

    [SerializeField]
    GameObject button;
    
    void Start() 
    {
        classes = ClassesLoader.GetClasses();
        playerController = FindFirstObjectByType<PlayerInstance>();
        classSelector = this.gameObject;
        GenerateButtons();
        CameraController.Instance.UpdateCamera();
    }

    public void SelectClass(string selectedClass) 
    {
        Debug.Log(selectedClass);
        if (!classes.ContainsKey(selectedClass))
        {
            Debug.Log($"Failed to find selected class: {selectedClass}");
            return;
        }

        playerController.InitPlayer(classes[selectedClass]);
        classSelector.SetActive(false);
        difficultySelector.SetActive(true);
    }

    /*
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
    */

    void GenerateButtons() {
        float yScale = 1f / classes.Count;
        for (int i = 0; i < classes.Count; i++) {
            float xOffset = (i%2) == 0 ?
                0.1f : 0.55f;
            button = Instantiate(button, classSelector.transform);
            ButtonScaler scaler = button.GetComponent<ButtonScaler>();
            scaler.yScale = yScale;
            scaler.xScale = 0.35f;
            scaler.yOffset = 0.1f + (yScale + 0.05f) * (i / 2);
            scaler.xOffset = xOffset;
            //button.transform.localPosition = new Vector3(xPos, yPos);
            // TODO button.GetComponent<MenuSelectorController>().spawner = this;
            button.GetComponent<ButtonController>().SetClass(classes.ElementAt(i).Key);
        }
    }
}
