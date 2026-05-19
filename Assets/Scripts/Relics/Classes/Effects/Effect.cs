using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
abstract public class Effect {
    protected string description;
    protected string type;
    protected string amount;
    protected string until;

    abstract public void PerformEffect();
}
