using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class Projectile {
    public string trajectory;
    public string speed;
    public int sprite;
    public string lifetime;
}
