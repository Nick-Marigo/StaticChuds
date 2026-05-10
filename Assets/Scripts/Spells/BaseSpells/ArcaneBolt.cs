using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ArcaneBolt : BaseSpell {

    override protected void SetAttributes() {
        name = "Arcane Bolt";
        // Lazy load this spells attributes
            List<JProperty> spells = SpellLoader.GetSpells();
            JProperty spell = spells.Where(spell => (string)((JObject)spell.Value)["name"] == name).FirstOrDefault();
            if (spell == null) {
                Debug.Log("Failed to find spell of type " + name);
            }
            Debug.Log(spell);
            // Populate this instance's fields
            JsonSerializer serializer = new JsonSerializer();
            serializer.Populate(spell.Value.CreateReader(), this);
    }

    override public int GetDamage() {
        return RPNEvaluator.RPNEvaluator.Evaluate(damage.amount, 
                new Dictionary<string, int> { {"power", owner.spellPower } });
    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team) {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(0, projectile.trajectory, where, target - where, 15f, OnHit);
        yield return new WaitForEndOfFrame();
    }

    public void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(GetDamage(), damage.type));
        }
    }


    public ArcaneBolt(SpellCaster owner) {
        this.owner = owner;
        SetAttributes();
    }
}
