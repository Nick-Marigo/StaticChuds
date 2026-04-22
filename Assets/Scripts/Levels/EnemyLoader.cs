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
        string wrappedJson = "{ \"enemies\": " + json + "}";

        EnemyDataWrapper wrapper = JsonUtility.FromJson<EnemyDataWrapper>(wrappedJson);
        enemyDataList = wrapper.enemies;

        Debug.Log("Loaded " + enemyDataList.Count + " enemies");

        foreach (EnemyData enemy in enemyDataList)
        {
            Debug.Log("Loaded Enemy: " + enemy.name + ", Sprite: " + enemy.sprite + ", HP: " + enemy.hp + ", Speed: " + enemy.speed + ", Damage: " + enemy.damage);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
