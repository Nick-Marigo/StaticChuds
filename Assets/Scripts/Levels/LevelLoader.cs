using Newtonsoft.Json;
using System.Collections.Generic;

public class LevelLoader
{
    public int LoadLevels(string json, out List<Level> levelData)
    {
        levelData = JsonConvert.DeserializeObject<List<Level>>(json);

        if (levelData == null) {
            return -1;
        }
        return 0;
    }
}
