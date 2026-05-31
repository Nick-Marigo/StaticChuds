using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class Doubler : SpellModifier {
    public static JObject config;
    protected string delay;
    protected string mana_multiplier;
    protected string cooldown_multiplier;
    [JsonIgnore]
    private PlayerController _playerController;

    void SetAttributes()
    {
        if (config == null) {
                Debug.Log("This spell's config has not been set");
                return;
            }
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(config.CreateReader(), this);
    }

    override public int GetManaCost()
    {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(mana_multiplier, floatRpnVals);
        return Mathf.RoundToInt(innerSpell.GetManaCost() * multiplier);
    }

    override public float GetCooldown()
    {
        float multiplier = RPNEvaluator.RPNEvaluator.Evaluatef(cooldown_multiplier, floatRpnVals);

        return innerSpell.GetCooldown() * multiplier;
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        // Let the inner spell handle its own casting behavior
        yield return innerSpell.Cast(where, target, team);

        // Wait for delay time between casts
        float delayTime = RPNEvaluator.RPNEvaluator.Evaluatef(delay, floatRpnVals);
        yield return new WaitForSeconds(delayTime);

        // Let the inner spell handle its own casting behavior and
        // get current pos from playerController
        yield return innerSpell.Cast(_playerController.position, target, team);
    }

    public Doubler(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
}
