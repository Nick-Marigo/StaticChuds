using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    public Spell Build(SpellCaster owner)
    {
        Debug.Log(SpellLoader.BaseSpells[0](owner));
        Debug.Log(SpellLoader.BaseSpells[1](owner));
        Debug.Log(SpellLoader.BaseSpells[2](owner));
        // TODO, always picks Arcane Bolt
        Spell spell = SpellLoader.BaseSpells[1](owner);
        // Wrap the base spell with a modifier
        spell = SpellLoader.SpellModifiers[0](owner, spell);
        // Wrapping a second time
        spell = SpellLoader.SpellModifiers[0](owner, spell);
        return spell;
    }

    public SpellBuilder()
    {        
    }

}
