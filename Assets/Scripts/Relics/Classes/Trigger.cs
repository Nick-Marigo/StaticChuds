using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
abstract class Trigger {
    protected string description;
    protected string type;

    // The effect this trigger invokes
    public Effect effect;

    abstract protected void InvokeEffect();
}
