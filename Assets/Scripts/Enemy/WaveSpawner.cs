using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    WaveStats waveStats;
    [SerializeField]
    EnemySpawner enemySpawner;

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
            // TODO ask for explanation
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
                    enemySpawner.SpawnEnemy(enemyType, spawn, currentWave);
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

}
