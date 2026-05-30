using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GainSpeedEffect : Effect {

    bool isActive = false;
    int _oldSpeed;

    public GainSpeedEffect(string description, string type, string amount, string until) : base() {
        this.description = description;
        this.type = type;
        this.amount = amount;
        this.until = until;
    }

    override public void PerformEffect() {
        if (isActive) return;
        isActive = true;
        base.PerformEffect();
        InvokeAttributePackageRequested();


        TemporarySpeedBoost();
        //CoroutineManager.Instance.Run(TemporarySpeedBoost());

    }

    void TemporarySpeedBoost()
    {
        isActive = true;
        base.PerformEffect();
        int amountSpeed = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());

        _oldSpeed = (int)attributePackage.AttributeDict["speed"].Get();
        int newSpeed = _oldSpeed * amountSpeed;

        attributePackage.AttributeDict["speed"].Set(newSpeed);
        //yield return new WaitForSeconds(1f);

    }

    override protected void _StopEffect() {
        base._StopEffect();
        attributePackage.AttributeDict["speed"].Set(_oldSpeed);

        isActive = false;

        Debug.Log("Speed boost ended. Going back to _old speed: " + _oldSpeed);

    }
}
