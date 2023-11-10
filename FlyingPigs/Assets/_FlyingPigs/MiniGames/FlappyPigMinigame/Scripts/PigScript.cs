using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigScript : MonoBehaviour
{
    public Timer timer;
    public UIManager uiManager;
    public static event Action OnDeath;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float force;
    [SerializeField] private float yBound;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ImageShow imageShow;
    public static int level = 1;
    public static float timeElapsed = 0f;
    public bool checkCollision = false;
    private bool gameOver = false;

    private void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began && rigidBody.position.y < yBound){
                if (Time.timeScale != 0f) {
                    audioManager.PlaySound(audioManager.jumpgClip);
                    Flap();
                }
            }
        }

        if(!timer.GetCounting() && !gameOver){
            gameOver = true;
            if (level >= 5) {
                audioManager.PlaySound(audioManager.endMinigameSucc, 1f);
            }
            timeElapsed += 10 - timer.GetTime();
            StartCoroutine(EndLevelAfterTime(2.0f));
            Time.timeScale = 0f;
        }
    }

    private void OnCollisionEnter2D() {
        if(!checkCollision) {
            checkCollision = true;
            audioManager.PlaySound(audioManager.woodLogClip);
            audioManager.PlaySound(audioManager.endMinigameFail, 1f);
            imageShow.SwitchShow(false);
            timeElapsed += 10 - timer.GetTime();
            StartCoroutine(PlayerDeath(2f));
            Time.timeScale = 0f;
        }
    }

    private void Flap() {
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(Vector2.up * force);
    }

    public static void SetLevel(int newLevel) => level = newLevel;

    public static void LevelUp() => level++;

    private void OnEndLevel() {
        if(PigScript.level < 5) {
            PigScript.LevelUp();
            uiManager.RestartGame();
        }
        else {
            OnDeath?.Invoke();
        }
    }

    private IEnumerator PlayerDeath(float time){
        yield return new WaitForSecondsRealtime(time);

        OnDeath?.Invoke();
    }

    private IEnumerator EndLevelAfterTime(float time){
        yield return new WaitForSecondsRealtime(time);

        OnEndLevel();
    }
}
