using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    GameObject enemy;
    private SpawnPoint[][] SpawnPoints;
    private Dictionary<string, Func<GameObject>> spawnSelect;

    private const int GREEN = 0;
    private const int RED = 0;
    private const int BONE = 0;

    void Start() {
        spawnSelect = new Dictionary<string, Func<GameObject>> {
            { "random green", () => { return SpawnPoints[GREEN][Random.Range(0, SpawnPoints[GREEN].Length)]; } },
            { "random red", () => { return SpawnPoints[RED][Random.Range(0, SpawnPoints[RED].Length)]; } },
            { "random bone", () => { return SpawnPoints[BONE][Random.Range(0, SpawnPoints[BONE].Length)]; } },
            { "random", () => { SpawnPoint[] type = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
                                  return type[Random.Range(0, SpawnPoints[type.Length])]; 
                              } },
        };
        SpawnPoints[GREEN] = FindGameObjectsWithTag("GreenSpawn");
        SpawnPoints[RED] = FindGameObjectsWithTag("RedSpawn");
        SpawnPoints[BONE] = FindGameObjectsWithTag("BoneSpawn");
    }

    public void SpawnEnemy(Enemy enemyType, Spawn spawn, int currentWave) {
        SpawnPoint spawn_point = SpawnPoints[0];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = GameObject.Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(enemyType.sprite);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(spawn.CalculateHP(enemyType.hp, currentWave), Hittable.Team.MONSTERS, new_enemy);
        en.damage = spawn.CalculateDamage(enemyType.damage, currentWave);
        en.speed = spawn.CalculateSpeed(enemyType.speed, currentWave);
        en.enemyName = enemyType.name;
        GameManager.Instance.AddEnemy(new_enemy);
        //Debug.Log("Enemy hp:" + en.hp.hp + "Enemy dmg: " + en.damage + "Enemy speed: " + en.speed); 
    }
}
