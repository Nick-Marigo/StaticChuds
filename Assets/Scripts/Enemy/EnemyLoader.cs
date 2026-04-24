using Newtonsoft.Json;
using System.Collections.Generic;

public class EnemyLoader
{
    public int LoadEnemies(string json, out List<Enemy> enemyData)
    {
        enemyData = JsonConvert.DeserializeObject<List<Enemy>>(json);

        if (enemyData == null) {
            return -1;
        }
        return 0;
    }
}
