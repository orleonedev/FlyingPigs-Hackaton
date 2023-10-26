using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    private float countdown = 5.0f; // Set the initial countdown time in seconds
    private bool isCounting = true;

    private void Update()
    {
        if (isCounting) {
            countdown -= Time.deltaTime; // Decrease countdown by time passed since the last frame
            score.text = Mathf.Floor(countdown).ToString();

            if (countdown <= 0.0f) {
                // Countdown has reached 0, perform your desired action
                Debug.Log("Countdown reached 0. Timer stopped.");
                isCounting = false; // Stop the timer
            }
        }
    }

    public void StopTimer()
    {
        isCounting = false; // Call this function to stop the timer
    }

    public void RestartTimer(float time = 5.0f)
    {
        isCounting = true; // Call this function to restart the timer
        countdown = time;
    }
}