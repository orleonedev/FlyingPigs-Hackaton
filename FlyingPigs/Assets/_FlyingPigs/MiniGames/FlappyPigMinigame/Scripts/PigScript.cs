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

    // Update is called once per frame
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
    }

    private void OnCollisionEnter2D() {
        audioManager.PlaySound(audioManager.woodLogClip);
        imageShow.SwitchShow(false);
        StartCoroutine(PlayerDeath(2f));
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(){
        if(!timer.GetCounting()){
            StartCoroutine(EndLevelAfterTime(2.0f));
            Time.timeScale = 0f;
        }
    }

    private void Flap(){
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(Vector2.up * force);
    }

    public static void SetLevel(int newLevel) => level = newLevel;

    public static void LevelUp() => level++;

    private void OnEndLevel() {
        if(PigScript.level < 5){
            PigScript.LevelUp();
        } else {
            PigScript.SetLevel(1);
            // termina minigioco
        }
        
        uiManager.RestartGame();
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
