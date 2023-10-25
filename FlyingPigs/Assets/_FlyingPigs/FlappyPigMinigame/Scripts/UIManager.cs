using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] private TMP_Text score;

    private void Awake(){
        PigScript.OnDeath += OnGameOver;
        PigScript.OnScore += OnScore;
    }

    private void OnDestroy(){
        PigScript.OnDeath -= OnGameOver;
        PigScript.OnScore -= OnScore;
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void OnGameOver() => playButton.SetActive(true);

    private void OnScore() => score.text = (int.Parse(score.text) + 1).ToString();
}
