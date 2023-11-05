using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PigSpawner : MonoBehaviour
{
    public static int level = 1;
    public static float timerElapsedTime = 0f;
    public static int lives = 1;
    public static Action OnGameOver;
    public PigHuntUImanager uiManager;
    [SerializeField] protected Timer timer;
    [SerializeField] protected MovingPig prefab;
    [SerializeField] protected ImageShow imageShow;
    private float speedModifier = 0.0f;
    [SerializeField] protected float time;
    [SerializeField] protected float yClamp;
    protected float elapsedTime;
    protected bool spawnDirection; // when true spawn from left, when false from right
    private bool gameOver = false;

    private void Start() {
        SetDifficultyLevel();

        var dir = UnityEngine.Random.Range(0.0f, 1.0f);

        if(dir >= 0.0f && dir <= 0.5f){
            spawnDirection = false;
        } else {
            spawnDirection = true;
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(!isGameOver()){
            if (!gameOver) {
                if(elapsedTime > time && timer.GetCounting()){
                    SpawnObject();
                    elapsedTime = 0.0f;
                } else if(!timer.GetCounting()){
                    gameOver = true;
                    timerElapsedTime += timer.GetTime();
                    StartCoroutine(EndLevelAfterTime(2.0f));
                }
            }
        } else if (!gameOver) {
            gameOver = true;
            timer.PauseTimer();
            imageShow.SwitchShow(false);
            timerElapsedTime += timer.GetTime();
            Invoke("GameOver", 2f);
        }
    }

    private void SetDifficultyLevel(){
        time -= 0.25f * (level - 1);
        speedModifier += 0.4f * (level - 1);
    }

    protected virtual void SpawnObject(){
        float offsetY = UnityEngine.Random.Range(yClamp, yClamp - 2);
        
        Vector2 pos = new Vector2(GetSpawnPosX(), this.transform.position.y + offsetY);

        MovingPig newPig = Instantiate(prefab, pos, Quaternion.identity, this.transform);
        newPig.speed += speedModifier;
    }

    protected float GetSpawnPosX(){
        float posX;

        if(spawnDirection){
            posX = UnityEngine.Random.Range(-2, 0);
            spawnDirection = false;
        } else {
            posX = UnityEngine.Random.Range(0, 2);
            spawnDirection = true;
        }

        return posX;
    }

    private void OnEndLevel(){
        level += 1;
        if(level <= 5) {
            lives = 1;
            uiManager.RestartGame();
        }
        else {
            OnGameOver?.Invoke();
        }
    }

    private bool isGameOver(){
        if(lives <= 0) {
            return true;
        } else {
            return false;
        }
    }

    private IEnumerator EndLevelAfterTime(float time){
        yield return new WaitForSeconds(time);

        OnEndLevel();
    }

    private void GameOver() {
        OnGameOver?.Invoke();
    }
}
