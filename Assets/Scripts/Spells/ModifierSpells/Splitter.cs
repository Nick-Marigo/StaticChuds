using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class Splitter : SpellModifier {
    public static JObject config;
    protected string angle;
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
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(mana_multiplier, floatRpnVals);
        return Mathf.RoundToInt(innerSpell.GetManaCost() * multiplier);
    }

    public override IEnumerator Cast(Transform where, Vector3 target, Hittable.Team team)
    {
        float angleValue = RPNEvaluator.RPNEvaluator.Evaluatef(angle, floatRpnVals);

        // Get original direction from caster to target
        Vector3 direction = target - where.position;

        // Rotate the direction by the angle to the left and right around the Z axis
        Vector3 leftDirection = Quaternion.Euler(0, 0, angleValue) * direction;
        Vector3 rightDirection = Quaternion.Euler(0, 0, -angleValue) * direction;

        // Convert the directions back into target positions
        Vector3 leftTarget = where.position + leftDirection;
        Vector3 rightTarget = where.position + rightDirection;

        // Let the inner spell handle its own casting behavior
        yield return innerSpell.Cast(where, leftTarget, team);
        yield return innerSpell.Cast(where, rightTarget, team);
    }

    public Splitter(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}
