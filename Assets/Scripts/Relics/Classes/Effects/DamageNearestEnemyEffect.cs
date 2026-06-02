using System.Collections.Generic;
using UnityEngine;

public class DamageNearestEnemyEffect : Effect {
    public DamageNearestEnemyEffect(string description, string type, string amount) : base() {
        this.description = description;
        this.type = type;
        this.amount = amount;
    }

    public override void PerformEffect() {
        base.PerformEffect();

        var playerSpellcaster = Object.FindAnyObjectByType<PlayerInstance>();
        
        GameObject closest = GameManager.Instance.GetClosestEnemy(playerSpellcaster.gameObject.transform.position);

        var inflictDamage = RPNEvaluator.RPNEvaluator.Evaluate(amount, new Dictionary<string, int>());
        closest.GetComponent<EnemyController>().hp.Damage(new Damage(inflictDamage, "physical"));
    }
}
