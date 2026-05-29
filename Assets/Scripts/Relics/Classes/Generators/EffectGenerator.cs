using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public class EffectGenerator {
    protected string description;
    protected string type;
    protected string amount;
    protected string until;

    public Effect GenerateEffect() {
        switch(type) {
            case "gain-mana": 
                return new GainManaEffect(description, type, amount);
            case "gain-spellpower": 
                return new GainSpellPowerEffect(description, type, amount, until);
            case "gain-speed":
                return new GainSpeedEffect(description, type, amount, until);
            case "gain-health":
                return new GainHealthEffect(description, type, amount);
            case "gain-percent-health":
                return new GainPercentHealthEffect(description, type, amount);
            case "damage-nearest":
                return new DamageNearestEnemyEffect(description, type, amount);
            case "next-spells-free":
                return new NextSpellsFreeEffect(description, type, amount);
            default: 
                return null;
        }
    }
}
