using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class EnemyLoader
{
    public List<EnemyData> enemyDataList;

    public List<EnemyData> LoadEnemies(string json)
    {
        enemyDataList = JsonConvert.DeserializeObject<List<EnemyData>>(json);

        if(enemyDataList == null)
        {
            Debug.Log("Failed to deserialize enemies");
            return new List<EnemyData>();
        }

        Debug.Log("Loaded " + enemyDataList.Count + " enemies");

        foreach (EnemyData enemy in enemyDataList)
        {
            Debug.Log("Enemy: " + enemy.name + ", Sprite: " + enemy.sprite + ", HP: " + enemy.hp + ", Speed: " + enemy.speed + ", Damage: " + enemy.damage);
        }

        return enemyDataList;
    }
}
