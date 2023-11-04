using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Timer timer;
    [SerializeField] GameObject playButton;

    private void Awake(){
        PigScript.OnDeath += OnGameOver;
    }

    private void OnDestroy(){
        PigScript.OnDeath -= OnGameOver;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //timer.RestartTimer();
    }

    private void OnGameOver(){
        PigScript.SetLevel(1);
        Time.timeScale = 1f;
        RestartGame(); //CHANGE WITH MAIN SCREEN
    }

}
