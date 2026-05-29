using UnityEngine;

[JsonObject(MemberSerialization.Fields)]
public class Recoil : SpellModifier {
	public static JObject config;
	protected string cooldown_multiplier;
	protected string knockback_force;
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

	override public int GetCooldownMultiplier() {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(cooldown_multiplier, floatRpnVals);

        return innerSpell.GetCooldown() * multiplier;
	}

	override public int GetDamage() {
		float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(damage_multiplier, floatRpnVals);

		return innerSpell.GetDamage() * multiplier;
	}
}
