using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    GameObject enemy;
    private GameObject[][] SpawnPoints;
    private Dictionary<string, System.Func<GameObject>> spawnSelect;

    private const int GREEN = 0;
    private const int RED = 1;
    private const int BONE = 2;

    void Start() {
        // Spawnpoints is a 2d array which collects all the spawnpoints of same types into their own subarrays.
        // When you add a new spawnpoint into the scene, make sure you tag it appropiately
        SpawnPoints = new GameObject[3][];
        SpawnPoints[GREEN] = GameObject.FindGameObjectsWithTag("GreenSpawn");
        SpawnPoints[RED] = GameObject.FindGameObjectsWithTag("RedSpawn");
        SpawnPoints[BONE] = GameObject.FindGameObjectsWithTag("BoneSpawn");


        // The spawn location property is used to index into the spawnSelect dictionary, which returns a spawnPoint game object
        spawnSelect = new Dictionary<string, System.Func<GameObject>> {
            { "random green", () => { return SpawnPoints[GREEN][Random.Range(0, SpawnPoints[GREEN].Length)]; } },
            { "random red", () => { return SpawnPoints[RED][Random.Range(0, SpawnPoints[RED].Length)]; } },
            { "random bone", () => { return SpawnPoints[BONE][Random.Range(0, SpawnPoints[BONE].Length)]; } },
            { "random", () => { GameObject[] type = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
                                  return type[Random.Range(0, type.Length)]; 
                              } },
        };
    }

    public void SpawnEnemy(Enemy enemyType, Spawn spawn, int currentWave) {
        GameObject spawn_point = spawnSelect[spawn.location]();
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
