using UnityEngine;
using System.Collections;

public abstract class SpellModifier : Spell {
    protected Spell innerSpell;
    override public DamageInfo damage { get { return innerSpell.damage; } }
    override public Hittable.Team team { get { return innerSpell.team; } }
    override public Projectile projectile { get { return innerSpell.projectile; } }

    public override float GetSpeed()
    {
        return innerSpell.GetSpeed();
    }

    public override int GetManaCost()
    {
        return innerSpell.GetManaCost();
    }

    public override float GetCooldown()
    {
        return innerSpell.GetCooldown();
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        Debug.Log("Modifier cast called on: " + this.GetType().Name);
        yield return innerSpell.Cast(where, target, team);
    }

    public SpellModifier(SpellCaster owner, Spell innerSpell) : base(owner) {
        this.innerSpell = innerSpell;
        innerSpell.statSource = this;
    }
}
