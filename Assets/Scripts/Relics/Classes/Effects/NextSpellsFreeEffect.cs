using System.Collections.Generic;
using UnityEngine;

public class NextSpellsFreeEffect : Effect {
    private int _freeSpells;
    
    public NextSpellsFreeEffect(string description, string type, string amount) : base() {
        this.description = description;
        this.type = type;
        this.amount = amount;
    }
    
    override public void Activate() {
        base.Activate();
        PlayerEventWrapper eventWrapper = (PlayerEventWrapper)attributePackage.AttributeDict["event_wrapper"].Get();
        eventWrapper.spellCast += RefundSpellCost;
    }

    public override void PerformEffect() {
        base.PerformEffect();
        _freeSpells = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
    }

    private void RefundSpellCost() {
        if (_freeSpells <= 0) {
            return;
        }

        var playerSpellcaster = Object.FindAnyObjectByType<PlayerController>().spellcaster;
        playerSpellcaster.Mana += playerSpellcaster.spells[playerSpellcaster.selectedSpellIndex].GetManaCost();

        _freeSpells--;

        if (_freeSpells == 0) {
            _StopEffect();
        }
    }
}
