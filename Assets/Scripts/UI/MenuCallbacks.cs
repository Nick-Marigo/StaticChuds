using Camera;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class MenuCallbacks : MonoBehaviour {
        [SerializeField] private string startSceneName = "StartMenu";
        [SerializeField] private string playSceneName = "Main";
        [SerializeField] private string creditsSceneName = "Credits";
        [SerializeField] private string settingsSceneName = "SettingsMenu";
        
        public void StartMenuScene() {
            SceneManager.LoadScene(startSceneName);
        }
        
        public void StartPlayScene() {
            SceneManager.LoadScene(playSceneName);
        }

        public void StartCreditsScene() {
            SceneManager.LoadScene(creditsSceneName);
        }

        public void StartSettingsScene() {
            SceneManager.LoadScene(settingsSceneName);
        }
        
        public void QuitGame() {
            Application.Quit();
        }

        public void SetCameraZoom(float zoom) {
            CameraController.Instance.size = zoom;
        }

        public void SetDisplayDamageNumbers(bool doShow) {
            DamageNumbers.displayNumbers = doShow;
        }
    }
}