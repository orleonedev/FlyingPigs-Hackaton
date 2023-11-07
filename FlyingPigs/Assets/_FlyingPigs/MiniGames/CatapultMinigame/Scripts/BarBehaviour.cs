using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Color low;
    [SerializeField] private Color high;
    [SerializeField] private Timer timer;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Coordinator coordinator;
    private ImageShow imageShow;
    static private int level = 1;
    static public float timeElapsed = 0f;
    private float count = 0.1f;
    private int rising = 1;
    private bool shouldMove = true;
    private bool lastResult = false;
    private int loopNum = -1;
    private bool gameOver = false;

    void Start() {
        imageShow = GetComponentInChildren<ImageShow>();
        loopNum = audioManager.PlaySoundLoop(audioManager.catapultLoop);
    }

    public void ResetForNextLevel() {
        if (lastResult) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else {
            EndMinigame();
        }
    }
    
    private void Update() {
        SetValue(count, 100);
        if (count >= 100 || count <= 0) {
            rising = -rising;
        }

        if (shouldMove) {
            count += 0.1f * rising * level / 2;
        }

        if (!timer.GetCounting() && !gameOver) {
            gameOver = true;
            timeElapsed += 10 - timer.GetTime();
            Debug.Log(timeElapsed);
            timer.PauseTimer();
            Invoke("EndMinigame", 2f);
            shouldMove = false;
            if (loopNum != -1) {
                audioManager.StopSoundLoop(loopNum);
                loopNum = -1;
            }
        }
    }

    public void SetValue(float value, float maxValue) {
        slider.value = value;
        slider.maxValue = maxValue;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, value/maxValue);
    }

    public void OnPress() {
        if (loopNum != -1) {
            audioManager.StopSoundLoop(loopNum);
            loopNum = -1;
        }
        audioManager.PlaySound(audioManager.catapultThrow);
        if (shouldMove) {
            shouldMove = false;
            if (count > 80) {
                lastResult = true;
                imageShow.SwitchShow(lastResult);
                level++;
                animator.SetTrigger("succ");
                Invoke("CastleSound", 0.5f);
            }
            else {
                lastResult = false;
                imageShow.SwitchShow(lastResult);
                animator.SetTrigger("fail");
                Invoke("SplatSound", 0.3f);
            }
            if (level <= 5 && lastResult) {
                Invoke("ResetForNextLevel", 2f);
            }
            else {
                Invoke("EndMinigame", 2f);
            }
            timeElapsed += 10 - timer.GetTime();
            Debug.Log(timeElapsed);
            timer.PauseTimer();
        }
    }

    public void EndMinigame() {
        if (level - 1 != 5 && timer.GetCounting()) {
            imageShow.SwitchShow(false);
        }
        Debug.Log("Catapult time elapsed "+ timeElapsed);
        SerializableDictionary<GameStatsEnum,float> value = new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.TimeElapsed, timeElapsed},
            {GameStatsEnum.GameHealth, (level - 1) * 0.02f},
            {GameStatsEnum.GameCurrency, (level - 1) * 3}
        };
        GameStatisticsManager.Instance.updateStatsWith(value);

        level = 1;
        timeElapsed = 0f;

        coordinator.LoadScene("MainGameScene");
    }

    public void SplatSound() {
        audioManager.PlaySound(audioManager.playerWallImpact);
    }

    public void CastleSound() {
        audioManager.PlaySound(audioManager.castleDestruction);
    }
}
