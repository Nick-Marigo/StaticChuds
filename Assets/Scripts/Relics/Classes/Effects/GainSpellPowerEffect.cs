using System;
using System.Collections.Generic;
using UnityEngine;

public class GainSpellPowerEffect : Effect {
    int extraSpellPower;
    bool _effectActive = false;


    public GainSpellPowerEffect(string description, string type, string amount, string until) {
        this.description = description;
        this.type = type;
        this.amount = amount;
        this.until = until;

        _SubscribeToStopCondition();
    }

    /* This function suscribes the StopEffect function to the correct event based on
     * what was passed into the "until" attribute */
    void _SubscribeToStopCondition() {
        PlayerEventWrapper eventWrapper = (PlayerEventWrapper)attributePackage["event_wrapper"].Get();
        switch(until){
            case("move"):
                eventWrapper.playerMoved += _StopEffect;
                break;
            default:
                return;
        }
    }

    void _StopEffect() {
        if (_effectActive == false) return;

        _effectActive = false;
        InvokeAttributePackageRequested();
        int spellpower = (int)attributePackage["spellpower"].Get();
        attributePackage["spellpower"].Set(spellpower - extraSpellPower);
        Debug.Log("spellpower back to " + attributePackage["spellpower"].Get());
    }

    override public void PerformEffect() {
        if (_effectActive) return;

        _effectActive = true;
        InvokeAttributePackageRequested();
        int waveNum = GameManager.Instance.currentWave;
        extraSpellPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int> {
            {"wave", waveNum}
        });
        int spellpower = (int)attributePackage["spellpower"].Get();
        attributePackage["spellpower"].Set(spellpower + extraSpellPower);
        Debug.Log("spellpower increased to " + attributePackage["spellpower"].Get());
    }
}

// TODO
// Add unsubscription and support for player switching
