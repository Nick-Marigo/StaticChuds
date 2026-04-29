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
    public WaveStats waveStats;

    private List<Enemy> enemies; 
    private Level selectedLevel;
    private int currentWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = EnemyLoader.GetEnemies();
        GameManager.Instance.waveStats = waveStats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(Level selectedLevel) {
        this.selectedLevel = selectedLevel;
        currentWave = 1;
        StartCoroutine(SpawnWave());
        //waveStats.StartLevel();
        waveStats.StartWave(currentWave);
    }

    public void NextWave()
    {
        currentWave++;
        waveStats.StartWave(currentWave);

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

        foreach (Spawn spawn in selectedLevel.spawns)
        {
            Enemy enemyType = enemies.Where(enemy => enemy.name == spawn.enemy).FirstOrDefault();
            if (enemyType == null)
            {
                Debug.Log("Failed to find enemy type: " + spawn.enemy);
                continue;
            }

            int spawnCount = spawn.CalculateSpawnCount(enemyType.hp, currentWave);

            int spawned = 0;
            int sequenceIndex = 0;

            while (spawned < spawnCount)
            {
                for (int i = 0; i < spawn.sequence[sequenceIndex] && spawned < spawnCount; i++)
                {
                    SpawnEnemy(enemyType, spawn);
                    spawned++;
                }

                //Debug.Log("Wave: " + currentWave + " | Enemy: " + spawn.enemy + " | Group size: " + spawn.sequence[sequenceIndex] + " | Already Spawned: " + spawned + " / " + spawnCount);

                sequenceIndex = (sequenceIndex + 1) % spawn.sequence.Length;

                yield return new WaitForSeconds(spawn.delay);
            }
    
        }

        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        waveStats.EndWave();
        waveStats.DisplayStats();
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    void SpawnEnemy(Enemy enemyType, Spawn spawn)
    {
        SpawnPoint spawn_point = SpawnPoints[0];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(enemyType.sprite);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(spawn.CalculateHP(enemyType.hp, currentWave), Hittable.Team.MONSTERS, new_enemy);
        en.damage = spawn.CalculateDamage(enemyType.damage, currentWave);
        en.speed = spawn.CalculateSpeed(enemyType.speed, currentWave);
        GameManager.Instance.AddEnemy(new_enemy);
        //Debug.Log("Enemy hp:" + en.hp.hp + "Enemy dmg: " + en.damage + "Enemy speed: " + en.speed); 
    }
}
