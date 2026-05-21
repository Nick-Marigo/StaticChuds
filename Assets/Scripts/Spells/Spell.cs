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
    public virtual int icon { get; protected set; }
    public Spell statSource;

    public Spell(SpellCaster owner) {
        this.owner = owner;
        this.statSource = this;
        UpdateDicts(owner);
        EventBus.Instance.SpellCast += UpdateDicts;
    }

    // FIX
    void OnDestroy() {
        EventBus.Instance.SpellCast -= UpdateDicts;
    }

    public virtual void SetStatsSource(Spell source)
    {
        statSource = source;
    }

    // Dictionaries for RPNE calculations
    protected Dictionary<string, int> intRpnVals;
    protected Dictionary<string, float> floatRpnVals;


    // On waveStart, all the dictionary values are updated to
    // reflect the current game state
    protected void UpdateDicts(SpellCaster caster) {
        if (caster != owner) return;
        int waveNum = GameManager.Instance.currentWave;
        intRpnVals = new Dictionary<string, int> {
            {"power", owner.spellPower},
            {"wave", waveNum}
        };
        floatRpnVals = new Dictionary<string, float> {
            {"power", owner.spellPower},
            {"wave", waveNum} 
        };
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

    virtual public int GetDamage() {
        return RPNEvaluator.RPNEvaluator.Evaluate(damage.amount, intRpnVals);
    }

    public virtual float GetSpeed()
    {
        return RPNEvaluator.RPNEvaluator.Evaluatef(projectile.speed, floatRpnVals);
    }

    public virtual string GetTrajectory()
    {
        return projectile.trajectory;
    }

    virtual public int GetManaCost() { return 0; }
    virtual public float GetCooldown() { return 0; }

    public bool IsReady()
    {
        return (last_cast + GetCooldown() < Time.time);
    }


    public virtual IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;
        yield return new WaitForEndOfFrame();
    }
}
