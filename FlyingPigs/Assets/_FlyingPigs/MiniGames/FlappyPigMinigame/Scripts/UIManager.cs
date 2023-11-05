using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Timer timer;
    [SerializeField] Coordinator coordinator;
    [SerializeField] GameObject playButton;

    private void Awake() {
        PigScript.OnDeath += OnGameOver;
    }

    private void OnDestroy() {
        PigScript.OnDeath -= OnGameOver;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnGameOver() {
        SerializableDictionary<GameStatsEnum,float> value = new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.TimeElapsed, PigScript.timeElapsed},
            {GameStatsEnum.GameHealth, (PigScript.level - 1) * 0.02f},
            {GameStatsEnum.GameCurrency, (PigScript.level - 1) * 3}
        };
        GameStatisticsManager.Instance.updateStatsWith(value);

        PigScript.SetLevel(1);
        PigScript.timeElapsed = 0f;

        Time.timeScale = 1f;
        coordinator.LoadScene("MainGameScene");

    }

}
