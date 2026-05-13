using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Buffers;

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

    override public int GetDamage() {
        float total = RPNEvaluator.RPNEvaluator.Evaluatef(damage.amount, 
                new Dictionary<string, int> { {"power", owner.spellPower} });
        return Mathf.RoundToInt(total);
    }

    public int CalculateN()
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(N, new Dictionary<string, int> { {"power", owner.spellPower} });
        
    }

    protected override void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            int finalDamage = owner.spell.GetDamage();
            Debug.Log("ArcaneBlast Intial: " + finalDamage);
            other.Damage(new Damage(finalDamage, damage.type));

            int count = CalculateN();
            for (int i = 0; i < count; i++)
            {
                //TODO: change this from random to sphere 
                Vector3 randomDir = Random.onUnitSphere;

                GameManager.Instance.projectileManager.CreateProjectile(secondary_projectile.sprite, secondary_projectile.trajectory, impact, randomDir, float.Parse(secondary_projectile.speed), OnSecondaryHit, float.Parse(secondary_projectile.lifetime));
            }
        }
    }

    private void OnSecondaryHit(Hittable other, Vector3 impact)
    {
        if(other.team != team)
        {
            int secDamage = RPNEvaluator.RPNEvaluator.Evaluate(secondary_damage, new Dictionary<string, int> { {"power", owner.spellPower} });
            Debug.Log("ArcaneBlast second: " + secDamage);
            other.Damage(new Damage(secDamage, damage.type));
        }
    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;

        Projectile p = this.projectile;
        float speed = RPNEvaluator.RPNEvaluator.Evaluatef(p.speed, new Dictionary<string, float> { {"power", (float)owner.spellPower}});

        GameManager.Instance.projectileManager.CreateProjectile(0, projectile.trajectory, where, target - where, speed, this.OnHit);
        yield return new WaitForEndOfFrame();
    }

    public ArcaneBlast(SpellCaster owner) : base(owner) {
        SetAttributes();
    }
}