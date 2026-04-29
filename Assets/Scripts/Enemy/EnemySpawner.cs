using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    GameObject enemy;
    [SerializeField]
    SpawnPoint[] SpawnPoints;

    public void SpawnEnemy(Enemy enemyType, Spawn spawn, int currentWave)
        
    {
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
