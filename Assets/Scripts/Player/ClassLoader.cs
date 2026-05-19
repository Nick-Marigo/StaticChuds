using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ClassesLoader
{

    private static Dictionary<string, List<Classes>> classes;

    public static Dictionary<string, List<Classes>> GetClasses()
    {
        if (classes == null)
        {
            classes = LoadClasses();
        }
        return classes;
    }

    private static Dictionary<string, List<Classes>> LoadClasses()
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

        return classes;
    }

    private static int JsonToList(string json, out Dictionary<string, List<Classes>> classesData)
    {
        classesData = JsonConvert.DeserializeObject<Dictionary<string, List<Classes>>>(json);

        if (classesData == null)
        {
            return -1;
        }

        return 0;
    }

}
