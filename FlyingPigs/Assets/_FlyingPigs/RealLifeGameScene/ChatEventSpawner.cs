using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChatEventSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject chatNotification; 

    [SerializeField]
    public GameLoopManager gameLoopManager;

    private float timelapse = 10f;
    private bool updateEnable = false;
    // Start is called before the first frame update
    void Start()
    {
        gameLoopManager.OnChatEvent += OnChatEvent;

    }

    // Update is called once per frame
    void Update()
    {
        if (updateEnable){
            timelapse -= Time.deltaTime;

            if (timelapse <= 0) {
                updateEnable = false;
                chatNotification.SetActive(false);
            }
        }
    }

    public void OnChatEvent() {
        Debug.Log("Create the Chat Notification");
        chatNotification.SetActive(true);
        timelapse = 10f;
        updateEnable = true;
    }
}
