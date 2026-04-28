using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class LevelLoader
{
    private static List<Level> levels;

    public static List<Level> GetLevels() {
        if (levels == null) {
            levels = LoadLevels();
        }
        return levels;
    }

    // Loads levels from levels.json and stores them in a list
     private static List<Level> LoadLevels() {
        TextAsset levelJson = Resources.Load<TextAsset>("levels");
        if (levelJson == null)
        {
            Debug.Log("Failed to get json from Resources");
            return null;
        }
        int status = LevelLoader.JsonToText(levelJson.text, out levels);
        if (status == -1) {
            Debug.Log("Failed to load levels from JSON"); 
            return null; 
        }
        return levels;
    }

    private static int JsonToText(string json, out List<Level> levelData)
    {
        levelData = JsonConvert.DeserializeObject<List<Level>>(json);

        if (levelData == null) {
            return -1;
        }
        return 0;
    }
}
