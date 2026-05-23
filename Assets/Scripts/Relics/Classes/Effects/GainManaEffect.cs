using UnityEngine;
using System.Collections.Generic;

public class GainManaEffect : Effect {

    public GainManaEffect(string description, string type, string amount) {
        this.description = description;
        this.type = type;
        this.amount = amount;
        }

    override public void PerformEffect() {
        InvokeAttributePackageRequested();
        int additionalMana = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
        Debug.Log("player gained " + additionalMana + " mana");
        Debug.Log(attributePackage);
        int mana = (int)attributePackage.AttributeDict["mana"].Get();
        //attributePackage.AttributeDict["mana"].Set(mana + additionalMana);
    }
}
