using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{

    [SerializeField]
    public AudioManager audioManager;

    [SerializeField]
    public GameObject transitionLayer;

    [SerializeField]
    public Animator animator;

    [SerializeField]
    public Coordinator coordinator;

    private int loopNum = -1;

    void Start()
    {
        Application.targetFrameRate = 60;
        loopNum = audioManager.PlaySoundLoop(audioManager.mainTheme);
        audioManager.audioSourcesLoop[loopNum].volume = 0f;
        StartCoroutine(audioManager.Fade(true, audioManager.audioSourcesLoop[loopNum], 1.5f, 0.7f));
        GameStatisticsManager.Instance.ReloadGame();
        GregChatEvents.Instance.ResetPreviousList();
        EvelyneChatEvents.Instance.ResetPreviousList();
        SarahChatEvents.Instance.ResetPreviousList();
        EmployerChatEvents.Instance.ResetPreviousList();
        BillChatEvents.Instance.ResetPreviousList();
        MarkChatEvents.Instance.ResetPreviousList();
        FakeGameManager.Instance.ReloadGame();
    }

    public void StartGame(){
        animator.SetBool("gameStart", true);
        StartCoroutine(audioManager.Fade(true, audioManager.audioSourcesLoop[loopNum], 1.5f, 0f));
        Invoke("LoadScene", 1.5f);
    }

    private void LoadScene(){
        coordinator.LoadScene("MainGameScene");
    }
}
