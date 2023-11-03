using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] audioSources;

    private AudioSource audioSourceLoop;

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
        audioSources = this.GetComponentsInChildren<AudioSource>();
        audioSourceLoop = this.GetComponentsInChildren<AudioSource>()[audioSources.Length - 1];
        audioSourceLoop.loop = true;
        Array.Resize(ref audioSources, audioSources.Length - 1);
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

    public void PlaySoundLoop(AudioClip clip){
        if(!audioSourceLoop.isPlaying){
            audioSourceLoop.clip = clip;
            audioSourceLoop.Play();
        }
    }

    public void StopSoundLoop(){
        if(audioSourceLoop.isPlaying){
            audioSourceLoop.Stop();
        }
    }
}
