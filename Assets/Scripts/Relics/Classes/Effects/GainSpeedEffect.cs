using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GainSpeedEffect : Effect {

    bool isActive = false;

    public GainSpeedEffect(string description, string type, string amount) {
        this.description = description;
        this.type = type;
        this.amount = amount;
    }

    override public void PerformEffect() {
        //base.PerformEffect();
        InvokeAttributePackageRequested();

        if (isActive) return;

        CoroutineManager.Instance.Run(TemporarySpeedBoost());

        /*
        int tempSpeed = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
        Debug.Log("player gained " + tempSpeed + " speed");
        int speed = (int)attributePackage.AttributeDict["speed"].Get();
        attributePackage.AttributeDict["speed"].Set(speed * tempSpeed);

        int additionalMana = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
        Debug.Log("player gained " + additionalMana + " mana");
        int mana = (int)attributePackage.AttributeDict["mana"].Get();
        attributePackage.AttributeDict["mana"].Set(mana + additionalMana);
        */
    }

    IEnumerator TemporarySpeedBoost()
    {
        isActive = true;
            
        int amountSpeed = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());

        int oldSpeed = (int)attributePackage["speed"].Get();
        int newSpeed = oldSpeed + amountSpeed;

        attributePackage["speed"].Set(newSpeed);

        yield return new WaitForSeconds(2f);

        attributePackage["speed"].Set(oldSpeed);

        isActive = false;

        Debug.Log("Speed boost ended. Going back to old speed: " + oldSpeed);
    }
    
}
