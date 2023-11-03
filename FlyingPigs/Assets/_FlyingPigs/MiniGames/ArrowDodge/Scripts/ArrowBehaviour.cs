using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private Transform arrows;
    [SerializeField] private Timer timer;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ImageShow imageShow;
    public Sprite spriteBigArrow;
    public Sprite spriteEndArrow;
    private bool descending = false;
    public float speed;
    private bool levelEnded = false;
    static public int level = 1;

    void Start()
    {
        Transform[] children = arrows.GetComponentsInChildren<Transform>();
        Destroy(children[UnityEngine.Random.Range(1, children.Count())].gameObject);
        speed = 2;
        audioManager.PlaySound(audioManager.arrowFire);
    }

    void Update()
    {
        if(!descending) {
            if(arrows.position.y > 6) {
                descending = true;
                speed = -speed;
                int count = arrows.transform.childCount;
                for(int i = 0; i < count; i++) {
                    Transform child = arrows.transform.GetChild(i);
                    child.gameObject.GetComponent<SpriteRenderer>().sprite = spriteBigArrow;
                }
                //arrows.transform.localScale = new Vector3(arrows.transform.localScale.x, -arrows.transform.localScale.y, arrows.transform.localScale.z);
            }
        }
        else {
            if(arrows.position.y < -4) {
                if (!levelEnded) {

                    int count = arrows.transform.childCount;
                    for(int i = 0; i < count; i++) {
                        Transform child = arrows.transform.GetChild(i);
                        child.gameObject.GetComponent<SpriteRenderer>().sprite = spriteEndArrow;
                    }
                    speed = 0;
                    audioManager.PlaySound(audioManager.arrowGroundImpact);

                    if (level < 5) {
                    level += 1;
                    imageShow.SwitchShow(true);
                    Invoke("RestartGame", 0.5f);
                    }
                    
                    else {
                        level = 0;
                        imageShow.SwitchShow(true);
                        Invoke("EndGame", 0.5f);
                    }
                }
                levelEnded = true;
            }
        }
        arrows.transform.position += transform.up * speed * level * Time.deltaTime;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timer.RestartTimer();
    }

    public void EndGame() {
        if (level != 0) {
            imageShow.SwitchShow(false);
        }
        level = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //CHANGE WITH MAIN SCREEN
        //timer.RestartTimer();
    }
}
