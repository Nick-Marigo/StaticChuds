using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
abstract public class Trigger {
    protected string description;
    protected string type;

    // The effect this trigger invokes
    public Effect effect;

    abstract protected void InvokeEffect();
}
