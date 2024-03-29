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
    [SerializeField] Coordinator coordinator;
    [SerializeField] AudioManager audioManager;
    [SerializeField] Camera gameCamera;

    public static float timeElapsed = 0f;
    public static int level = 1;
    public static int spriteIndex;
    private bool levelEnd = false;
    private float cameraHeight;
    private float cameraWidth;

    void Start() {

        cameraHeight = gameCamera.orthographicSize * 2f;
        cameraWidth = cameraHeight * gameCamera.aspect;

        levelEnd = false;
        InvokeRepeating("spawnObject", this.spawnInterval * (3 / (float) level), this.spawnInterval * (3 / (float) level));
    }

    void Update() {
        if(!timer.GetCounting() && !levelEnd){
            level ++;
            if (level <= 5) {
                timeElapsed += 10 - timer.GetTime();
                StartCoroutine(RestartLevelAfterTime(2.0f));
            }
            else {
                audioManager.PlaySound(audioManager.endMinigameSucc, 1f);
                timeElapsed += 10 - timer.GetTime();
                StartCoroutine(EndLevelAfterTime(2.0f));
            }
            levelEnd = true;
            Time.timeScale = 0f;
            
        }
    }

    private void spawnObject() {
        GameObject newObject = Instantiate(this.fruitToSpawn);
        newObject.transform.position = new Vector2(Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
        newObject.transform.localScale = new Vector3(cameraWidth / 20, cameraWidth / 20, cameraWidth / 20);
        spriteIndex = Random.Range(0, this.objectSprites.Length);
        newObject.GetComponent<CutFruit>().spriteIndex = spriteIndex;
        Sprite objectSprite = objectSprites[spriteIndex];
        newObject.GetComponent<SpriteRenderer>().sprite = objectSprite;
    }

    public void GameOver() {
        audioManager.PlaySound(audioManager.endMinigameFail, 1f);
        timeElapsed += 10 - timer.GetTime();
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
        Debug.Log("loot Ninja time elapsed "+ timeElapsed);
        SerializableDictionary<GameStatsEnum,float> value = new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.TimeElapsed, timeElapsed},
            {GameStatsEnum.GameHealth, (level - 1) * 0.02f},
            {GameStatsEnum.GameCurrency, (level - 1) * 3}
        };
        GameStatisticsManager.Instance.updateStatsWith(value);

        level = 1;
        timeElapsed = 0f;

        Time.timeScale = 1f;
        coordinator.LoadScene("MainGameScene");
    }

}
