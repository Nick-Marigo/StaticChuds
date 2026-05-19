using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class TriggerGenerator {
    protected string description;
    protected string type;

    public Trigger GenerateTrigger() {
        Debug.Log(type);
        switch(type) {
            case("take-damage"):
                return new TakeDamageTrigger(description, type);
            default:
                return null;
        }
    }
}
