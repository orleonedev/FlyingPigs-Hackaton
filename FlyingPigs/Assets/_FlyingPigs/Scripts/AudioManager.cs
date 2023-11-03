using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] audioSources;
    public AudioClip pigClip;
    public AudioClip shootClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = this.GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip){
        foreach(AudioSource audioSource in audioSources){
            if(!audioSource.isPlaying){
                audioSource.clip = clip;
                audioSource.Play();
                return;
            }
        }
    }
}
