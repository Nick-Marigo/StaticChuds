using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class RelicLoader {
    /* RelicLoader exposes a Dictionary of lambdas that
     * can be used to instantiate new relics from. The keys are
     * the relic names.*/
    private static Dictionary<string, Func<Relic>> _relics;
    public static Dictionary<string, Func<Relic>> Relics { 
        get {
            if (_relics == null) LoadRelics();
            return _relics;
        } 
    }

    public static List<string> _relicNames = new();
    public static List<string> RelicNames {
        get {
            if (_relics == null) LoadRelics();
            return _relicNames;
        }
    }

    private static void LoadRelics() {
        TextAsset relicJson = Resources.Load<TextAsset>("relics");
        if (relicJson == null)
        {
            Debug.Log("Failed to get relics json from Resources");
            return;
        }

        int status = JsonToDictionary(relicJson.text, out _relics);
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
            _relicNames.Add(name);
        }

        if (relics == null) {
            return -1;
        }
        return 0;
    }
}
