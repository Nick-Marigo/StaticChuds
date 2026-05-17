using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.OptOut)]
public class BaseSpell : Spell {
    [JsonProperty]
    override public int icon { get; protected set; }
    [JsonProperty]
    protected string mana_cost;
    [JsonProperty]
    protected string cooldown;
    [JsonProperty]
    override public Projectile projectile { get; protected set; }
    [JsonProperty]
    override public Hittable.Team team { get; protected set; }
    [JsonProperty]
    override public DamageInfo damage { get; protected set; }

    public BaseSpell(SpellCaster owner) : base(owner) {}

    override public int GetManaCost()
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(mana_cost, intRpnVals);
    }

    override public float GetCooldown()
    {
        return RPNEvaluator.RPNEvaluator.Evaluatef(cooldown, floatRpnVals);
    }

    protected virtual void OnHit(Hittable other, Vector3 impact) {
        if (other.team != team) {
            other.Damage(new Damage(statSource.GetDamage(), damage.type));
        }
    }
}
