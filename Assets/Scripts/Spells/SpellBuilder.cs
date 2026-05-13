using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    public Spell Build(SpellCaster owner)
    {
        // TODO, always picks Arcane Bolt
        Spell spell = SpellLoader.BaseSpells[0](owner);
        // Wrap the base spell with a modifier
        spell = SpellLoader.SpellModifiers[UnityEngine.Random.Range(0, SpellLoader.SpellModifiers.Count)](owner, spell);
        // Wrapping a second time
        //spell = SpellLoader.SpellModifiers[0](owner, spell);
        Debug.Log("Built spell with modifier: " + spell.GetType().Name);
        return spell;
    }

    public SpellBuilder()
    {        
    }

}
