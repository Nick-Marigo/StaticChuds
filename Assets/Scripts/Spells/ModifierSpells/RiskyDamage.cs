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
            new Dictionary<string, float> { {"power", (float)owner.spellPower } });

        float damageMax = RPNEvaluator.RPNEvaluator.Evaluatef(damageMax_multiplier, 
            new Dictionary<string, float> { {"power", (float)owner.spellPower } });

        float damageMultiplier = UnityEngine.Random.Range(damageMin, damageMax);

        return Mathf.RoundToInt(innerSpell.GetDamage() * damageMultiplier);
    }

    public RiskyDamage(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}
