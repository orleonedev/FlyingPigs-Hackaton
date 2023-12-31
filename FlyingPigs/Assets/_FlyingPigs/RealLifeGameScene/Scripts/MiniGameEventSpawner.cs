using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MiniGameEventSpawner : MonoBehaviour
{
    

    [SerializeField]
    public GameObject MinigameEventAlert; 

    [SerializeField]
    public GameLoopManager gameLoopManager;

    private float timelapse = 30f;
    private bool updateEnable = false;
    // Start is called before the first frame update
    void Start()
    {
        gameLoopManager.OnMinigameEvent += OnMinigameEvent;

    }

    // Update is called once per frame
    void Update()
    {
        if (updateEnable){
            timelapse -= Time.deltaTime;

            if (timelapse <= 0) {
                updateEnable = false;
                MinigameEventAlert.SetActive(false);
            }
        }
    }

    public void OnMinigameEvent() {
        Debug.Log("SetVisible the minigame alert");
        MinigameEventAlert.SetActive(true);
        timelapse = 30f;
        updateEnable = true;
    }
}
