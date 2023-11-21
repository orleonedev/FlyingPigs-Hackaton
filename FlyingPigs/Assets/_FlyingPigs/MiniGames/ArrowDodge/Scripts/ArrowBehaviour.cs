using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private Transform arrows;
    [SerializeField] private Transform arrowsLateral;
    [SerializeField] private Timer timer;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ImageShow imageShow;
    [SerializeField] private InputManagerArrow inputManagerArrow;
    [SerializeField] private Coordinator coordinator;
    public Sprite spriteBigArrow;
    public Sprite spriteEndArrow;
    private bool descending = false;
    public float speed;
    private bool levelEnded = false;
    static public int level = 1;
    static public float timeElapsed = 0f;

    void Start()
    {
        Transform[] children = arrows.GetComponentsInChildren<Transform>();
        Destroy(children[UnityEngine.Random.Range(1, children.Count())].gameObject);
        speed = 1.5f;
        audioManager.PlaySound(audioManager.arrowFire);
    }

    void Update()
    {
        if(!descending) {
            if(arrows.position.y > 6f) {
                descending = true;
                speed = -speed;
                int count = arrows.transform.childCount;
                for(int i = 0; i < count; i++) {
                    Transform child = arrows.transform.GetChild(i);
                    child.gameObject.GetComponent<SpriteRenderer>().sprite = spriteBigArrow;
                }
                int countLateral = arrowsLateral.transform.childCount;
                for(int i = 0; i < countLateral; i++) {
                    Transform child = arrowsLateral.transform.GetChild(i);
                    child.gameObject.GetComponent<SpriteRenderer>().sprite = spriteBigArrow;
                }
            }
        }
        else {
            if(arrows.position.y < -3.8f) {
                if (!levelEnded) {

                    int count = arrows.transform.childCount;
                    for(int i = 0; i < count; i++) {
                        Transform child = arrows.transform.GetChild(i);
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = spriteEndArrow;
                    }
                    int countLateral = arrowsLateral.transform.childCount;
                    for(int i = 0; i < countLateral; i++) {
                        Transform child = arrowsLateral.transform.GetChild(i);
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = spriteEndArrow;
                    }
                    speed = 0;
                    audioManager.PlaySound(audioManager.arrowGroundImpact);
                    level += 1;
                    if (level <= 5) {
                    imageShow.SwitchShow(true);
                    timeElapsed += 10 - timer.GetTime();
                    timer.PauseTimer();
                    Invoke("RestartGame", 2f);
                    }
                    else {
                        imageShow.SwitchShow(true);
                        timeElapsed += 10 - timer.GetTime();
                        timer.PauseTimer();
                        audioManager.PlaySound(audioManager.endMinigameSucc, 1f);
                        Invoke("EndGame", 2f);
                    }
                }
                levelEnded = true;
                inputManagerArrow.canMove = false;
            }
        }
        arrows.transform.position += transform.up * speed * level * Time.deltaTime;
        arrowsLateral.transform.position += transform.up * speed * level * Time.deltaTime;
        
        if (!timer.GetCounting()) {
            timeElapsed += 10 - timer.GetTime();
            Invoke("EndGame", 2f);
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame() {
        if (level - 1 != 5) {
            imageShow.SwitchShow(false);
        }

        Debug.Log("Arrow dodge time elapsed "+ timeElapsed);
        SerializableDictionary<GameStatsEnum,float> value = new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.TimeElapsed, timeElapsed},
            {GameStatsEnum.GameHealth, (level - 1) * 0.02f},
            {GameStatsEnum.GameCurrency, (level - 1) * 3}
        };
        GameStatisticsManager.Instance.updateStatsWith(value);

        level = 1;
        timeElapsed = 0f;

        coordinator.LoadScene("MainGameScene");
    }
}
