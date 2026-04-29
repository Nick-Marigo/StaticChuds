using UnityEngine;
using TMPro;

public class WaveStats : MonoBehaviour
{
    private int waveNumber;
    //private List<int> enemiesKilled;
    private int totalEnemies;
    private int totalDamageDealt;
    private float totalTime;
    private float waveTime;

    public TextMeshProUGUI waveDisplay; // Reference to UI element, assign in inspector

    public void startLevel()
    {
        totalTime = Time.time;
    }
    public void startWave(int currentWave)
    {
        waveNumber = currentWave;
        totalEnemies = 0;
        totalDamageDealt = 0;
        waveTime = Time.time;
    }

    public void endWave()
    {
        waveTime = Time.time - waveTime;
        totalTime = Time.time - totalTime;
    }

    public void updateTotalEnemies()
    {
        totalEnemies++;
    }

    public void updateTotalDamageDealt(int damage)
    {
        totalDamageDealt += damage;
    }

    public void DisplayStats()
    {
        string displayText = "Wave " + waveNumber + " Stats:\n";
        displayText += "Total Enemies: " + totalEnemies + "\n";
        displayText += "Total Damage Dealt: " + totalDamageDealt + "\n";
        displayText += "Wave Time: " + waveTime + " seconds\n";
        displayText += "Total Time: " + totalTime + " seconds\n";
        waveDisplay.text = displayText;
    }

    void update()
    {
        if(GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {
            DisplayStats();
        }
    }

}
