using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public abstract class BaseSpell : Spell {
    protected int icon;
    protected DamageInfo damage;
    protected string mana_cost;
    protected string cooldown;
    protected Projectile projectile;

    public void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(GetDamage(), damage.type));
        }
    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(icon, projectile.trajectory, where, target - where, 15f, OnHit);
        yield return new WaitForEndOfFrame();
    }

    override public int GetDamage() {
        return RPNEvaluator.RPNEvaluator.Evaluate(damage.amount, 
                new Dictionary<string, int> { {"power", owner.spellPower } });
    }
}
