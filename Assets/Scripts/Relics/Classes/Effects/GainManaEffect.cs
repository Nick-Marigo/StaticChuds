using UnityEngine;
using System.Collections.Generic;

public class GainManaEffect : Effect {
    SpellCaster spellCaster;

    public GainManaEffect(Relic owner, string description, string type, string amount) {
        this.relic = owner;
        this.description = description;
        this.type = type;
        this.amount = amount;
    }

    override public void ChangeOwner(GameObject owner) {
        if (owner == null) {
            spellCaster = null;
            return;
        }

        spellCaster = owner.GetComponent<PlayerController>().spellcaster;
        if (spellCaster == null) {
            Debug.Log(relic.name + ": owner (" + this.relic.Owner + ") has no spellCaster component attached to it");
        }
    }

    override public void PerformEffect() {
        if (spellCaster == null) {
            return;
        }

        // TODO make public dictionaries for calculations
        int manaAmount = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
        Debug.Log("player gained " + manaAmount + " mana");
        spellCaster.mana += manaAmount;
    }
}
