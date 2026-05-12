using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    public Spell Build(SpellCaster owner)
    {
        Spell spell = SpellLoader.BaseSpells[0](owner);
        return spell;
    }

    public SpellBuilder()
    {        
    }

}
