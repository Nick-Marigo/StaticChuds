using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class EnemyLoader
{
    private static List<Enemy> enemies;

    public static List<Enemy> GetEnemies() {
        if (enemies == null) {
            enemies = LoadEnemies();
        }
        return enemies;
    }

    private static List<Enemy> LoadEnemies() {
        TextAsset enemyJson = Resources.Load<TextAsset>("enemies");
        if (enemyJson == null)
        {
            Debug.Log("Failed to get enemies json from Resources");
            return null;
        }

        int status = EnemyLoader.JsonToList(enemyJson.text, out enemies);
        if (status == -1) {
            Debug.Log("Failed to load enemies from JSON");
            return null;
        }
        return enemies;
    }

    private static int JsonToList(string json, out List<Enemy> enemyData)
    {
        enemyData = JsonConvert.DeserializeObject<List<Enemy>>(json);

        if (enemyData == null) {
            return -1;
        }
        return 0;
    }
}
