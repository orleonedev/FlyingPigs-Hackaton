using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigHuntUImanager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text livesScore;
    [SerializeField] private Coordinator coordinator;

    void Start()
    {
        PigSpawner.OnGameOver += OnGameOver;
        MovingPig.OnMissPig += OnMissPig;
    }

    private void OnDestroy(){
        PigSpawner.OnGameOver -= OnGameOver;
        MovingPig.OnMissPig -= OnMissPig;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //timer.RestartTimer();
    }

    private void OnGameOver(){
        SerializableDictionary<GameStatsEnum,float> value = new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.TimeElapsed, PigSpawner.timerElapsedTime},
            {GameStatsEnum.GameHealth, (PigSpawner.level - 1) * 0.02f},
            {GameStatsEnum.GameCurrency, (PigSpawner.level - 1) * 3}
        };
        GameStatisticsManager.Instance.updateStatsWith(value);

        PigSpawner.level = 1;
        PigSpawner.lives = 1;
        PigSpawner.timerElapsedTime = 0f;

        Time.timeScale = 1f;
        coordinator.LoadScene("MainGameScene");
    }

    private void OnMissPig(){
        livesScore.text = (int.Parse(livesScore.text) - 1).ToString();
    }
}
