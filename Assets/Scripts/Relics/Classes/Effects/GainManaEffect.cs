using UnityEngine;

public class GainManaEffect : Effect {
    public GainManaEffect(string description, string type, string amount) {
        this.description = description;
        this.type = type;
        this.amount = amount;
    }

    override public void PerformEffect() {
    }
}
