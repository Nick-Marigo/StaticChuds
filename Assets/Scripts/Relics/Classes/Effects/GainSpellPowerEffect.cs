using UnityEngine;

public class GainSpellPowerEffect : Effect {

    public GainSpellPowerEffect(Relic owner, string description, string type, string amount, string until) {
        this.relic = owner;
        this.description = description;
        this.type = type;
        this.until = until;
    }

    override public void PerformEffect() {
        Debug.Log(until);
    }

    override public void ChangeOwner(GameObject owner) {
        return;
    }
}
