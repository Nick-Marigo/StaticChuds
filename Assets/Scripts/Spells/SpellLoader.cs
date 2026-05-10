using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class SpellLoader
{
    /* This class works slightly different from the other loaders, it
     * populates the spells List with JProperties which are then fully parsed
     * by the spell classes. This is because the types of attributes in 
     * each spell type is unique to the spell */
    private static List<JProperty> spells;

    public static List<JProperty> GetSpells() {
        if (spells == null) {
            spells = LoadSpells();
        }
        return spells;
    }

    private static List<JProperty> LoadSpells() {
        TextAsset spellJson = Resources.Load<TextAsset>("spells");
        if (spellJson == null)
        {
            Debug.Log("Failed to get spells json from Resources");
            return null;
        }

        List<JProperty> spells;
        int status = JsonToJObjectList(spellJson.text, out spells);
        if (status == -1) {
            Debug.Log("Failed to load spell JProperites from JSON");
            return null;
        }
        return spells;
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
