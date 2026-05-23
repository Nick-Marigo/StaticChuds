using System.Collections.Generic;
using UnityEngine;

public class GainSpellPowerEffect : Effect {
    int _extraSpellPower;
    bool _effectActive = false;


    public GainSpellPowerEffect(string description, string type, string amount, string until) : base() {
        this.description = description;
        this.type = type;
        this.amount = amount;
        this.until = until;
    }

    override public void Activate(){
        base.Activate();
        // Since stats are reset when the wave starts, we must
        // make sure we do not mutate it with extraspellpower
        EventBus.Instance.OnWaveStart += (_) => {
            _extraSpellPower = 0;
            _StopEffect();
        };
    }


    override protected void _StopEffect() {
        if (!_effectActive) return;

        _effectActive = false;
        InvokeAttributePackageRequested();
        InvokeEffectStopped();
        int spellpower = (int)attributePackage.AttributeDict["spellpower"].Get();
        attributePackage.AttributeDict["spellpower"].Set(spellpower - _extraSpellPower);
        Debug.Log("spellpower back to " + attributePackage.AttributeDict["spellpower"].Get());
    }

    override public void PerformEffect() {
        if (_effectActive) return;

        _effectActive = true;
        InvokeAttributePackageRequested();
        int waveNum = GameManager.Instance.currentWave;
        _extraSpellPower = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int> {
                {"wave", waveNum}
                });
        int spellpower = (int)attributePackage.AttributeDict["spellpower"].Get();
        attributePackage.AttributeDict["spellpower"].Set(spellpower + _extraSpellPower);
        Debug.Log("spellpower increased to " + attributePackage.AttributeDict["spellpower"].Get());
    }
}

// TODO
// Add unsubscription and support for player switching
