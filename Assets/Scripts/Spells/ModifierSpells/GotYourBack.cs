using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class GotYourBack : SpellModifier {
    public static JObject config;
    protected string mana_multiplier;

    void SetAttributes()
    {
        if (config == null) {
                Debug.Log("This spell's config has not been set");
                return;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(config.CreateReader(), this);
    }

    override public int GetManaCost()
    {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(mana_multiplier, new Dictionary<string, float> { {"power", (float)owner.spellPower} });
        return Mathf.RoundToInt(innerSpell.GetManaCost() * multiplier);
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        // Let the inner spell handle its own casting behavior
        yield return innerSpell.Cast(where, target, team);

        // Let the inner spell handle its own casting behavior
        yield return innerSpell.Cast(where, where - (target - where), team);
    }

    public GotYourBack(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}
