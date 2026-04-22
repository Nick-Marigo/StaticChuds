using UnityEngine;
using System.Collections.Generic;
public class EnemyLoader : MonoBehaviour
{
    private List<EnemyData> enemyDataList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("enemies");

        if(jsonFile == null)
        {
            Debug.Log("Failed to load enemies.json");
            return;
        }

        string json = jsonFile.text;
        Debug.Log(json);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
