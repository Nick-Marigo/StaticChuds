using UnityEngine;
using System.Collections.Generic;

public class GainManaEffect : Effect {

    public GainManaEffect(Relic owner, string description, string type, string amount) {
        this.relic = owner;
        this.description = description;
        this.type = type;
        this.amount = amount;
        }

    SpellCaster FindSpellCaster() {
        if (relic.owner == null) {
            Debug.Log(relic.name + " does not have an owner");
        }
        // TODO change how owner assignment works?
        SpellCaster spellCaster = relic.owner.GetComponent<PlayerController>().spellcaster;
        if (spellCaster == null) {
            Debug.Log(relic.name + ": owner (" + this.relic.owner + ") has no spellCaster component attached to it");
        }
        return spellCaster;
    }

    override public void PerformEffect() {
        SpellCaster spellCaster = FindSpellCaster();
        int manaAmount = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
        Debug.Log("player gained " + manaAmount + " mana");
        spellCaster.mana += manaAmount;
    }

}
