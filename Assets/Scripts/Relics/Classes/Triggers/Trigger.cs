using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
abstract public class Trigger {
    protected string description;
    protected string type;

    // The relic this trigger belongs to
    [JsonIgnore]
    protected Relic relic;

    abstract protected void InvokeEffect();
}
