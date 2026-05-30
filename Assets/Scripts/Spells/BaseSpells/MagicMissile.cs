using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MagicMissile : BaseSpell
{
    public static JObject config;

    void SetAttributes() {
        if (config == null) {
            Debug.Log("This spell's config has not been set");
            return;
        }
        JsonSerializer serializer = new JsonSerializer();
        serializer.Populate(config.CreateReader(), this);
    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(projectile.sprite, statSource.GetTrajectory(), where, target - where, statSource.GetSpeed(), onHitCallbacks);
        yield return new WaitForEndOfFrame();
    }

    public MagicMissile(SpellCaster owner) : base(owner) {
        SetAttributes();
		onHitCallbacks.Add(OnHit);
    }
}
