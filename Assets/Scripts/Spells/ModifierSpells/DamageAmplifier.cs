using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class DamageAmplifier : SpellModifier {
    public static JObject config;
    protected string damage_multiplier;
    protected string mana_multiplier;

    void SetAttributes() {
            if (config == null) {
                Debug.Log("This spell's config has not been set");
                return;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(config.CreateReader(), this);
    }

    override public int GetDamage() {
        // TODO make a calculator class
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(damage_multiplier, 
                new Dictionary<string, float> { {"power", (float)owner.spellPower } });
        return Mathf.RoundToInt(innerSpell.GetDamage() * multiplier);
    }

    public override int GetManaCost()
    {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(mana_multiplier, 
                new Dictionary<string, float> { {"power", (float)owner.spellPower } });
        return Mathf.RoundToInt(innerSpell.GetManaCost() * multiplier);
    }

    public DamageAmplifier(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}
