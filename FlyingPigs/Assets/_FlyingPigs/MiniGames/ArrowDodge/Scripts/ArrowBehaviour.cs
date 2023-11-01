using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private Transform arrows;
    [SerializeField] private Timer timer;
    [SerializeField] private ImageShow imageShow;
    private bool descending = false;
    private float speed;
    private bool levelEnded = false;
    static private int level = 1;

    void Start()
    {
        Transform[] children = arrows.GetComponentsInChildren<Transform>();
        Destroy(children[UnityEngine.Random.Range(1, children.Count())].gameObject);
        speed = 2;
    }

    void Update()
    {
        if(!descending) {
            if(arrows.position.y > 6) {
                descending = true;
                speed = -speed;
                arrows.transform.localScale = new Vector3(arrows.transform.localScale.x, -arrows.transform.localScale.y, arrows.transform.localScale.z);
            }
        }
        else {
            if(arrows.position.y < -6) {
                if (!levelEnded) {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //CHANGE WITH MAIN SCREEN
        //timer.RestartTimer();
    }
}