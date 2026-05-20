using Newtonsoft.Json;
using UnityEngine;

[JsonObject(MemberSerialization.Fields)]
abstract public class Trigger {
    protected string description;
    protected string type;
    protected string amount;

    // The relic this trigger belongs to
    [JsonIgnore]
    protected Relic relic;

    virtual protected void InvokeEffect() {
        if (relic.effect == null) {
            Debug.Log("relic " + relic.name + " has a trigger but no effect");
            return;
        }
        relic.effect.PerformEffect();
    }
}
