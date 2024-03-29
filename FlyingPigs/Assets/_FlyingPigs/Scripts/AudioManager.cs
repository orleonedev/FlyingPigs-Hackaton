using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource[] audioSourcesLoop;

    [Header("Misc")]
    public AudioClip buttonpressClip;
    public AudioClip genericSuccessClip;
    public AudioClip genericFailClip;
    public AudioClip gameOverClip;
    public AudioClip messageBubbleClip;
    public AudioClip messageNotificationClip;
    public AudioClip minigameEventSound;
    public AudioClip typingClip;
    public AudioClip buyGemsClip;
    public AudioClip startOfDayClip;
    public AudioClip endOfDayClip;

    [Header("MainScene")]
    public AudioClip attack;
    public AudioClip levelUp;
    public AudioClip slimeDeath;

    [Header("Endings")]
    public AudioClip GoodEndingClip;
    public AudioClip BadEndingClip;

    [Header("Timer")]
    public AudioClip tickingClip;
    public AudioClip timeUpClip;

    [Header("Loot Ninja")]
    public AudioClip LootCut;

    [Header("Pig Hunt")]
    public AudioClip pigClip;
    public AudioClip shootClip;

    [Header("Flappy Pig")]
    public AudioClip jumpgClip;
    public AudioClip woodLogClip;

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

    [Header("Legend of Vase")]
    public AudioClip vaseClip;

    [Header("SuperKnightBros")]
    public AudioClip jump;
    public AudioClip rockSmash;

    [Header("End Minigame")]
    public AudioClip endMinigameFail;
    public AudioClip endMinigameSucc;

    [Header("MainGame")]
    public AudioClip mainTheme;
    public AudioClip mainGameTheme;
    public AudioClip minigameTheme;

    [Header("Credits")]
    public AudioClip mainThemeCredits;

    void Start() {
        /*
        audioSources = this.GetComponentsInChildren<AudioSource>();
        audioSourceLoop = this.GetComponentsInChildren<AudioSource>()[audioSources.Length - 1];
        audioSourceLoop.loop = true;
        Array.Resize(ref audioSources, audioSources.Length - 1);
        */

    }

    public void PlaySound(AudioClip clip, float volume = 0.7f){
        foreach(AudioSource audioSource in audioSources){
            if(!audioSource.isPlaying){
                audioSource.volume = volume;
                audioSource.clip = clip;
                audioSource.Play();
                return;
            }
        }
    }

    public int PlaySoundLoop(AudioClip clip, float volume = 0.5f){
        for (int i = 0; i < audioSourcesLoop.Length; i++) {
            if(!audioSourcesLoop[i].isPlaying){
                audioSourcesLoop[i].volume = volume;
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

    public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume){
        if(!fadeIn){
            double lenghtOfSource = (double) source.clip.samples/source.clip.frequency;
            yield return new WaitForSecondsRealtime((float) lenghtOfSource - duration);
        }

        float time = 0f;
        float startVolume = source.volume;
        while(time < duration){
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time/duration);
            yield return null;
        }

        yield break;
    }
}
