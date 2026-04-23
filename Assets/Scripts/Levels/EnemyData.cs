using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class EnemyData
{
    [JsonProperty("name")]
    public string name;
    [JsonProperty("sprite")]
    public int sprite;
    [JsonProperty("hp")]
    public int hp;
    [JsonProperty("speed")]
    public int speed;
    [JsonProperty("damage")]
    public int damage;

}

