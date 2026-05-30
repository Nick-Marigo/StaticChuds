using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class Cursed : SpellModifier {
    public static JObject config;
    protected string cooldown_multiplier;

    void SetAttributes() {
            if (config == null) {
                Debug.Log("This spell's config has not been set");
                return;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(config.CreateReader(), this);
    }

    public override float GetCooldown() {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(cooldown_multiplier, floatRpnVals);
        return innerSpell.GetCooldown() * multiplier;
    }

    public Cursed(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}
