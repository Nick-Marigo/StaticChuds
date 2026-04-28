using Newtonsoft.Json;

[System.Serializable]
public class Spawn {
    public string enemy; 
    public string count;
    public string hp = "base";
    public int delay = 2;
    public int[] sequence = {1};
    public string location = "random";
    public string speed = "base";
    public string damage = "base";
}
