using Newtonsoft.Json;

[System.Serializable]
public class SpawnData {
    [JsonProperty("enemy")]
    public string enemy;
    [JsonProperty("count")]
    public string count;
    [JsonProperty("hp")]
    public string hp;
    [JsonProperty("delay")]
    public int delay;
    [JsonProperty("sequence")]
    public int[] sequence;
    [JsonProperty("location")]
    public string location;
}
