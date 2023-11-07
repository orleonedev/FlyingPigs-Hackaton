using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] public AudioManager audioManager;

    private int loopNum = -1;

    void Start()
    {

        loopNum = audioManager.PlaySoundLoop(audioManager.mainThemeCredits);

    }

}
