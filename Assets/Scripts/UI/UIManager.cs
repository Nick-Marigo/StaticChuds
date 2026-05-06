using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

    [Header("UI Screens")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject difficultySelector;
    [SerializeField] GameObject rewardScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject waveStatsDisplay;

    public void UpdateUIState(GameManager.GameState newState)
    {
        Debug.Log("State changed");
        Debug.Log(newState);
        if(newState == GameManager.GameState.PREGAME)
        {
            background.SetActive(true);
            difficultySelector.SetActive(true);
        }
        else if (newState == GameManager.GameState.COUNTDOWN)
        {
            background.SetActive(false);
            difficultySelector.SetActive(false);
            waveStatsDisplay.SetActive(false);
        }
        else if (newState == GameManager.GameState.INWAVE)
        {
            
        }
        else if (newState == GameManager.GameState.WAVEEND)
        {
            background.SetActive(true);
            waveStatsDisplay.SetActive(true);
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnChangedState += UpdateUIState;
        //waveStatsDisplay.SetActive(false);
    }

    void OnDestory()
    {
        
    }
}
