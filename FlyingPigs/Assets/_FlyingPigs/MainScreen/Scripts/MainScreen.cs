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
        GregChatEvents.Instance.ResetPreviousList();
        EvelyneChatEvents.Instance.ResetPreviousList();
        SarahChatEvents.Instance.ResetPreviousList();
        EmployerChatEvents.Instance.ResetPreviousList();
        BillChatEvents.Instance.ResetPreviousList();
        MarkChatEvents.Instance.ResetPreviousList();
        FakeGameManager.Instance.ReloadGame();
    }

    void Update()
    {
        
    }
}
