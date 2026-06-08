using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class MenuCallbacks : MonoBehaviour {
        [SerializeField] private string startSceneName = "StartMenu";
        [SerializeField] private string playSceneName = "Main";
        [SerializeField] private string creditsSceneName = "Credits";
        
        public void StartMenuScene() {
            SceneManager.LoadScene(startSceneName);
        }
        
        public void StartPlayScene() {
            SceneManager.LoadScene(playSceneName);
        }

        public void StartCreditsScene() {
            SceneManager.LoadScene(creditsSceneName);
        }
        
        public void QuitGame() {
            Application.Quit();
        }
    }
}