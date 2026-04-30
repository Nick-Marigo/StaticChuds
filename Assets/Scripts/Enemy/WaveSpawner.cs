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
    [SerializeField]
    int countdown = 3;

    private List<Enemy> enemies; 
    private Level selectedLevel;
    private int currentWave;
    private int totalRemainingSpawns = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = EnemyLoader.GetEnemies();
        GameManager.Instance.waveStats = waveStats;
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

        if (currentWave <= selectedLevel.waves)
        {
            StartCoroutine(SpawnWave());
            return;
        }
    }

    IEnumerator SpawnWave()
    {
        // Start countdown
        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = countdown;
        for (int i = countdown; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }
        GameManager.Instance.state = GameManager.GameState.INWAVE;

        Spawn[] spawns = selectedLevel.spawns;
        int numEnemyTypes = spawns.Length;

        /* For each type of enemy, start a coroutine that spawns that enemy at it's
           unique tempo */
        for (int i = 0; i < numEnemyTypes; i++) {
            Spawn spawn = spawns[i];
            int numOfType = spawn.CalculateSpawnCount(currentWave); 
            totalRemainingSpawns += numOfType;
            StartCoroutine(SpawnSequences(spawn, numOfType));
        }

        // Do not check the win wave condition until all the enemies have been spawned
        yield return new WaitWhile(() => totalRemainingSpawns > 0);

        //Debug.Log("Wave: " + currentWave + " | Enemy: " + spawn.enemy + " | Group size: " + spawn.sequence[sequenceIndex] + " | Already Spawned: " + spawned + " / " + spawnCount);

        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        if(currentWave == selectedLevel.waves)
        {
            GameManager.Instance.state = GameManager.GameState.GAMEOVER;
        }
        waveStats.EndWave();
        waveStats.DisplayStats();
        if(GameManager.Instance.state == GameManager.GameState.INWAVE) {
            GameManager.Instance.state = GameManager.GameState.WAVEEND;
        }
    }

    /* Called for each enemy, this does the heavy lifting of spawning each enemy on
     * tempo at the correct amounts per sequence step. It also keeps track of how
     * many are left to spawn using remainingSpawns */
    IEnumerator SpawnSequences(Spawn spawn, int remainingSpawns) {
        Enemy enemyType = enemies.Where(enemy => enemy.name == spawn.enemy).FirstOrDefault();
        if (enemyType == null)
            Debug.Log("Failed to find enemy type: " + spawn.enemy);
        int currentSequence = 0;

        // Keep stepping through sequences until all enemies of this type are spawned
        while (remainingSpawns > 0) {
            // Spawn every enemy of this sequence
            int sequenceIndex = currentSequence++ % spawn.sequence.Length;
            int desiredSpawn = spawn.sequence[sequenceIndex];
            int realSpawn = desiredSpawn < remainingSpawns ? 
                desiredSpawn : remainingSpawns;
            for (int i = 0; i < realSpawn; i++) {
                enemySpawner.SpawnEnemy(enemyType, spawn, currentWave);
                remainingSpawns--;
                totalRemainingSpawns--;
            }
            // Wait for next sequence
            yield return new WaitForSeconds(spawn.delay);
        }
    }
}
