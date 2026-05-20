using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class TriggerGenerator {
    protected string description;
    protected string type;
    protected string amount;

    public Trigger GenerateTrigger(Relic owner) {
        switch(type) {
            case("take-damage"):
                return new TakeDamageTrigger(owner, description, type);
            case("stand-still"):
                return new StandStillTrigger(owner, description, type, amount);
            default:
                return null;
        }
    }
}
