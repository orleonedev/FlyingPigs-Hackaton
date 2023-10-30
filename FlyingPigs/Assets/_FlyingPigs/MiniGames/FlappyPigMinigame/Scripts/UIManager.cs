using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timer.RestartTimer();
    }

    private void OnGameOver(){
        PigScript.SetLevel(1);
        playButton.SetActive(true);
    }

}
