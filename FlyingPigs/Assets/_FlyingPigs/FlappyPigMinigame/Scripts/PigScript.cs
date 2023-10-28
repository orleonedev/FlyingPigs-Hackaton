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
    //public Spawner pipeSpawner;

    private void Start(){
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && rigidBody.position.y < yBound){
            Flap();
        }
    }

    private void OnCollisionEnter2D(){
        OnDeath?.Invoke();

        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(){
        if(!timer.GetCounting()){
            StartCoroutine(EndLevelAfterTime(1.5f));
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
            Debug.Log("Level: " + PigScript.level);
            Spawner.time -= 0.25f;
            Debug.Log("Time: " + Spawner.time);
        } else {
            // termina minigioco
        }
        
        //uiManager.SetLevel(PigScript.level);
        uiManager.RestartGame();
    }

    private IEnumerator EndLevelAfterTime(float time){
        yield return new WaitForSeconds(time);

        OnEndLevel();
    }
}
