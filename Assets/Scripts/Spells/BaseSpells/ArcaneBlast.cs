using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ArcaneBlast : BaseSpell
{
    public static JObject config;
    [JsonProperty] protected string N;
    [JsonProperty] protected string secondary_damage;
    [JsonProperty] protected Projectile secondary_projectile;

    void SetAttributes() {
        if (config == null) {
            Debug.Log("This spell's config has not been set");
            return;
        }
        JsonSerializer serializer = new JsonSerializer();
        serializer.Populate(config.CreateReader(), this);
    }

    int CalculateN()
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(N, intRpnVals);
    }

    protected override void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            int finalDamage = statSource.GetDamage();
            other.Damage(new Damage(finalDamage, damage.type));

            int count = CalculateN();
            for (int i = 0; i < count; i++)
            {
                float angle = i * 360f / count;
                Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);

                GameManager.Instance.projectileManager.CreateProjectile(secondary_projectile.sprite, statSource.GetTrajectory(), impact, direction, statSource.GetSpeed(), new(){ OnSecondaryHit }, float.Parse(secondary_projectile.lifetime));
            }
        }
    }

    private void OnSecondaryHit(Hittable other, Vector3 impact)
    {
        if(other.team != team)
        {
            int secDamage = RPNEvaluator.RPNEvaluator.Evaluate(secondary_damage, intRpnVals);
            other.Damage(new Damage(secDamage, damage.type));
        }
    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;

        Projectile p = this.projectile;
        float speed = RPNEvaluator.RPNEvaluator.Evaluatef(p.speed, floatRpnVals);

        GameManager.Instance.projectileManager.CreateProjectile(0, statSource.GetTrajectory(), where, target - where, speed, onHitCallbacks);
        yield return new WaitForEndOfFrame();
    }

    public ArcaneBlast(SpellCaster owner) : base(owner) {
        SetAttributes();
		onHitCallbacks.Add(OnHit);
    }
}
