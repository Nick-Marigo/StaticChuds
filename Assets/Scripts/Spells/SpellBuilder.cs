using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{
    // Builds an Arcane Bolt with no modifiers for starting spell
    public static Spell BuildArcaneBolt(SpellCaster owner)
    {
        return SpellLoader.BaseSpells[0](owner);
    }

    // Builds an Arcane Bolt with modifiers
    public static Spell Build(SpellCaster owner)
    {
        int spellIndex = UnityEngine.Random.Range(0, SpellLoader.BaseSpells.Count);
        Spell spell = SpellLoader.BaseSpells[spellIndex](owner);

        // Wrap the base spell with a modifier
        int modifierIndex = UnityEngine.Random.Range(0, SpellLoader.SpellModifiers.Count);
        spell = SpellLoader.SpellModifiers[0](owner, spell);

        // 50% chance that is gets a second modifier
        if (UnityEngine.Random.value < 0.5f)
        {
            int modifierIndexTwo = UnityEngine.Random.Range(0, SpellLoader.SpellModifiers.Count);
            spell = SpellLoader.SpellModifiers[modifierIndexTwo](owner, spell);
        }

        Debug.Log("Built spell with modifier: " + spell.GetType().Name);
        return spell;
    }
}
