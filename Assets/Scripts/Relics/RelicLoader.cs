using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class RelicLoader {
    /* RelicLoader exposes a Dictionary of lambdas that
     * can be used to instantiate new relics from. The keys are
     * the relic names.*/
    private static Dictionary<string, Func<Relic>> relics;
    public static Dictionary<string, Func<Relic>> Relics { 
        get {
            if (relics == null) LoadRelics();
            return relics;
        } 
    }

    private static void LoadRelics() {
        TextAsset relicJson = Resources.Load<TextAsset>("relics");
        if (relicJson == null)
        {
            Debug.Log("Failed to get relics json from Resources");
            return;
        }

        int status = JsonToDictionary(relicJson.text, out relics);
        if (status == -1) {
            Debug.Log("Failed to load relics from JSON");
            return;
        }
    }

    private static int JsonToDictionary(string json, out Dictionary<string, Func<Relic>> relics) {
        relics = new();

        JArray relicData = JArray.Parse(json);
        foreach(JObject relic in relicData) {
            string name = (string)relic["name"]; 
            relics.Add(name, () => relic.ToObject<Relic>());
        }

        if (relics == null) {
            return -1;
        }
        return 0;
    }
}
