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
        // TODO make damage a float
        int mul = (int) RPNEvaluator.RPNEvaluator.Evaluatef(damage_multiplier, 
                new Dictionary<string, float> { {"power", (float)owner.spellPower } });
        return innerSpell.GetDamage() * mul;
    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(0, projectile.trajectory, where, target - where, 15f, OnHit);
        yield return new WaitForEndOfFrame();
    }

    protected virtual void OnHit(Hittable other, Vector3 impact) {
        if (other.team != team) {
            other.Damage(new Damage(GetDamage(), damage.type));
        }
    }

    public DamageAmplifier(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
    }
}
