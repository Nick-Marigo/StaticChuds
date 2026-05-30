using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[JsonObject(MemberSerialization.Fields)]
public class Shielding : SpellModifier {
	public static JObject config;
	protected string cooldown_multiplier;
	protected string knockback_force;
	protected string knockback_timer;
	protected string damage_multiplier;
	[JsonIgnore]
	private PlayerController playerController;

	private void SetAttributes() {
		if(config == null) {
			Debug.Log("This spell's config has not been set");
			return;
		}

		JsonSerializer serializer = new JsonSerializer();
		serializer.Populate(config.CreateReader(), this);
	}

	override public float GetCooldown() {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(cooldown_multiplier, floatRpnVals);

        return innerSpell.GetCooldown() * multiplier;
	}

	override public int GetDamage() {
		float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(damage_multiplier, floatRpnVals);

		return (int)(innerSpell.GetDamage() * multiplier);
	}

	public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
		yield return innerSpell.Cast(where, target, team);
	}

	protected void OnHit(Hittable other, Vector3 impact) {
		if(other.team == team) {
			return;
		}

		int finalDamage = statSource.GetDamage();
		other.Damage(new Damage(finalDamage, damage.type));

		float knockbackScale = RPNEvaluator.RPNEvaluator.Evaluatef(knockback_force, floatRpnVals);

		other.owner.GetComponent<Unit>().canMove = false;
		other.owner.GetComponent<Rigidbody2D>().AddForce((impact - other.owner.transform.position).normalized * knockbackScale);

		CoroutineManager.Instance.Run(EnableCanMove(other.owner.GetComponent<Unit>(), RPNEvaluator.RPNEvaluator.Evaluatef(knockback_timer, floatRpnVals)));
	}

	private IEnumerator EnableCanMove(Unit target, float delay) {
		yield return new WaitForSeconds(delay);

		target.canMove = true;
	}

	public Shielding(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
		SetAttributes();
		innerSpell.onHitCallbacks.AddRange(onHitCallbacks);
		innerSpell.onHitCallbacks.Add(OnHit);
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
}
