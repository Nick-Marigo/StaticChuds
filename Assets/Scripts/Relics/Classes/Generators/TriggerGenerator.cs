using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class TriggerGenerator {
    protected string description;
    protected string type;
    protected string amount;

    public Trigger GenerateTrigger() {
        switch(type) {
            case("take-damage"):
                return new TakeDamageTrigger(description, type);
            case("stand-still"):
                return new StandStillTrigger(description, type, amount);
            default:
                return null;
        }
    }
}
