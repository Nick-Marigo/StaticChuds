using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

public class RelicLoader {
    private static List<Relic> relics;
    public static List<Relic> Relics { 
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

        int status = JsonToList(relicJson.text, out relics);
        if (status == -1) {
            Debug.Log("Failed to load relics from JSON");
            return;
        }
    }

    private static int JsonToList(string json, out List<Relic> relics) {
        relics = JsonConvert.DeserializeObject<List<Relic>>(json);

        if (relics == null) {
            return -1;
        }
        return 0;
    }
}
