using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class BaseSpell : Spell {
    protected int icon;
    protected DamageInfo damage;
    protected string mana_cost;
    protected string cooldown;
    protected Projectile projectile;

    protected virtual void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(GetDamage(), damage.type));
        }
    }

    public BaseSpell(SpellCaster owner, JObject config) {
        this.owner = owner;
    }


}
