using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ArcaneBolt : BaseSpell {
    public static JObject config;

    void SetAttributes() {
        if (config == null) {
            Debug.Log("This spell's config has not been set");
            return;
        }
        JsonSerializer serializer = new JsonSerializer();
        serializer.Populate(config.CreateReader(), this);
    }

    override public int GetDamage() {
        return RPNEvaluator.RPNEvaluator.Evaluate(damage.amount, 
                new Dictionary<string, int> { {"power", owner.spellPower } });
    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(projectile.sprite, statSource.GetTrajectory(), where, target - where, statSource.GetSpeed(), OnHit);
        yield return new WaitForEndOfFrame();
    }

    public ArcaneBolt(SpellCaster owner) : base(owner) {
        SetAttributes();
    }
}
