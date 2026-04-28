using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/* This class is responsible for generating the difficulty buttons and invoking the enemySpawner when a button is pressed.*/
public class LevelSelector : MonoBehaviour {

    private List<Level> levels;
    private GameObject levelSelector;
    //TODO determine if spawner needs to be dynamic
    private EnemySpawner spawner;

    [SerializeField]
    GameObject button;
    
    void Start() {
        levels = LevelLoader.GetLevels();
        spawner = FindFirstObjectByType<EnemySpawner>();
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

        // Hide the level selector
        levelSelector.SetActive(false);
        // TODO this is not nice: we should not have to be required to tell the player directly that the level is starting
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        spawner.StartLevel(selectedLevel);
    }

    void GenerateButtons() {
        int buttonXOffset = 90;
        int buttonYOffset = 50;
        for (int i = 0; i < levels.Count; i++) {
            float xPos = (i%2) == 0 ?
                -buttonXOffset : buttonXOffset;
            float yPos = 90-buttonYOffset*(i/2);
            button = Instantiate(button, levelSelector.transform);
            button.transform.localPosition = new Vector3(xPos, yPos);
            // TODO button.GetComponent<MenuSelectorController>().spawner = this;
            button.GetComponent<ButtonController>().SetLevel(levels[i].name);
        }
    }
}
