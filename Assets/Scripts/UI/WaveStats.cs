using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class WaveStats : MonoBehaviour
{
    private int waveNumber;
    private Dictionary<string, int> enemiesKilledByType;
    private int totalEnemies;
    private int totalWaveEnemies;
    private int totalDamageDealt;
    private int totalWaveDamageDealt;
    private float totalTime;
    private float waveStartTime;
    private float waveTime;

    [SerializeField]
    TextMeshProUGUI waveDisplay; // Reference to UI element, assign in inspector

    public void StartWave(int wave)
    {
        waveDisplay.gameObject.SetActive(false);
        waveNumber = wave;
        totalWaveEnemies = 0;
        totalWaveDamageDealt = 0;
        waveStartTime = Time.time;
        enemiesKilledByType = new Dictionary<string, int>();
    }

    public void EndWave()
    {
        waveTime = Time.time - waveStartTime;
        totalTime += waveTime;
    }

    public void UpdateTotalEnemies(string enemyName)
    {
        totalWaveEnemies++;
        totalEnemies++;

        if (!enemiesKilledByType.ContainsKey(enemyName))
        {
            enemiesKilledByType[enemyName] = 0;
        }

        enemiesKilledByType[enemyName]++;
    }

    public void UpdateTotalDamageDealt(int damage)
    {
        totalWaveDamageDealt += damage;
        totalDamageDealt += damage;
    }

    public void DisplayStats()
    {
        var ts = System.TimeSpan.FromSeconds(waveTime);
        
        waveDisplay.gameObject.SetActive(true);
        string displayText = "Wave " + waveNumber + " Stats:\n";
        displayText += "Wave Enemies Killed: " + totalWaveEnemies + "\n";
        foreach (var enemy in enemiesKilledByType)
        {
            displayText += Capitalize(enemy.Key) + "s Killed: " + enemy.Value + "\n";
        }
        displayText += "Total Enemies Killed: " + totalEnemies + "\n";
        displayText += "Wave Damage Dealt: " + totalWaveDamageDealt + "\n";
        displayText += "Total Damage Dealt: " + totalDamageDealt + "\n";
        displayText += "Wave Time: " + waveTime.ToString("F0") + " seconds\n";
        displayText += "Total Time: " + totalTime.ToString("F0") + " seconds\n";
        waveDisplay.text = displayText;
    }

    public void HideStats()
    {
        waveDisplay.gameObject.SetActive(false);
    }

    private string Capitalize(string str)
    {
        return char.ToUpper(str[0]) + str.Substring(1);
    }

}
