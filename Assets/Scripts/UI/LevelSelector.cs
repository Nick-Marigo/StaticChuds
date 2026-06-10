using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/* This class is responsible for generating the difficulty buttons and invoking the enemySpawner when a button is pressed.*/
public class LevelSelector : MonoBehaviour {

    private List<Level> levels;
    private GameObject levelSelector;
    //TODO determine if spawner needs to be dynamic
    private WaveSpawner spawner;

    [SerializeField]
    GameObject button;
    [SerializeField]
    GameObject wave;
    
    void Start() {
        levels = LevelLoader.GetLevels();
        spawner = FindFirstObjectByType<WaveSpawner>();
        levelSelector = this.gameObject;
        GenerateButtons();
    }

    public void StartLevel(string level) {
        // Get the correct Level obj associated with the calling button's text
        Level selectedLevel = levels.Where(curLevel => curLevel.name == level).FirstOrDefault();
        if (selectedLevel == null)
        {
            Debug.Log($"Failed to find selected level: {level}");
            return;
        }

        // Show wave text
        wave.SetActive(true);
        spawner.StartLevel(selectedLevel);
    }

    void GenerateButtons() {
        float yScale = 1f / levels.Count;
        for (int i = 0; i < levels.Count; i++) {
            float xOffset = (i%2) == 0 ?
                0.1f : 0.55f;
            button = Instantiate(button, levelSelector.transform);
            ButtonScaler scaler = button.GetComponent<ButtonScaler>();
            scaler.yScale = yScale;
            scaler.xScale = 0.35f;
            scaler.yOffset = 0.1f + (yScale + 0.05f) * (i / 2);
            scaler.xOffset = xOffset;
            //button.transform.localPosition = new Vector3(xPos, yPos);
            // TODO button.GetComponent<MenuSelectorController>().spawner = this;
            button.GetComponent<ButtonController>().SetLevel(levels[i].name);
        }
    }
}
