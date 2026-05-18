using UnityEngine;
using System.Collections;

public abstract class SpellModifier : Spell {
    protected Spell innerSpell;
    override public DamageInfo damage { get { return innerSpell.damage; } }
    override public Hittable.Team team { get { return innerSpell.team; } }
    override public Projectile projectile { get { return innerSpell.projectile; } }
    override public int icon { get { return innerSpell.icon; }}

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
        Debug.Log("Modifier cast called on: " + this.GetType().Name);
        yield return innerSpell.Cast(where, target, team);
    }

    public override void SetStatsSource(Spell source)
    {
        statSource = source;
        innerSpell.SetStatsSource(source);
    }

    public SpellModifier(SpellCaster owner, Spell innerSpell) : base(owner) {
        this.innerSpell = innerSpell;
        SetStatsSource(this);
    }
}
