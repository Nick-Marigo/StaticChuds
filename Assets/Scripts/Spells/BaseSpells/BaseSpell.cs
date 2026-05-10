using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public abstract class BaseSpell : Spell {
    protected int icon;
    protected DamageInfo damage;
    protected string mana_cost;
    protected string cooldown;
    protected Projectile projectile;

}
