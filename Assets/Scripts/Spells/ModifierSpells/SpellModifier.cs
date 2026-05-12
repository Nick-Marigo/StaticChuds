using UnityEngine;
using System.Collections;

public abstract class SpellModifier : Spell {
    protected Spell innerSpell;
    override public DamageInfo damage { get { return innerSpell.damage; } }
    override public Hittable.Team team { get { return innerSpell.team; } }
    override public Projectile projectile { get { return innerSpell.projectile; } }

    public SpellModifier(SpellCaster owner, Spell innerSpell) : base(owner) {
        this.innerSpell = innerSpell;
    }
}
