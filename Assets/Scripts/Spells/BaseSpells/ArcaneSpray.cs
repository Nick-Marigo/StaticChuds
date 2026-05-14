using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ArcaneSpray : BaseSpell {
    public static JObject config;
    [JsonProperty] protected string N;
    [JsonProperty] protected string spray;

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

    int CalculateN()
    {
        return RPNEvaluator.RPNEvaluator.Evaluate(N, new Dictionary<string, int> { {"power", owner.spellPower} });
    }

    float CalculateSpray()
    {
        return RPNEvaluator.RPNEvaluator.Evaluatef(spray, new Dictionary<string, float> { {"power", (float)owner.spellPower} });
    }

    float CalculateLifeTime()
    {
        return RPNEvaluator.RPNEvaluator.Evaluatef(projectile.lifetime, new Dictionary<string, float> { {"power", (float)owner.spellPower} });
    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;

        int count = CalculateN();
        float sprayAmount = CalculateSpray();
        float lifetime = CalculateLifeTime();

        Vector3 direction = (target - where).normalized;
        float baseAngle = Mathf.Atan2(direction.y, direction.x);

        for (int i = 0; i < count; i++)
        {
            float t = count == 1 ? 0.5f : (float)i / (count - 1);

            float angleOffset = Mathf.Lerp(-sprayAmount / 2f, sprayAmount / 2f, t);

            float finalAngle = baseAngle + angleOffset;

            Vector3 sprayDirection = new Vector3(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle), 0f);

            GameManager.Instance.projectileManager.CreateProjectile(projectile.sprite, statSource.GetTrajectory(), where, sprayDirection, statSource.GetSpeed(), OnHit, lifetime);
        }

        yield return new WaitForEndOfFrame();
    }

    public ArcaneSpray(SpellCaster owner) : base(owner) {
        SetAttributes();
    }
}