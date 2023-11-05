using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{

    [SerializeField]
    public AudioManager audioManager;

    [SerializeField]
    public GameObject transitionLayer;

    private int loopNum = -1;

    void Start()
    {
        loopNum = audioManager.PlaySoundLoop(audioManager.mainTheme);
        GameStatisticsManager.Instance.ReloadGame();
        FakeGameManager.Instance.ReloadGame();
    }

    void Update()
    {
        
    }
}
