using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public abstract class SpellModifier : Spell, iNodeObject {
    protected Spell innerSpell;
    override public DamageInfo damage { get { return innerSpell.damage; } }
    override public Hittable.Team team { get { return innerSpell.team; } }
    override public Projectile projectile { get { return innerSpell.projectile; } }
    override public int icon { get { return innerSpell == null? -1 : innerSpell.icon; }} 

    [JsonProperty("icon")]
    protected int nodeIcon;

    public int GetNodeIcon()
    {
        return nodeIcon;
    }

    public override string GetDisplayName()
    {
        return name + " " + innerSpell.GetDisplayName();
    }

    public override string GetFullDescription()
    {
        return GetDisplayName() + "\n" + name + ": " + description + "\n" + innerSpell.GetFullDescription();
    }

    public override int GetDamage()
    {
        return innerSpell.GetDamage();
    }

    public override float GetSpeed()
    {
        return innerSpell.GetSpeed();
    }

    override public int GetManaCost()
    {
        return innerSpell.GetManaCost();
    }

    override public float GetCooldown()
    {
        return innerSpell.GetCooldown();
    }

    public override string GetTrajectory()
    {
        return innerSpell.GetTrajectory();
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        //Debug.Log("Modifier cast called on: " + this.GetType().Name);
        yield return innerSpell.Cast(where, target, team);
    }

    public override void SetStatsSource(Spell source)
    {
        if (source == null || innerSpell == null) return;
        statSource = source;
        innerSpell.SetStatsSource(source);
    }

    public Spell WrapOver(Spell innerSpell) {
        this.innerSpell = innerSpell;
        SetStatsSource(this);
        return this;
    }

    public SpellModifier(SpellCaster owner, Spell innerSpell) : base(owner) {
        this.innerSpell = innerSpell;
        SetStatsSource(this);
    }
}
