using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class Chaos : SpellModifier {
    public static JObject config;
    protected string damage_multiplier;
    protected string projectile_trajectory;

    void SetAttributes()
    {
        if (config == null) {
                Debug.Log("This spell's config has not been set");
                return;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(config.CreateReader(), this);
    }

    public override int GetDamage()
    {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(damage_multiplier, new Dictionary<string, float> { {"power", (float)owner.spellPower} });
        return Mathf.RoundToInt(innerSpell.GetDamage() * multiplier);
    }

    public override string GetTrajectory()
    {
        return projectile_trajectory;
    }

    public Chaos(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}