using System.Collections.Generic;
using System.Linq;
using System;

public class ObjectByTypeFetcher {

   public static T FetchUnusedObject<T> (Dictionary<string, Dictionary<string, T>> dict, string affinity, string weakness) {
        var types = GameManager.Instance.types;
        if ( !types.Contains(affinity) || !types.Contains(weakness) ) {
            throw new ArgumentException($"affinity:{affinity} or weakness:{weakness}  are not of a valid type");
        }

        T obj;
        if (GetObjectOfType<T>(dict, affinity, out obj)) return obj;
        foreach (string type in types) {
            if (type == weakness) continue;
            if (GetObjectOfType<T>(dict, type, out obj)) return obj;
        }
        if (GetObjectOfType<T>(dict, weakness, out obj)) return obj;
        return default;
    }

    private static bool GetObjectOfType <T> (Dictionary<string, Dictionary<string, T>> dict, string type, out T obj) {
        var objDict = dict[type];
        obj = default;
        if (objDict.Count == 0) return false;
        int relicIndex = UnityEngine.Random.Range(0, objDict.Count);
        obj = objDict.ElementAt(relicIndex).Value;
        return true;
    }
}
