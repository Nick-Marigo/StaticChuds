using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class SpellLoader
{
    /* This class works slightly different from the other loaders, it
     * populates the spells List with JProperties which are then fully parsed
     * by the spell classes. This is because the types of attributes in 
     * each spell type is unique to the spell */
    private static List<JProperty> spells;
    private static List<Func<SpellCaster, BaseSpell>> baseSpells;
    private static List<Func<Spell, ModifierSpell>> modifierSpells;

    /* These lists of lambdas are used to randomly
     * instantiate new spells and decorators from */
    public static List<Func<SpellCaster, BaseSpell>> BaseSpells { get
        {
            if (spells == null) LoadSpells();
            return baseSpells;
        }
    }
    public static List<Func<Spell, ModifierSpell>> ModifierSpells { get
        {
            if (spells == null) LoadSpells();
            return modifierSpells;
        }
    }

    /* Maps all spell entries in the json to a spell
     * class. This adds a lamda to create the read spell
     * and sets that spell's config object */
    private static void MapSpellsToClass() {
        foreach (JProperty spell in spells) {
            JObject config = (JObject) spell.Value;
            switch(spell.Name) {
                case "arcane_bolt":
                    ArcaneBolt.config = config;
                    baseSpells.Add( (owner) => 
                            new ArcaneBolt(owner) );
                    break;
                    /*
                case "damage_amp":
                    modifierSpells.Add( (innerSpell) => new DamageAmp(innerSpell, config) );
                    break;
                    */
            }
        }
    }

    private static void LoadSpells() {
        baseSpells = new();
        modifierSpells = new();
        TextAsset spellJson = Resources.Load<TextAsset>("spells");
        if (spellJson == null)
        {
            Debug.Log("Failed to get spells json from Resources");
            return;
        }

        List<JProperty> spells;
        int status = JsonToJObjectList(spellJson.text, out spells);
        if (status == -1) {
            Debug.Log("Failed to load spell JProperites from JSON");
            return;
        }
        SpellLoader.spells = spells;
        MapSpellsToClass();
    }

    private static int JsonToJObjectList(string json, out List<JProperty> spells)
    {
        spells = new List<JProperty>();
        JObject spellData = JObject.Parse(json); 
        foreach (JProperty spell in spellData.Children()) {
            spells.Add(spell);
        }
        if (spells.Count == 0) {
            return -1;
        }
        return 0;
    }
}
