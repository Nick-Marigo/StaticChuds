using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Runtime.Versioning;

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;
    public SpawnPoint[] SpawnPoints;

    private List<Enemy> enemies;
    private List<Level> levels;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset enemyJson = Resources.Load<TextAsset>("enemies");
        if (enemyJson == null)
        {
            Debug.Log("Failed to load enemies from json");
            return;
        }

        TextAsset levelJson = Resources.Load<TextAsset>("levels");
        if (levelJson == null)
        {
            Debug.Log("Failed to load levels from json");
            return;
        }

        string enemyJsonText = enemyJson.text;
        string levelJsonText = levelJson.text;

        EnemyLoader enemyLoader = new EnemyLoader();
        int status = enemyLoader.LoadEnemies(enemyJsonText, out enemies);
        if (status == -1) {
            Debug.Log("Failed to load enemies from JSON");
            return;
        }

        LevelLoader levelLoader = new LevelLoader();
        status = levelLoader.LoadLevels(levelJsonText, out levels);
        if (status == -1) {
            Debug.Log("Failed to load levels from JSON");
            return;
        }

        // Generate all the buttons
        int buttonXOffset = 90;
        int buttonYOffset = 50;
        string[] modes = {"Easy", "Medium", "Hard"};
        for (int i = 0; i < modes.Length; i++) {
            float xPos = (i%2) == 0 ?
                -buttonXOffset : buttonXOffset;
            float yPos = 90-buttonYOffset*(i/2);
            button = Instantiate(button, level_selector.transform);
            button.transform.localPosition = new Vector3(xPos, yPos);
            button.GetComponent<MenuSelectorController>().spawner = this;
            button.GetComponent<MenuSelectorController>().SetLevel(modes[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(string levelname)
    {


        level_selector.gameObject.SetActive(false);
        // this is not nice: we should not have to be required to tell the player directly that the level is starting
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        StartCoroutine(SpawnWave());
    }

    public void NextWave()
    {
        StartCoroutine(SpawnWave());
    }


    IEnumerator SpawnWave()
    {
        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = 3;
        // Start countdown
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }
        GameManager.Instance.state = GameManager.GameState.INWAVE;
        for (int i = 0; i < levels[1].waves; ++i)
        {
            yield return SpawnEnemy();
        }
        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    IEnumerator SpawnEnemy()
    {
        SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(0);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(50, Hittable.Team.MONSTERS, new_enemy);
        en.speed = 10;
        GameManager.Instance.AddEnemy(new_enemy);
        yield return new WaitForSeconds(0.5f);
    }
}
