using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnFruit : MonoBehaviour
{
    [SerializeField] private GameObject fruitToSpawn;
    [SerializeField] private float spawnInterval, objectMinX, objectMaxX, objectY;
    [SerializeField] private Sprite[] objectSprites;
    [SerializeField] private ImageShow imageShow;
    [SerializeField] private Timer timer;

    public static int level = 1;
    private bool levelEnd = false;

    void Start() {
        levelEnd = false;
        InvokeRepeating("spawnObject", this.spawnInterval * (3 / (float) level), this.spawnInterval * (3 / (float) level));
    }

    void Update() {
        if(!timer.GetCounting() && !levelEnd){
            if (level < 5) {
                level ++;
                StartCoroutine(RestartLevelAfterTime(2.0f));
            }
            else {
                StartCoroutine(EndLevelAfterTime(2.0f));
            }
            levelEnd = true;
            Time.timeScale = 0f;
            
        }
    }

    private void spawnObject() {
        GameObject newObject = Instantiate(this.fruitToSpawn);
        newObject.transform.position = new Vector2(Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
        Sprite objectSprite = objectSprites[Random.Range(0, this.objectSprites.Length)];
        newObject.GetComponent<SpriteRenderer>().sprite = objectSprite;
    }

    public void GameOver() {
        CancelInvoke();
        imageShow.SwitchShow(false);
        Time.timeScale = 0f;
        StartCoroutine(EndLevelAfterTime(2f));
    }

    private IEnumerator RestartLevelAfterTime(float time){
        yield return new WaitForSecondsRealtime(time);

        RestartLevel();
    }

    private void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator EndLevelAfterTime(float time){
        yield return new WaitForSecondsRealtime(time);

        EndLevel();
    }

    private void EndLevel() {
        Time.timeScale = 1f;
        level = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //CHANGE WITH MAIN SCENE
    }

}
