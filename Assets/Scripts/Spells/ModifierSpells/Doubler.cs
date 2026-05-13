using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class Doubler : SpellModifier {
    public static JObject config;
    protected string delay;
    protected string mana_multiplier;
    protected string cooldown_multiplier;

    void SetAttributes()
    {
        if (config == null) {
                Debug.Log("This spell's config has not been set");
                return;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(config.CreateReader(), this);
    }

    public override int GetManaCost()
    {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(mana_multiplier, new Dictionary<string, float> { {"power", (float)owner.spellPower} });
        return Mathf.RoundToInt(innerSpell.GetManaCost() * multiplier);
    }

    public override float GetCooldown()
    {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(cooldown_multiplier, new Dictionary<string, float> { {"power", (float)owner.spellPower} });

        return innerSpell.GetCooldown() * multiplier;
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        yield return innerSpell.Cast(where, target, team);

        float delayTime = RPNEvaluator.RPNEvaluator.Evaluatef(delay, new Dictionary<string, float> { {"power", (float)owner.spellPower} });

        yield return new WaitForSeconds(delayTime);

        yield return innerSpell.Cast(where, target, team);
    }

    public Doubler(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}