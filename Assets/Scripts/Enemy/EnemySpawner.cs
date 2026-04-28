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
    public GameObject enemy;
    public SpawnPoint[] SpawnPoints;

    private List<Enemy> enemies;
    private Level selectedLevel;
    private int currentWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO move this enemyLoader
        TextAsset enemyJson = Resources.Load<TextAsset>("enemies");
        if (enemyJson == null)
        {
            Debug.Log("Failed to load enemies from json");
            return;
        }

        string enemyJsonText = enemyJson.text;

        EnemyLoader enemyLoader = new EnemyLoader();
        int status = enemyLoader.LoadEnemies(enemyJsonText, out enemies);
        if (status == -1) {
            Debug.Log("Failed to load enemies from JSON");
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(Level selectedLevel) {
        this.selectedLevel = selectedLevel;
        currentWave = 1;
        StartCoroutine(SpawnWave());
    }

    public void NextWave()
    {
        currentWave++;

        if(currentWave <= selectedLevel.waves)
        {
            StartCoroutine(SpawnWave());
            return;
        } else
        {
            //GameManager.Instance.state = GameManager.GameState.LEVELEND;
        }
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

        Dictionary<string, int> variables = new Dictionary<string, int>();
        variables["wave"] = currentWave;
        foreach (Spawn spawn in selectedLevel.spawns)
        {
            Enemy enemyType = enemies.Where(enemy => enemy.name == spawn.enemy).FirstOrDefault();
            if (enemyType == null)
            {
                Debug.Log("Failed to find enemy type: " + spawn.enemy);
                continue;
            }

            // Calculate hp for the enemyType. I made this simple for now, but I dont like passings hp as another parameter because if we ever need to pass speed or damage then we have to keep adding more parameters. I think we should do something like Enemy enemyTemp and add json and any other varaibles that would be calculated and then just pass that through. 
            int hp;
            if (!string.IsNullOrEmpty(spawn.hp))
            {
                variables["base"] = enemyType.hp;
                hp = RPNEvaluator.RPNEvaluator.Evaluate(spawn.hp, variables);
            } else
            {
                hp = enemyType.hp;
            }

            int spawnCount = RPNEvaluator.RPNEvaluator.Evaluate(spawn.count, variables);

            int spawned = 0;
            int sequenceIndex = 0;
            //Could probably add the check for sequence and delay to the level loader. In the assignment it says if its not specified then sequence should default to 1 and delay should default to 2.
            if (spawn.sequence == null || spawn.sequence.Length == 0)
            {
                spawn.sequence = new int[] { 1 };
            }
            if (spawn.delay <= 0)
            {
                spawn.delay = 2;
            }

            while (spawned < spawnCount)
            {
                for (int i = 0; i < spawn.sequence[sequenceIndex] && spawned < spawnCount; i++)
                {
                    SpawnEnemy(enemyType, hp);
                    spawned++;
                }

                //Debug.Log("Wave: " + currentWave + " | Enemy: " + spawn.enemy + " | Group size: " + spawn.sequence[sequenceIndex] + " | Already Spawned: " + spawned + " / " + spawnCount);

                sequenceIndex = (sequenceIndex + 1) % spawn.sequence.Length;

                yield return new WaitForSeconds(spawn.delay);
            }
    
        }

        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    void SpawnEnemy(Enemy enemyType, int hp)
    {
        SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(enemyType.sprite);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(hp, Hittable.Team.MONSTERS, new_enemy);
        en.speed = enemyType.speed;
        GameManager.Instance.AddEnemy(new_enemy);
    }
}
