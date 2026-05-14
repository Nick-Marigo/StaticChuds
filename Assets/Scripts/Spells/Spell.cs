using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.OptIn)]
public abstract class Spell 
{
    [JsonProperty]
    protected string name;
    [JsonProperty]
    protected string description;

    public float last_cast;
    public SpellCaster owner;
    public virtual Hittable.Team team { get; protected set; }
    public virtual DamageInfo damage { get; protected set; }
    public virtual Projectile projectile { get; protected set; }
    public Spell statSource;

    public Spell(SpellCaster owner) {
        this.owner = owner;
        this.statSource = this;
    }

    public string GetName()
    {
        return name;
    }

    public virtual string GetDescription()
    {
        return description;
    }

    public virtual string GetDisplayName()
    {
        return name;
    }

    public virtual string GetFullDescription()
    {
        return name + ": " + description;
    }

    public virtual int GetDamage() {
        return 10;
    }

    public virtual float GetSpeed()
    {
        return RPNEvaluator.RPNEvaluator.Evaluatef(projectile.speed, new Dictionary<string, float> { {"power", (float)owner.spellPower} });
    }

    public virtual int GetManaCost()
    {
        return 10;
    }

    public virtual float GetCooldown()
    {
        return 0.75f;
    }

    public virtual int GetIcon()
    {
        return 0;
    }

    public virtual string GetTrajectory()
    {
        return projectile.trajectory;
    }
    
    public bool IsReady()
    {
        return (last_cast + GetCooldown() < Time.time);
    }

    public virtual IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;
        yield return new WaitForEndOfFrame();
    }
}
