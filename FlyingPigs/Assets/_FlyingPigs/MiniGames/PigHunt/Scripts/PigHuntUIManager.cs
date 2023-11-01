using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PigHuntUImanager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text livesScore;

    // Start is called before the first frame update
    void Start()
    {
        PigSpawner.OnGameOver += OnGameOver;
        ClickableObject.OnMissShot += OnMissShot;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy(){
        PigSpawner.OnGameOver -= OnGameOver;
        ClickableObject.OnMissShot -= OnMissShot; 
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timer.RestartTimer();
    }

    private void OnGameOver(){
        PigSpawner.level = 1;
        PigSpawner.lives = 2;
        RestartGame();
    }

    private void OnMissShot(){
        livesScore.text = (int.Parse(livesScore.text) - 1).ToString();
    }
}