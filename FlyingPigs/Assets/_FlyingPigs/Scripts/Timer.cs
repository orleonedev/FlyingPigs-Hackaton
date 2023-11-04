using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject boom;
    [SerializeField] private Slider slider;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TMP_Text timesupText;
    [SerializeField] private ImageShow imageShow;
    [SerializeField] private bool isTimeBasedMinigame = false;
    private float countdown = 10.0f; // Set the initial countdown time in seconds
    private bool isCounting = true;
    private int audioSourceNumber = -1;

    private void Start() {
        audioSourceNumber = audioManager.PlaySoundLoop(audioManager.tickingClip);
        timesupText.enabled = false;
    }

    private void Update()
    {
        if (isCounting) {
            countdown -= Time.deltaTime; // Decrease countdown by time passed since the last frame
            slider.value = countdown / 10f;

            if (countdown <= 0.0f) {
                // Countdown has reached 0, perform your desired action
                countdown = 0f;
                isCounting = false; // Stop the timer
                bomb.SetActive(false);
                boom.SetActive(true);
                if(isTimeBasedMinigame) {
                    imageShow.SwitchShow(true);
                }
                else {
                    timesupText.enabled = true;
                }
                audioManager.PlaySound(audioManager.timeUpClip);
                if (audioSourceNumber != -1) {
                    audioManager.StopSoundLoop(audioSourceNumber);
                }
                audioSourceNumber = -1;
            }
        }
    }

    public void StopTimer()
    {
        isCounting = false; // Call this function to stop the timer
        if (audioSourceNumber != -1) {
            audioManager.StopSoundLoop(audioSourceNumber);
        }
        audioSourceNumber = -1;
    }

    public void PauseTimer() {
        isCounting = !isCounting;
    }

    public void RestartTimer(float time = 10.0f)
    {
        isCounting = true; // Call this function to restart the timer
        countdown = time;
        bomb.SetActive(true);
        boom.SetActive(false);
        if(isTimeBasedMinigame) {
                    imageShow.SwitchShow(true);
                }
                else {
                    timesupText.enabled = true;
                }
        if (audioSourceNumber != -1) {
            audioManager.StopSoundLoop(audioSourceNumber);
        }
        audioSourceNumber = audioManager.PlaySoundLoop(audioManager.tickingClip);
    }

    public bool GetCounting() {
        return isCounting;
    }
}