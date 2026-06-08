using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

// MAX: TODO remove old relic loading
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

    private static Dictionary<string, Dictionary<string, Func<Relic>>> _relicsByType;
    public static Dictionary<string, Dictionary<string, Func<Relic>>> RelicsByType {
        get {
            if (_relicsByType == null) LoadRelics();
            return _relicsByType;
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
        _relicsByType = new();
        foreach (string type in GameManager.Instance.types) {
            _relicsByType.Add(type, new());
        }

        JArray relicData = JArray.Parse(json);
        foreach(JObject relic in relicData) {
            string name = (string)relic["name"]; 
            string type = (string)relic["type"];
            relics.Add(name, () => relic.ToObject<Relic>());
            _relicsByType[type].Add(name, () => relic.ToObject<Relic>());
            _relicNames.Add(name);
        }

        foreach (var relicDict in _relicsByType) {
            foreach (var relicKVPair in relicDict.Value) {
            // Debug.Log($"{relicKVPair.Key} || {relicKVPair.Value().name}");
            }
        }
        if (relics == null) {
            return -1;
        }
        return 0;
    }
}
