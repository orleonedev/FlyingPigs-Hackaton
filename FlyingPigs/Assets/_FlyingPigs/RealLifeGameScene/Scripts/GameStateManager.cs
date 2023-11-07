using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    public GameObject InGameSceneObject;

    [SerializeField]
    public GameObject ChatSceneObject;

    [SerializeField]
    public AudioManager audioManager;
    public int numOfAudioSource = -1;

    void Start()
    {
        SwitchToInGame();
    }

    public void SwitchToInGame() {
        if (ChatSceneObject.activeInHierarchy) {
            ChatSceneObject.SetActive(false);
        }
        InGameSceneObject.SetActive(true);
        numOfAudioSource = audioManager.PlaySoundLoop(audioManager.mainGameTheme, 0f);
        StartCoroutine(audioManager.Fade(true, audioManager.audioSourcesLoop[numOfAudioSource], 3f, 1f));
    }

    public void SwitchToChat() {
        if (InGameSceneObject.activeInHierarchy) {
            InGameSceneObject.SetActive(false);
        }
        ChatSceneObject.SetActive(true);
        if (numOfAudioSource != -1) {
            audioManager.StopSoundLoop(numOfAudioSource);
            numOfAudioSource = -1;
        }
    }
}
