using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
abstract public class Effect {
    protected string description;
    protected string type;
    protected string amount;
    protected string until;

    // The Relic this effect belongs to
    [JsonIgnore]
    protected Relic relic;

    abstract public void PerformEffect();
}
