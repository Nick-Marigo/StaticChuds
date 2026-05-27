using UnityEngine;
using System.Collections.Generic;
using System;

public class GainPercentHealthEffect : Effect {

    public GainPercentHealthEffect(string description, string type, string amount) : base() {
        this.description = description;
        this.type = type;
        this.amount = amount;
    }

    override public void PerformEffect() {
        base.PerformEffect();
        InvokeAttributePackageRequested();
        int percent = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
        int currentHealth = (int)attributePackage.AttributeDict["health"].Get();
        Debug.Log("Old health: " + currentHealth);
        int maxHealth = (int)attributePackage.AttributeDict["max_health"].Get();
        int healAmount = Mathf.RoundToInt(maxHealth * (percent / 100f));
        Debug.Log("Heal Amount: " + healAmount);
        attributePackage.AttributeDict["health"].Set(currentHealth + healAmount);

        currentHealth = (int)attributePackage.AttributeDict["health"].Get();
        Debug.Log("New health: " + currentHealth);
    }
}
