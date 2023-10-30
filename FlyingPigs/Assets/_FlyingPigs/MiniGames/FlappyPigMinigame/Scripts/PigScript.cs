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
    public static int level = 1;

    private void Start(){
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            //Vector3 value = cam.ScreenToWorldPoint(new Vector3(touch.position.x, 0f, 0f));
            
            if(touch.phase == TouchPhase.Began && rigidBody.position.y < yBound){
                Flap();
            }
        }
    }

    private void OnCollisionEnter2D(){
        OnDeath?.Invoke();

        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(){
        if(!timer.GetCounting()){
            StartCoroutine(EndLevelAfterTime(2.0f));
        }
    }

    private void Flap(){
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(Vector2.up * force);
    }

    public static void SetLevel(int newLevel) => level = newLevel;

    public static void LevelUp() => level++;

    private void OnEndLevel(){
        if(PigScript.level < 5){
            PigScript.LevelUp();
        } else {
            PigScript.SetLevel(1);
            // termina minigioco
        }
        
        uiManager.RestartGame();
    }

    private IEnumerator EndLevelAfterTime(float time){
        yield return new WaitForSeconds(time);

        OnEndLevel();
    }
}
