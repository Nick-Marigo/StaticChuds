using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class RiskyDamage : SpellModifier {
    public static JObject config;
    protected string damageMin_multiplier;
    protected string damageMax_multiplier;

    void SetAttributes() {
            if (config == null) {
                Debug.Log("This spell's config has not been set");
                return;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(config.CreateReader(), this);
    }

    override public int GetDamage() {

        float damageMin = RPNEvaluator.RPNEvaluator.Evaluatef(damageMin_multiplier, 
            floatRpnVals);

        float damageMax = RPNEvaluator.RPNEvaluator.Evaluatef(damageMax_multiplier, 
            floatRpnVals);

        float damageMultiplier = UnityEngine.Random.Range(damageMin, damageMax);

        return Mathf.RoundToInt(innerSpell.GetDamage() * damageMultiplier);
    }

    public RiskyDamage(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}
