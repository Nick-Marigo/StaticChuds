using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class SpeedAmplifier : SpellModifier {
    public static JObject config;
    protected string speed_multiplier;

    void SetAttributes() {
            if (config == null) {
                Debug.Log("This spell's config has not been set");
                return;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(config.CreateReader(), this);
    }

    public override float GetSpeed()
    {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(speed_multiplier, new Dictionary<string, float> { {"power", (float)owner.spellPower} });
        Debug.Log("BEING CALLED");
        return innerSpell.GetSpeed() * multiplier;
    }

    public SpeedAmplifier(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}
