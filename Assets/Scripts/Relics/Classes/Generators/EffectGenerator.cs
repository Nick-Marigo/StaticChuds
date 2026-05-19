using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class EffectGenerator {
    protected string description;
    protected string type;
    protected string amount;
    protected string until;

    public Effect GenerateEffect(Relic owner) {
        switch(type) {
            case "gain-mana": 
                return new GainManaEffect(owner, description, type, amount);
            default: 
                return null;
        }
    }
}
