using UnityEngine;
using System.Collections.Generic;

public class GainHealthEffect : Effect {

    public GainHealthEffect(string description, string type, string amount) {
        this.description = description;
        this.type = type;
        this.amount = amount;
    }

    override public void PerformEffect() {
        base.PerformEffect();
        InvokeAttributePackageRequested();
        int additionalHealth = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
        Debug.Log("player gained " + additionalHealth + " health");
        int health = (int)attributePackage.AttributeDict["mana"].Get();
        Debug.Log("Player old health: " + health);
        attributePackage.AttributeDict["mana"].Set(health + additionalHealth);

        health = (int)attributePackage.AttributeDict["mana"].Get();
        Debug.Log("Player new health: " + health);
    }
}
