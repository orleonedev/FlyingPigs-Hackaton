using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject boom;
    [SerializeField] private Slider slider;
    [SerializeField] private AudioManager audioManager;
    private float countdown = 10.0f; // Set the initial countdown time in seconds
    private bool isCounting = true;

    private void Start() {
        audioManager.PlaySound(audioManager.tickingClip);
    }

    private void Update()
    {
        if (isCounting) {
            countdown -= Time.deltaTime; // Decrease countdown by time passed since the last frame
            slider.value = countdown/10f;

            if (countdown <= 0.0f) {
                // Countdown has reached 0, perform your desired action
                countdown = 0f;
                isCounting = false; // Stop the timer
                bomb.SetActive(false);
                boom.SetActive(true);
                audioManager.PlaySound(audioManager.timeUpClip);
            }
        }
    }

    public void StopTimer()
    {
        isCounting = false; // Call this function to stop the timer
    }

    public void RestartTimer(float time = 10.0f)
    {
        isCounting = true; // Call this function to restart the timer
        countdown = time;
        bomb.SetActive(true);
        boom.SetActive(false);
    }

    public bool GetCounting() {
        return isCounting;
    }
}