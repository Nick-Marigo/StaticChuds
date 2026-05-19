using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ClassesLoader
{

    private static Dictionary<string, Classes> classes;

    public static Dictionary<string, Classes> GetClasses()
    {
        if (classes == null)
        {
            classes = LoadClasses();
        }
        return classes;
    }

    private static Dictionary<string, Classes> LoadClasses()
    {
        TextAsset classesJson = Resources.Load<TextAsset>("classes");

        if (classesJson == null)
        {
            Debug.Log("Failed to get classes json from Resources");
            return null;
        }

        int status = ClassesLoader.JsonToList(classesJson.text, out classes);
        if (status == -1)
        {
            Debug.Log("Failed to load levels from Json");
            return null;
        }

        foreach (KeyValuePair<string, Classes> kvp in classes)
        {
            string name = kvp.Key;
            Classes info = kvp.Value;
            
            Debug.Log("Class: " + name);
            Debug.Log("sprite: " + info.sprite);
            Debug.Log("health: " + info.health);
            Debug.Log("mana: " + info.mana);
            Debug.Log("mana_reg: " + info.mana_regeneration);
            Debug.Log("spellpower: " + info.spellpower);
            Debug.Log("speed: " + info.speed);

        }

        return classes;
    }

    private static int JsonToList(string json, out Dictionary<string, Classes> classesData)
    {
        classesData = JsonConvert.DeserializeObject<Dictionary<string, Classes>>(json);

        if (classesData == null)
        {
            return -1;
        }

        return 0;
    }

}
