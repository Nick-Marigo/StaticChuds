using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.Fields)]
public class GotYourBack : SpellModifier {
    public static JObject config;
    protected string mana_multiplier;

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

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        Vector3 direction = target - where;

        // Let the inner spell handle its own casting behavior
        yield return innerSpell.Cast(where, target, team);

        // Let the inner spell handle its own casting behavior
        Vector3 newWhere = _playerController.position;
        Vector3 backwardTarget = newWhere - direction;
        yield return innerSpell.Cast(newWhere, backwardTarget, team);
    }

    public GotYourBack(SpellCaster owner, Spell innerSpell) : base(owner, innerSpell) {
        SetAttributes();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
}
