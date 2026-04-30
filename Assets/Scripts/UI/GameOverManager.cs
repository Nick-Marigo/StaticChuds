using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    WaveStats waveStats;
    [SerializeField]
    TextMeshProUGUI gameOverDisplay;
    [SerializeField]
    public PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.state == GameManager.GameState.GAMEOVER)
        {
            if(player.isDead)
            {
                gameOverDisplay.text = "You Lost!";
            }
            else
            {
                gameOverDisplay.text = "You Won!";
            }
            gameOverUI.SetActive(true);
            waveStats.DisplayStats();
        }
        else
        {
            gameOverUI.SetActive(false);
            //waveStats.HideStats();
        }
    }



    public void RestartScene()
    {
        GameManager.Instance.player = null;
        EventBus.Instance.Clear();
        GameManager.Instance.ClearEnemies();
        GameManager.Instance.state = GameManager.GameState.PREGAME;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
