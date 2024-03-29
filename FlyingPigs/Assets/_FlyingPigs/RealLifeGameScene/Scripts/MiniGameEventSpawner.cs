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

    [SerializeField]
    public GameStateManager GameState;

    [SerializeField]
    public AudioManager audioManager;

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
        if (updateEnable && GameState.InGameSceneObject.activeInHierarchy){
            timelapse -= (Time.deltaTime*2);

            if (timelapse <= 0) {
                updateEnable = false;
                MinigameEventAlert.SetActive(false);
            }
        }
    }

    public void OnMinigameEvent() {
        Debug.Log("SetVisible the minigame alert");
        MinigameEventAlert.SetActive(true);
        audioManager.PlaySound(audioManager.minigameEventSound, 1.0f);
        timelapse = 30f;
        updateEnable = true;
    }
}
