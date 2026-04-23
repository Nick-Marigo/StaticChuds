using Newtonsoft.Json;
using System.Collections.Generic;

public class EnemyLoader
{
    public int LoadEnemies(string json, out List<EnemyData> enemyData)
    {
        enemyData = JsonConvert.DeserializeObject<List<EnemyData>>(json);

        if (enemyData == null) {
            return -1;
        }
        return 0;
    }
}
