using Newtonsoft.Json;

[System.Serializable]
public class LevelData {
    [JsonProperty("name")]
    public string name;
    [JsonProperty("waves")]
    public int waves;
    [JsonProperty("spawns")]
    public SpawnData[] spawns;
}

