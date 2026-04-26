using Newtonsoft.Json;

[System.Serializable]
public class Level {
    [JsonProperty("name")]
    public string name;
    [JsonProperty("waves")]
    public int waves;
    [JsonProperty("spawns")]
    public Spawn[] spawns;
}

