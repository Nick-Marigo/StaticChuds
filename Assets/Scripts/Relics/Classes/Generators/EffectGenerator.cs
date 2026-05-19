using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class EffectGenerator {
    protected string description;
    protected string type;
    protected string amount;
    protected string until;

    public Effect GenerateEffect() {
        Debug.Log(type);
        switch(type) {
            case "gain-mana": 
                return new GainManaEffect(description, type, amount);
            default: 
                return null;
        }
    }
}
