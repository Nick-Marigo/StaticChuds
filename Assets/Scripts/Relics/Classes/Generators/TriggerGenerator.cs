using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class TriggerGenerator {
    protected string description;
    protected string type;

    public Trigger GenerateTrigger(Relic owner) {
        switch(type) {
            case("take-damage"):
                return new TakeDamageTrigger(owner, description, type);
            default:
                return null;
        }
    }
}
