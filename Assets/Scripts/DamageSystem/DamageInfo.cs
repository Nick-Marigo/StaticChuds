using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class DamageInfo {
   public string amount;
   public string type;
}
