using System;
using System.Collections.Generic;
using UnityEngine;

public class GainSpellPowerEffect : Effect {
    SpellCaster spellCaster;
    int extraSpellPower;
    // Used to save our subscription
    Action _unsubscribe;
    bool _effectActive = false;


    public GainSpellPowerEffect(Relic owner, string description, string type, string amount, string until) {
        this.relic = owner;
        this.description = description;
        this.type = type;
        this.amount = amount;
        this.until = until;

        SubscribeToStopCondition();
    }

    /* This function suscribes the StopEffect function to the correct event based on
     * what was passed into the "until" attribute */
    void SubscribeToStopCondition() {
        switch(until){
            case("move"):
                EventBus.Instance.PlayerMoved += StopEffect;
                _unsubscribe = () => EventBus.Instance.PlayerMoved -= StopEffect;
                break;
            default:
                return;
        }
    }

    void StopEffect(GameObject owner) {
        if (owner != relic.Owner || !_effectActive) return;
        _effectActive = false;
        spellCaster.spellPower -= extraSpellPower;
        Debug.Log("spellpower back to " + spellCaster.spellPower);
    }

    override public void PerformEffect() {
        if (spellCaster == null || _effectActive) return;

        _effectActive = true;
        int waveNum = GameManager.Instance.currentWave;
        extraSpellPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int> {
            {"wave", waveNum}
        });
        spellCaster.spellPower += extraSpellPower;
        Debug.Log("spellpower increased to " + spellCaster.spellPower);
    }

    override public void ChangeOwner(GameObject owner) {
        if (_effectActive) StopEffect(owner);
        if (owner == null) {
            spellCaster = null;
            return;
        }

        spellCaster = owner.GetComponent<PlayerController>().spellcaster;
        if (spellCaster == null) {
            Debug.Log(relic.name + ": owner (" + this.relic.Owner + ") has no spellCaster component attached to it");
        }
    }

    override protected void Unsuscribe() {
        _unsubscribe();
    }
}
