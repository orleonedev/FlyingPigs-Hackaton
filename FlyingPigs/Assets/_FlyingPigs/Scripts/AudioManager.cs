using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    public AudioSource[] audioSourcesLoop;

    [Header("Timer")]
    public AudioClip tickingClip;
    public AudioClip timeUpClip;

    [Header("Pig Hunt")]
    public AudioClip pigClip;
    public AudioClip shootClip;

    [Header("Arrow")]
    public AudioClip arrowFire;
    public AudioClip arrowGroundImpact;
    public AudioClip playerHit;
    public AudioClip playerStep;

    [Header("Catapult")]
    public AudioClip catapultThrow;
    public AudioClip catapultLoop;
    public AudioClip playerWallImpact;
    public AudioClip castleDestruction;

    void Start() {
        /*
        audioSources = this.GetComponentsInChildren<AudioSource>();
        audioSourceLoop = this.GetComponentsInChildren<AudioSource>()[audioSources.Length - 1];
        audioSourceLoop.loop = true;
        Array.Resize(ref audioSources, audioSources.Length - 1);
        */
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

    public int PlaySoundLoop(AudioClip clip){
        for (int i = 0; i < audioSourcesLoop.Length; i++) {
            if(!audioSourcesLoop[i].isPlaying){
                audioSourcesLoop[i].clip = clip;
                audioSourcesLoop[i].Play();
                return i;
            }
        }
        return -1;
    }

    public void StopSoundLoop(int i){
        if(audioSourcesLoop[i].isPlaying) {
            audioSourcesLoop[i].Stop();
        }
    }
}
