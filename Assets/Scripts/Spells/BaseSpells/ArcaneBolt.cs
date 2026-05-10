using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ArcaneBolt : BaseSpell {
    // This instance is used to load defaults into from json
    private static ArcaneBolt configInstance;

    void setAttributes() {
        name = "Arcane Bolt";
        // Lazy load this spells attributes
        if (configInstance == null) {
            List<JProperty> spells = SpellLoader.GetSpells();
            JProperty spell = spells.Where(spell => (string)((JObject)spell.Value)["name"] == name).FirstOrDefault();
            if (spell == null) {
                Debug.Log("Failed to find spell of type " + name);
            }
            Debug.Log(spell);
            configInstance = spell.Value.ToObject<ArcaneBolt>();
        }
    }

    public ArcaneBolt(SpellCaster owner) {
        this.owner = owner;
        Debug.Log(configInstance);
        setAttributes();
        Debug.Log(configInstance);
        Debug.Log(configInstance.damage.amount);

    }
}
