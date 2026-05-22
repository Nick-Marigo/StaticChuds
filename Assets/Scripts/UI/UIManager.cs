using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("UI Screens")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject classSelector;
    [SerializeField] GameObject difficultySelector;
    [SerializeField] GameObject rewardScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject waveStatsDisplay;

    [Header("Universal Button")]
    [SerializeField] GameObject universalButton;
    [SerializeField] TextMeshProUGUI buttonText;

    [Header("External References")]
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] GameOverManager gameOverManager;
    [SerializeField] RewardScreenManager rewardScreenManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnChangedState += UpdateUIState;

        UpdateUIState(GameManager.Instance.state);
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnChangedState -= UpdateUIState;
        }
    }

    public void UpdateUIState(GameManager.GameState newState)
    {
        classSelector.SetActive(false);
        difficultySelector.SetActive(false);
        rewardScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        waveStatsDisplay.SetActive(false);
        universalButton.SetActive(false);

        switch (newState)
        {
            case GameManager.GameState.PREGAME:
                background.SetActive(true);
                classSelector.SetActive(true);
                break;

            case GameManager.GameState.COUNTDOWN:
                background.SetActive(false);
                difficultySelector.SetActive(false);
                waveStatsDisplay.SetActive(false);
                break;

            case GameManager.GameState.INWAVE:
                break;

            case GameManager.GameState.WAVEEND:
                background.SetActive(true);
                rewardScreen.SetActive(true);
                universalButton.SetActive(true);
                buttonText.text = "Skip Reward";
                rewardScreenManager.ShowReward();
                break;
            case GameManager.GameState.RELICREWARD:
                background.SetActive(true);
                rewardScreen.SetActive(true);
                universalButton.SetActive(true);
                buttonText.text = "Skip Reward";
                rewardScreenManager.ShowRelicReward();
                break;
            case GameManager.GameState.WAVESTATS:
                background.SetActive(true);
                waveStatsDisplay.SetActive(true);
                universalButton.SetActive(true);
                buttonText.text = "Next Wave";
                break;

            case GameManager.GameState.GAMEOVER:
                background.SetActive(true);
                waveStatsDisplay.SetActive(true);
                gameOverScreen.SetActive(true);
                universalButton.SetActive(true);
                buttonText.text = "Restart Game";
                break;
        }
    }

    public void OnUniversalButtonClick()
    {
        if (GameManager.GameState.WAVEEND == GameManager.Instance.state)
        {
                rewardScreenManager.ClearReward();
                GameManager.Instance.state = GameManager.GameState.WAVESTATS;
        }
        else if (GameManager.GameState.RELICREWARD == GameManager.Instance.state)
        {
            rewardScreenManager.ClearReward();
            GameManager.Instance.state = GameManager.GameState.WAVESTATS;
        }
        else if (GameManager.GameState.WAVESTATS == GameManager.Instance.state)
        {
            waveSpawner.NextWave();
        }
        else if (GameManager.GameState.GAMEOVER == GameManager.Instance.state)
        {
            gameOverManager.RestartScene();
        }
    }
}
