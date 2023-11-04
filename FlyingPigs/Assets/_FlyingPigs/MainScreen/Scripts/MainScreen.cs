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

    // Start is called before the first frame update
    void Start()
    {
        loopNum = audioManager.PlaySoundLoop(audioManager.mainTheme);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
